namespace NftShirtApi.Blockchain.Infra;
public interface IPolygonRepository
{
    public Task<string> getMetadadosNftToBlockchain();
    public Task<string> getContrateNft();
}