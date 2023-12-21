namespace NftShirtApi.Blockchain.Services;

public static class PolygonService{
    private static string _provedorRpc = "https://polygon-rpc.com";
    
    public static dynamic web3 = new Nethereum.Web3.Web3(_provedorRpc);
    public static async Task<dynamic>  getContractAsync (string abi, string contractAddress){
        return await web3.Eth.GetContract(abi, contractAddress);
    }  
    public static async Task<string> getTokenByUriAndIdAsync (dynamic contract, string tokenUri, string tokenId){
        var function = contract.GetFunction(tokenUri);
        return await function.CallAsync<string>(tokenId);
    }  
}