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
using Newtonsoft.Json;
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
        _httpClient = new HttpClient();
        _web3 = new Nethereum.Web3.Web3("https://polygon-rpc.com");
    }
    
    public async Task<string> GetAbiByAdress(string contractAddress)
{
    string url = $"https://api.polygonscan.com/api?module=contract&action=getabi&address={contractAddress}";
    Console.WriteLine($"Link do request da abi: {url}");
    try
    { 
        HttpResponseMessage response = await _httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            string jsonResponse = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
            string abi = responseObject.result; // Extrai a ABI do campo 'result'
            if (string.IsNullOrEmpty(abi) || abi == "Contract source code not verified")
            {
                throw new Exception("ABI não encontrada ou contrato não verificado.");
            }
            Console.WriteLine("ABI obtida com sucesso.");
            return abi;
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
    
    public async Task<dynamic> GetContractAsync(string contractAddress)
    {
        try
        {
            string abi = await GetAbiByAdress(contractAddress);
            var contract = _web3.Eth.GetContract(abi, contractAddress); 

            if (contract != null)
            {
                Console.WriteLine("Contrato obtido com sucesso.");
                return contract;
            }
            else
            {
                Console.WriteLine("Contrato não encontrado.");
                return null;
            }
        }
        catch (Exception e)
        {
            throw new Exception($"Erro ao obter contrato: {e.Message}");
        }
    }



    
}