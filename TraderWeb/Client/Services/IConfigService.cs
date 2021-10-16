using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraderWeb.Shared;

namespace TraderWeb.Client.Services
{
    public interface IConfigService
    {
        List<Config> configs { get; set; }
        event Action OnChange;
        Task<List<Config>> GetConfigs();
        Task<List<Config>> UpdateConfig(Config config,int id);
        Task<Config> GetSingleConfig(int id);
    }
}
