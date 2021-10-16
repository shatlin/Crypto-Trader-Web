using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraderWeb.Server.Data;
using TraderWeb.Shared;



namespace TraderWeb.Server.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {

        private readonly ILogger<PlayerController> _logger;
        private readonly DB _db;

        public PlayerController(ILogger<PlayerController> logger, DB db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<List<Player>> AllPlayer()
        {
            var players = await _db.Player.AsNoTracking().OrderByDescending(x => x.ProfitLossAmt).ToListAsync();

            foreach(var player in players)
            {
                if (player.IsTrading)
                {
                    var signal = await _db.SignalCandle.Where(x => x.Pair == player.Pair && x.CandleType == "1hour").AsNoTracking().OrderByDescending(x => x.CloseTime).ToListAsync();
                    player.TodayProfitLoss = ((signal.First().ClosePrice - signal.Last().ClosePrice) / signal.Last().ClosePrice) * 100;

                }
            }

            return players;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await AllPlayer());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSinglePlayer(int id)
        {
            return Ok(await _db.Player.FirstOrDefaultAsync(x => x.Id == id));
        }

        [HttpPut]
        [Route("[action]/{playername}")]
        public async Task<IActionResult> SellPlayer(string playername)
        {
            Player player = null;
            if (!string.IsNullOrEmpty(playername))
            {
                player = await _db.Player.FirstOrDefaultAsync(x => x.Name == playername);
                if (player != null)
                {
                    player.ForceSell = true;
                    _db.Update(player);
                    await _db.SaveChangesAsync();
                }
            }
            return Ok(player);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlayer(Player Player, int id)
        {
            if (Player != null && id > 0)
            {
                var dbPlayer = await _db.Player.FirstOrDefaultAsync(x => x.Id == id);
                dbPlayer.SellAbovePerc = Player.SellAbovePerc;
                dbPlayer.SellBelowPerc = Player.SellBelowPerc;
                dbPlayer.isSellAllowed = Player.isSellAllowed;
                dbPlayer.isBuyAllowed = Player.isBuyAllowed;
                dbPlayer.RepsTillCancelOrder = Player.RepsTillCancelOrder;
                dbPlayer.ForceSell = Player.ForceSell;

                dbPlayer.isBuyOrderCompleted = Player.isBuyOrderCompleted;
                dbPlayer.Quantity = Player.Quantity;
                dbPlayer.TotalBuyCost = Player.TotalBuyCost;
                dbPlayer.TotalSellAmount = Player.TotalSellAmount;
                dbPlayer.BuyOrderId = Player.BuyOrderId;
                dbPlayer.BuyCoinPrice = Player.BuyCoinPrice;
                dbPlayer.SellCoinPrice = Player.SellCoinPrice;
                dbPlayer.BuyCommision = Player.BuyCommision;

                dbPlayer.SellCommision = Player.SellCommision;
                dbPlayer.TotalCurrentValue = Player.TotalCurrentValue;
                dbPlayer.AvailableAmountToBuy = Player.AvailableAmountToBuy;
                dbPlayer.BuyTime = Player.BuyTime;

                _db.Update(dbPlayer);
                await _db.SaveChangesAsync();
            }
            return Ok(await AllPlayer());
        }
    }
}
