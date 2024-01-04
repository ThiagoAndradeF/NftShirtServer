using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.OpenApi.Any;
using Microsoft.VisualBasic;
using Nethereum.ABI.FunctionEncoding;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts.QueryHandlers.MultiCall;
using Nethereum.Contracts.Standards.ENS.ENSRegistry.ContractDefinition;
using Nethereum.RPC.Eth.DTOs;
using NftShirt.Server.Infra.IRepositories;

namespace NftShirtApi.Infra.Blockchain;

public interface IPollygonContractService{
    public Task<string> GetAbiByAdress(string contractAddress);
    public Task<dynamic>  GetContractAsync(string contractAddress);
}
public class PollygonContractService : IPollygonContractService{
    private readonly HttpClient _httpClient;
    private dynamic _web3;
    public PollygonContractService(){
        Console.WriteLine("Chgueeeei aqui;");
        _httpClient = new HttpClient();
        _web3 = new Nethereum.Web3.Web3("https://polygon-rpc.com");
    }
    
    public async Task<string> GetAbiByAdress(string contractAddress){
        string url = $"https://api.polygonscan.com/api?module=contract&action=getabi&address={contractAddress}";
        Console.WriteLine($"Link do request da abi: {url}");
        try
        { 
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                Console.WriteLine("json " + json);
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
    
    public async Task<dynamic>  GetContractAsync(string contractAddress){
        try{
            string abi = await GetAbiByAdress(contractAddress);
            var contract = await _web3.Eth.GetContract(abi, contractAddress); 
            Console.WriteLine(contract);
            return contract;
        }
        catch(Exception e){
             throw new Exception($"Erro ao obter contrato: {e.Message}");
        }
        
    }  

    
}