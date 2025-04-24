using ASC_Web.Areas.Configuration.Models;

namespace ASC_Web.Areas.Configuration.Models
{
    public class MasterKeysViewModel
    {
        public List<MasterDataKeyViewModel>? MasterKeys { get; set; }
        public MasterDataKeyViewModel MasterKeyInContext { get; set; }
        public bool IsEdit { get; set; }
    }
}