using System;
using Newtonsoft.Json;
using NftShirtApi.Blockchain.Models;
namespace NftShirtApi.Blockchain.Services;



public class JsonLoader
{
    public string LoadJson(string filePath)
    {
        try
        {
            string json = File.ReadAllText(filePath);
            return json;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao carregar JSON: " + ex.Message);
            return null;
        }
    }
}