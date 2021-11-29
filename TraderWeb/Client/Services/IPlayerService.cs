using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraderWeb.Shared;

namespace TraderWeb.Client.Services
{
    public interface IPlayerService
    {
        List<Player> Players { get; set; }
        event Action OnChange;
        Task<List<Player>> GetPlayers();
        Task<List<Player>> UpdatePlayer(Player Player,int id);
        Task<Player> GetSinglePlayer(int id);
        Task<Player> SellPlayer(string playername);

        Task<Player> CombinePlayers(string pair);
        Task<Player> RemovePlayer(string playername);
        Task<Player> AddPlayer(string emptyString);
     
    }
}
