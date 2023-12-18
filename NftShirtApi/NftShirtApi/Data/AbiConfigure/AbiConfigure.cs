using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
namespace NftShirt.Server.Data;
public class AbiConfigure
{
    public string setarAbiFirst(){
        string jsonString = "{\"status\":\"1\",\"message\":\"OK-Missing/Invalid API Key, rate limit of 1/5sec applied\",\"result\":\"[{\\\"inputs\\\":[{\\\"internalType\\\":\\\"address\\\",\\\"name\\\":\\\"beacon\\\",\\\"type\\\":\\\"address\\\"},{\\\"internalType\\\":\\\"bytes\\\",\\\"name\\\":\\\"data\\\",\\\"type\\\":\\\"bytes\\\"}],\\\"stateMutability\\\":\\\"payable\\\",\\\"type\\\":\\\"constructor\\\"},{\\\"stateMutability\\\":\\\"payable\\\",\\\"type\\\":\\\"fallback\\\"},{\\\"stateMutability\\\":\\\"payable\\\",\\\"type\\\":\\\"receive\\\"}]\"}";
        return configurarAbi(jsonString);
    }

    public string configurarAbi(string JsonAbiDesconfigurado){
        JObject jsonObject = JObject.Parse(JsonAbiDesconfigurado);
        string resultString = jsonObject["result"].ToString();
        JArray resultArray = JArray.Parse(resultString);
        string jsonToSave = resultArray.ToString(Newtonsoft.Json.Formatting.None);
        return jsonToSave;
    }



// ...




}