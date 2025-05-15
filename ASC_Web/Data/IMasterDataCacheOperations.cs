namespace ASC_Web.Data
{
    public interface IMasterDataCacheOperations
    {
        Task<MasterDataCache> GetMasterDataCacheAsync();
        Task CreateMasterDataCacheAsync();
    }
}
