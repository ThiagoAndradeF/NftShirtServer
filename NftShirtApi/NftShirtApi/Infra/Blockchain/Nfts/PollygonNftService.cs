using System.Net.Http;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.OpenApi.Any;
using Nethereum.ABI.FunctionEncoding;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts.QueryHandlers.MultiCall;
using Nethereum.Contracts.Standards.ENS.ENSRegistry.ContractDefinition;
using Nethereum.RPC.Eth.DTOs;
using Newtonsoft.Json;
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
    private readonly IPollygonContractService _pollygonContractService;
    private string? _contractAddress;
    private dynamic _web3;

    public PollygonNftService(INftRepository nftRepository, IPollygonContractService pollygonContractService){
        _httpClient = new HttpClient();
        _web3 = new Nethereum.Web3.Web3("https://polygon-rpc.com");
        _nftRepository = nftRepository;
        _pollygonContractService = pollygonContractService; 
        // SetInitValuesAsync();
    }
    
    public async Task<string> GetTokenUriAsync(string tokenId, string contractAddress)
    {
        try
        {
            var contract =  await _pollygonContractService.GetContractAsync(contractAddress);
            var tokenURIFunction = contract.GetFunction("uri");
            var tokenURI = await tokenURIFunction.CallAsync<string>(tokenId);
            return tokenURI;
        }
        catch (Exception ex)
        {
            throw new Exception($"Problema ao encontrar URI desse token: {ex.Message}");
        }
    }
    
    
    public async Task<string> GetCurrentWalletAddressAsync(string tokenId, string contractAddress)
    {
        try
        {
            BigInteger tokenBigInt = BigInteger.Parse(tokenId);
            var contract = await _pollygonContractService.GetContractAsync(contractAddress);
            var transferSingleEvent = contract.GetEvent("TransferSingle");
            var filterInput = transferSingleEvent.CreateFilterInput(new BlockParameter(0), BlockParameter.CreateLatest(), new object[] { null, null, null, tokenBigInt });
            var logs = await transferSingleEvent.GetAllChanges<TransferSingleEventDTO>(filterInput);

            if (logs.Any())
            {
                return logs.Last().Event.To; // Retorna o último endereço 'to'
            }

            return null; // Se não houver logs de transferência, não é possível determinar o proprietário atual
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao obter o endereço da carteira atual: {ex.Message}");
        }
    }
    public class TransferSingleEventDTO : IEventDTO
    {
        [Parameter("address", "_operator", 1, true)]
        public string Operator { get; set; }

        [Parameter("address", "_from", 2, true)]
        public string From { get; set; }

        [Parameter("address", "_to", 3, true)]
        public string To { get; set; }

        [Parameter("uint256", "_id", 4, true)]
        public BigInteger TokenId { get; set; }

        [Parameter("uint256", "_value", 5, false)]
        public BigInteger Value { get; set; }
    }
}