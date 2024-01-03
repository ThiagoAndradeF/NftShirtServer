using System.Net.Http;
using System.Runtime.CompilerServices;
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


public interface IPollygonNftService{
    public Task<string>  GetTokenUriAsync(string tokenId, string contractAdress);
    public Task<string>  GetCurrentWalletAddressAsync(string tokenId, string contractAdress);
}
public class PollygonNftService : IPollygonNftService{
    private readonly HttpClient _httpClient;
    private readonly INftRepository _nftRepository;
    private string? _abi;
    private string? _contractAddress;
    private dynamic _web3;

    public PollygonNftService(INftRepository nftRepository){
        _httpClient = new HttpClient();
        _web3 = new Nethereum.Web3.Web3("https://polygon-rpc.com");
        _nftRepository = nftRepository;
        // SetInitValuesAsync();
    }
    public async Task<string> GetAbiByAdress(string contractAdress){
        string url = $"https://api.polygonscan.com/api?module=contract&action=getabi&address={contractAdress}";
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

    public async Task<string> GetTokenUriAsync(string tokenId, string contractAdress)
    {
        try
        {
            var abi = GetAbiByAdress(contractAdress);
            var contract = _web3.Eth.GetContract(_abi, tokenId);
            var tokenURIFunction = contract.GetFunction("tokenURI");
            var tokenURI = await tokenURIFunction.CallAsync<string>(tokenId);
            return tokenURI;
        }
        catch (Exception ex)
        {
            
            throw new Exception($"Problema ao encontrar URI desse token : {ex.Message}");
        }
    }
    
    
    public async Task<string> GetCurrentWalletAddressAsync(string tokenId, string contractAdress)
    {
        var abi = GetAbiByAdress(contractAdress);
        var contract = _web3.Eth.GetContract(abi, contractAdress);
        var transferEventHandler = contract.GetEvent("Transfer");
        var filterInput = transferEventHandler.CreateFilterInput(new BlockParameter(0), BlockParameter.CreateLatest(), tokenId: tokenId);
        var logs = await transferEventHandler.GetAllChanges<TransferEventDTO>(filterInput);
        if (logs.Count > 0)
        {
            return logs[^1].Event.To; // Retorna o último endereço 'to'
        }
        var ownerOfFunction = contract.GetFunction("ownerOf");
        string ownerAddress = await ownerOfFunction.CallAsync<string>(tokenId);
        return ownerAddress; // Nenhuma transação encontrada para este Token ID retorna endereço do owner
    }
}