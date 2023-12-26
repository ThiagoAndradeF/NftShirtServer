using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.OpenApi.Any;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts.QueryHandlers.MultiCall;
using Nethereum.Contracts.Standards.ENS.ENSRegistry.ContractDefinition;
using Nethereum.RPC.Eth.DTOs;
using NftShirt.Server.Infra.IRepositories;
using NftShirt.Server.Infra.Models;

namespace NftShirtApi.Infra.Blockchain;

public class PollygonNftService{
    private readonly HttpClient _httpClient;
    private string? _abi;
    private string? _contractAddress;
    private dynamic? _web3;
    private readonly INftRepository _nftRepository;
    private NftDto _nftSelected;
    public PollygonNftService(NftDto nftSelected , string contractAddress ){
        _httpClient = new HttpClient();
        _web3 = new Nethereum.Web3.Web3("https://polygon-rpc.com");
        getAbiByAdress();
        _contractAddress = contractAddress;
        _nftSelected = nftSelected;
    }
    public PollygonNftService(INftRepository nftRepository, string tokenId){
        _httpClient = new HttpClient();
        _nftRepository = nftRepository;
        setarValoresPorBanco(tokenId);
    }
    public async void setarValoresPorBanco(string tokenId){
        try
        {
            _web3 = new Nethereum.Web3.Web3("https://polygon-rpc.com");
            NftWithCollectionDto nftComplete = await _nftRepository.GetNftCompleteByIdAsync(tokenId);
            _contractAddress = nftComplete.Contract.Adress;
            if ( nftComplete.Contract.Abi.ToString() == null){
                _abi = nftComplete.Contract.Abi.ToString();
            }else{
                getAbiByAdress();
            }
            _nftSelected = nftComplete.Nft;
        }
        catch (System.Exception)
        {
            throw new Exception("Erro ocorrido ao resgatar valores pelo banco de dados");
        }
        
    }


    public async Task<string> getAbiByAdress(){
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
    
    public async Task<dynamic>  getContractAsync(){
        return await _web3.Eth.GetContract(_abi, _contractAddress);
    }  

    public  async Task<string> getTokenByUriAndIdAsync (){

        var contract = await getContractAsync();
        var function = contract.GetFunction(_nftSelected.TokenURI);
        return await function.CallAsync<string>(_nftSelected.TokenId);
    }  

    [Event("Transfer")]
    public class TransferEventDTO : IEventDTO
    {
        [Parameter("address", "from", 1, true)]
        public string From { get; set; }

        [Parameter("address", "to", 2, true)]
        public string To { get; set; }

        [Parameter("uint256", "tokenId", 3, true)]
        public AnyType TokenId { get; set; }
    }
    
    public async Task<string> GetCurrentWalletAddressAsync()
    {
        var contract = _web3.Eth.GetContract(_abi, _contractAddress);
        var transferEventHandler = contract.GetEvent("Transfer");
        var filterInput = transferEventHandler.CreateFilterInput(new BlockParameter(0), BlockParameter.CreateLatest(), tokenId: _nftSelected.TokenId);
        var logs = await transferEventHandler.GetAllChanges<TransferEventDTO>(filterInput);
        if (logs.Count > 0)
        {
            return logs[^1].Event.To; // Retorna o último endereço 'to'
        }
        return null; // Nenhuma transação encontrada para este Token ID
    }
}