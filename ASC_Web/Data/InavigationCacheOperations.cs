using ASC_Web.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ASC_Web.Data
{
    public interface INavigationCacheOperations
    {
        Task<NavigationMenu> GetNavigationCacheAsync();

        Task CreateNavigationCacheAsync();
    }
}