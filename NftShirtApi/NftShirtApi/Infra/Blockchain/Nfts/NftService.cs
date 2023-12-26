using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.OpenApi.Any;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts.Standards.ENS.ENSRegistry.ContractDefinition;
using Nethereum.RPC.Eth.DTOs;
using NftShirt.Server.Infra.Models;

namespace NftShirtApi.Infra.Blockchain.Nfts;

public class NftService{
    private readonly HttpClient _httpClient;
    private readonly string _abi;
    private readonly string _contractAddress;
    private readonly dynamic _web3;
    private NftDto _nftSelected;
    public NftService(NftDto nftSelected , string abi, string contractAddress){
        _httpClient = new HttpClient();
        _abi = abi;
        _contractAddress = contractAddress;
        _web3 = new Nethereum.Web3.Web3("https://polygon-rpc.com");
        _nftSelected = nftSelected;
        
    }
    
    public async Task<string> getAbiByAdress (){
        string url = $"https://api.polygonscan.com/api?module=contract&action=getabi&address={_contractAddress}";
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                return json;
            }
            else
            {
                Console.WriteLine($"Erro na requisição: {response.StatusCode}");
                return null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exceção ao fazer a requisição: {ex.Message}");
            return null;
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