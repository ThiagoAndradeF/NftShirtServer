using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace NftShirt.Server.Data;
public class AbiConfigure
{
    public string setarAbiFirst(){
        string jsonString = File.ReadAllText("C:/Users/PC/Desktop/NftShirt/NftShirtServer/NftShirtApi/NftShirtApi/Blockchain/Services/AbiConfigure/AbiFirst.json");
        return configurarAbi(jsonString);
    }
    public T ConvertJsonToObject<T>(string jsonString)
    {
        try
        {
            T obj = JsonConvert.DeserializeObject<T>(jsonString);
            return obj;
        }
        catch (JsonException ex)
        {
            // Manipulação de erros de conversão
            Console.WriteLine("Erro na conversão do JSON: " + ex.Message);
            return default(T);
        }
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