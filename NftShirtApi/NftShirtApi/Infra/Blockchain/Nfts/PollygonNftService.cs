using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.OpenApi.Any;
using Nethereum.ABI.FunctionEncoding;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts.QueryHandlers.MultiCall;
using Nethereum.Contracts.Standards.ENS.ENSRegistry.ContractDefinition;
using Nethereum.RPC.Eth.DTOs;
using NftShirt.Server.Infra.IRepositories;
using NftShirt.Server.Infra.Models;

namespace NftShirtApi.Infra.Blockchain;
public class PollygonNftService{
    private readonly HttpClient _httpClient;
    private readonly INftRepository _nftRepository;
    private string? _abi;
    private string? _contractAddress;
    private dynamic _web3;
    private string _tokenId;
    private string? _tokenUri;
    public PollygonNftService(INftRepository nftRepository, string tokenId){
        _httpClient = new HttpClient();
        _web3 = new Nethereum.Web3.Web3("https://polygon-rpc.com");
        _tokenId = tokenId;
        _nftRepository = nftRepository;
        SetInitValuesAsync();
    }

    public async void SetInitValuesAsync(){
        try
        {
            NftWithCollectionDto nftComplete = await _nftRepository.GetNftCompleteByIdAsync(_tokenId);
            _tokenUri = await GetTokenUriAsync();
            if(nftComplete.Contract != null){
                _contractAddress = nftComplete.Contract.Adress;
            }else{
                throw new Exception("Erro ocorrido ao resgatar o adress do contrato no banco de dados");
            }
            if ( nftComplete.Contract.Abi.ToString() != null){
                _abi = nftComplete.Contract.Abi.ToString();
            }else{
                _abi = await GetAbiByAdress();
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ocorrido ao vincular valores: " + ex.Message);
        }
        
    }


    public async Task<string> GetAbiByAdress(){
        string url = $"https://api.polygonscan.com/api?module=contract&action=getabi&address={_contractAddress}";
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                _abi = json;
                return json;
            }
            else
            {
                throw new Exception($"Erro na requisição: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Exceção ao fazer a requisição: {ex.Message}");
        }

    }
    
    public async Task<dynamic>  GetContractAsync(){
        return await _web3.Eth.GetContract(_abi, _contractAddress);
    }  

    [Event("Transfer")]
    public class TransferEventDTO : IEventDTO
    {
        [Parameter("address", "from", 1, true)]
        public string From { get; set; } = string.Empty;

        [Parameter("address", "to", 2, true)]
        public string To { get; set; } = string.Empty;

        [Parameter("uint256", "tokenId", 3, true)]
        public AnyType TokenId { get; set; }
    }



    public async Task<string> GetTokenUriAsync()
    {
        try
        {
            var contract = _web3.Eth.GetContract(_abi, _tokenId);
            var tokenURIFunction = contract.GetFunction("tokenURI");
            var tokenURI = await tokenURIFunction.CallAsync<string>(_tokenId);
            return tokenURI;
        }
        catch (Exception ex)
        {
            
            throw new Exception($"Problema ao encontrar URI desse token : {ex.Message}");
        }
    }
    
    
    public async Task<string> GetCurrentWalletAddressAsync()
    {
        var contract = _web3.Eth.GetContract(_abi, _contractAddress);
        var transferEventHandler = contract.GetEvent("Transfer");
        var filterInput = transferEventHandler.CreateFilterInput(new BlockParameter(0), BlockParameter.CreateLatest(), tokenId: _tokenId);
        var logs = await transferEventHandler.GetAllChanges<TransferEventDTO>(filterInput);
        if (logs.Count > 0)
        {
            return logs[^1].Event.To; // Retorna o último endereço 'to'
        }
        var ownerOfFunction = contract.GetFunction("ownerOf");
        string ownerAddress = await ownerOfFunction.CallAsync<string>(_tokenId);
        return ownerAddress; // Nenhuma transação encontrada para este Token ID retorna endereço do owner
    }



    
}