using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
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

            foreach (var player in players)
            {
                if (player.IsTrading)
                {
                    //var signal = await _db.SignalCandle.Where(x => x.Pair == player.Pair && x.CandleType == "1hour").AsNoTracking().OrderByDescending(x => x.CloseTime).ToListAsync();
                    //player.TodayProfitLoss = ((signal.First().ClosePrice - signal.Last().ClosePrice) / signal.Last().ClosePrice) * 100;
                    player.TodayProfitLoss = 0;
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
                    player.SellAtPrice = null;
                    _db.Update(player);
                    await _db.SaveChangesAsync();
                }
            }
            return Ok(player);
        }


        [HttpPut]
        [Route("[action]/{pair}")]
        public async Task<IActionResult> CombinePlayers(string pair)
        {
            Player firstPlayer = null;

            var players = await _db.Player.Where(x => x.Pair == pair && x.IsTrading == true).ToListAsync();

            if (players.Any())
            {
                firstPlayer = players.FirstOrDefault();
            }

            decimal totalQty = 0;
            decimal totalbuyCommission = 0;
            decimal totalbuycoinPrice = 0;
            decimal totalbuyCost = 0;
            decimal totalsellAmount = 0;
            decimal totalCurrentValue = 0;
            decimal totalSellCommision = 0;
            decimal totalProfitAmount = 0;

            foreach (var player in players)
            {
                if (player.Quantity != null)
                {
                    totalQty += Convert.ToDecimal(player.Quantity);
                    totalbuyCommission += Convert.ToDecimal(player.BuyCommision);
                    totalbuycoinPrice += Convert.ToDecimal(player.BuyCoinPrice);
                    totalbuyCost += Convert.ToDecimal(player.TotalBuyCost);
                    totalsellAmount += Convert.ToDecimal(player.TotalSellAmount);
                    totalCurrentValue += Convert.ToDecimal(player.TotalCurrentValue);
                    totalSellCommision += Convert.ToDecimal(player.SellCommision);
                    totalProfitAmount += Convert.ToDecimal(player.ProfitLossAmt);
                }

                //if (player.SellAtPrice != null && player.SellAtPrice > 0)
                //{
                //    sellAtPriceCounts++;
                //    totalSellAtPrice = Convert.ToDecimal(player.SellAtPrice);
                //}
            }
            firstPlayer.DayHigh = players[0].DayHigh;
            firstPlayer.DayLow = players[0].DayLow;
            firstPlayer.Quantity = totalQty;
            firstPlayer.BuyCommision = totalbuyCommission;
            firstPlayer.TotalBuyCost = totalbuyCost;
            firstPlayer.BuyCoinPrice = totalbuyCost / totalQty;
            firstPlayer.TotalSellAmount = totalsellAmount;
            firstPlayer.TotalCurrentValue = totalCurrentValue;
            firstPlayer.SellCommision = totalSellCommision;
            firstPlayer.SellAtPrice = null;
            firstPlayer.isSellAllowed = false;
            firstPlayer.ProfitLossAmt = totalProfitAmount;
            firstPlayer.BuyAtPrice = 0;
            firstPlayer.ForceSell = false;
            firstPlayer.IsTracked = true;
            _db.Player.Update(firstPlayer);

            foreach (var player in _db.Player.Where(x => x.Name != firstPlayer.Name &&x.Pair==pair &&x.IsTrading==true))
            {
                //player.Pair = null;
                //player.BuyOrSell = string.Empty;
                //player.DayHigh = 0;
                //player.DayLow = 0;
                //player.BuyCoinPrice = 0;
                //player.Quantity = 0;
                //player.BuyCommision = 0;
                //player.ProfitLossAmt = 0;
                //player.TotalSellAmount = 0;
                //player.TotalBuyCost = 0;
                //player.TotalCurrentValue = 0;
                //player.SellCoinPrice = 0;
                //player.AvailableAmountToBuy = 0;
                //player.IsTrading = false;
                //player.SellCommision = 0;
                //player.BuyOrderId = 0;
                //player.SellOrderId = 0;
                //player.CurrentCoinPrice = 0;
                //player.LastRoundProfitPerc = 0;
                //player.ProfitLossChanges = null;
                //player.isSellAllowed = false;
                //player.HardSellPerc = 0;
                //player.isBuyOrderCompleted = false;
                //player.isBuyAllowed = true;
                //player.RepsTillCancelOrder = 0;
                //player.ForceSell = false;
                //player.BuyAtPrice = 0;
                //player.SellAtPrice = 0;
                _db.Player.Remove(player);
            }

            await _db.SaveChangesAsync();
            return Ok(firstPlayer);
        }

        [HttpPut]
        [Route("[action]/{playername}")]
        public async Task<IActionResult> RemovePlayer(string playername)
        {
            Player player = null;
            if (!string.IsNullOrEmpty(playername))
            {
                player = await _db.Player.FirstOrDefaultAsync(x => x.Name == playername);
                if (player != null)
                {
                    _db.Remove(player);
                    await _db.SaveChangesAsync();
                }
            }
            return Ok(player);
        }

        [HttpPut]
        [Route("[action]/{emtpyString}")]
        public async Task<IActionResult> AddPlayer(string emtpyString)
        {

            int maxNumber = 0;

            var players = await _db.Player.ToListAsync();

            foreach (var player in players)
            {
                int playerNumber = Convert.ToInt32(player.Name.Replace("DIA", ""));
                if (playerNumber > maxNumber) maxNumber = playerNumber;
            }
            var newPlayer = new Player();
            newPlayer.Name = "DIA" + (maxNumber + 1);
            newPlayer.Pair = null;
            newPlayer.BuyBelowPerc = players[0].BuyBelowPerc;
            newPlayer.SellBelowPerc = players[0].SellBelowPerc;
            newPlayer.SellAbovePerc = players[0].SellAbovePerc;
            newPlayer.LossSellBelow = players[0].LossSellBelow;
            newPlayer.BuyOrSell = string.Empty;
            newPlayer.DayHigh = 0;
            newPlayer.DayLow = 0;
            newPlayer.BuyCoinPrice = 0;
            newPlayer.Quantity = 0;
            newPlayer.BuyCommision = 0;
            newPlayer.ProfitLossAmt = 0;
            newPlayer.TotalSellAmount = 0;
            newPlayer.TotalBuyCost = 0;
            newPlayer.TotalCurrentValue = 0;
            newPlayer.SellCoinPrice = 0;
            newPlayer.AvailableAmountToBuy = 0;
            newPlayer.IsTrading = false;
            newPlayer.SellCommision = 0;
            newPlayer.BuyOrderId = 0;
            newPlayer.SellOrderId = 0;
            newPlayer.CurrentCoinPrice = 0;
            newPlayer.LastRoundProfitPerc = 0;
            newPlayer.ProfitLossChanges = null;
            newPlayer.isSellAllowed = true;
            newPlayer.HardSellPerc = 0;
            newPlayer.isBuyOrderCompleted = false;
            newPlayer.isBuyAllowed = true;
            newPlayer.RepsTillCancelOrder = 0;
            newPlayer.ForceSell = false;
            newPlayer.BuyAtPrice = 0;
            newPlayer.SellAtPrice = 0;
            newPlayer.IsTracked=true;

            await _db.Player.AddAsync(newPlayer);
            await _db.SaveChangesAsync();


            return Ok(newPlayer);
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
                dbPlayer.Pair = Player.Pair;
                dbPlayer.BuyAtPrice = Player.BuyAtPrice;
                dbPlayer.SellAtPrice = Player.SellAtPrice;
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
