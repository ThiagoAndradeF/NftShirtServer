using System.Net.Http;
using System.Threading.Tasks;

namespace NftShirtApi.Blockchain.Services;

public class PolygonService{
    private readonly HttpClient _httpClient;
    public PolygonService(){
        _httpClient = new HttpClient();
    }
    private static string _provedorRpc = "https://polygon-rpc.com";
    
    public static dynamic web3 = new Nethereum.Web3.Web3(_provedorRpc);
    public async Task<string> getAbiByAdress (string Adress){
        string url = $"https://api.polygonscan.com/api?module=contract&action=getabi&address={Adress}";

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
    public async Task<dynamic>  getContractAsync (string abi, string contractAddress){
        return await web3.Eth.GetContract(abi, contractAddress);
    }  
    public  async Task<string> getTokenByUriAndIdAsync (dynamic contract, string tokenUri, string tokenId){
        var function = contract.GetFunction(tokenUri);
        return await function.CallAsync<string>(tokenId);
    }  
}