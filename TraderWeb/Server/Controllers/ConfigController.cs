using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TraderWeb.Server.Data;
using TraderWeb.Shared;



namespace TraderWeb.Server.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ConfigrController : ControllerBase
    {


        private readonly ILogger<ConfigrController> _logger;
        private readonly DB _db;

        public ConfigrController(ILogger<ConfigrController> logger,DB db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<List<Config>> AllConfig()
        {
            return await _db.Config.ToListAsync();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await AllConfig());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleConfig(int id)
        {
            return Ok(await _db.Config.FirstOrDefaultAsync(x => x.id == id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConfig(Config config, int id)
        {
            var dbconfig= await _db.Config.FirstOrDefaultAsync(x => x.id == id);

            dbconfig.IsBuyingAllowed = config.IsBuyingAllowed;
            dbconfig.IsSellingAllowed = config.IsSellingAllowed;
            dbconfig.IsReducingSellAbvAllowed = config.IsReducingSellAbvAllowed;

            dbconfig.ShowBuyLogs = config.ShowBuyLogs;
            dbconfig.ShowNoBuyLogs = config.ShowNoBuyLogs;
            dbconfig.ShowSellLogs = config.ShowSellLogs;


            dbconfig.ShowNoSellLogs = config.ShowNoSellLogs;

            dbconfig.ShowScalpBuyLogs = config.ShowScalpBuyLogs;
            dbconfig.ShowNoScalpBuyLogs = config.ShowNoScalpBuyLogs;
            dbconfig.UpdateCoins = config.UpdateCoins;

            dbconfig.MaxRepsBeforeCancelOrder = config.MaxRepsBeforeCancelOrder; 
            dbconfig.DayHighGreaterthanToSell = config.DayHighGreaterthanToSell;
            dbconfig.DayHighLessthanToSell = config.DayHighLessthanToSell;
            dbconfig.DayLowGreaterthanTobuy = config.DayLowGreaterthanTobuy;
            dbconfig.DayLowLessthanTobuy = config.DayLowLessthanTobuy;

            dbconfig.ScalpFiveMinDownMoreThan = config.ScalpFiveMinDownMoreThan;
            dbconfig.ScalpFifteenMinDownMoreThan = config.ScalpFifteenMinDownMoreThan;
            dbconfig.ScalpThirtyMinDownMoreThan = config.ScalpThirtyMinDownMoreThan;
            dbconfig.ScalpOneHourDownMoreThan = config.ScalpOneHourDownMoreThan;
            dbconfig.ScalpFourHourDownMoreThan = config.ScalpFourHourDownMoreThan;

            dbconfig.ScalpFiveMinDiffLessThan = config.ScalpFiveMinDiffLessThan;
            dbconfig.ScalpFifteenMinDiffLessThan = config.ScalpFifteenMinDiffLessThan;
            dbconfig.ScalpThirtyMinDiffLessThan = config.ScalpThirtyMinDiffLessThan;
            dbconfig.ScalpOneHourDiffLessThan = config.ScalpOneHourDiffLessThan;
            dbconfig.ScalpFourHourDiffLessThan = config.ScalpFourHourDiffLessThan;

            dbconfig.DefaultSellAbovePerc = config.DefaultSellAbovePerc;
            dbconfig.MinAllowedTradeCount = config.MinAllowedTradeCount;
            dbconfig.ReduceSellAboveBy = config.ReduceSellAboveBy;
            dbconfig.ReducePriceDiffPercBy = config.ReducePriceDiffPercBy;
            dbconfig.IntervalMinutes=config.IntervalMinutes;
            dbconfig.ShouldSellWhenAllBotsAtLoss=config.ShouldSellWhenAllBotsAtLoss;
            dbconfig.SellWhenAllBotsAtLossBelow=config.SellWhenAllBotsAtLossBelow;
            dbconfig.CrashSell=config.CrashSell;

            _db.Update(dbconfig);
            await _db.SaveChangesAsync();
            return Ok(await AllConfig());
        }
    }
}
