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
    public class SignalController : ControllerBase
    {

        private readonly ILogger<SignalController> _logger;
        private readonly DB _db;

        public SignalController(ILogger<SignalController> logger, DB db)
        {
            _logger = logger;
            _db = db;
        }




        public async Task<List<SignalCandle>> AllSignal()
        {
            return await _db.SignalCandle.AsNoTracking().OrderByDescending(x => x.CloseTime).ToListAsync();
        }

        public async Task<List<string>> AllPair()
        {
            return await _db.MyCoins.Where(x=>x.IsIncludedForTrading==true).OrderBy(x=>x.Pair).Select(x=>x.Pair).ToListAsync();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await AllPair());
        }

        [HttpGet("{pair}")]
        public async Task<IActionResult> GetSignals(string pair)
        {
            return Ok(await _db.SignalCandle.Where(x => x.Pair == pair).ToListAsync());
        }

    }
}
