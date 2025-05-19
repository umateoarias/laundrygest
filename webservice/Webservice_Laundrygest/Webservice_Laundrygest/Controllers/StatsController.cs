using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webservice_Laundrygest.Models;

namespace Webservice_Laundrygest.Controllers
{

    public class StatsController : ControllerBase
    {
        private readonly LaundrygestContext _context;

        public StatsController(LaundrygestContext context)
        {
            _context = context;
        }

        // GET: api/stats/pricelistStats
        [Route("api/stats/pricelistStats")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PricelistStatsDTO>>> GetPricelistsStats([FromQuery] DateTime dateFrom,[FromQuery] DateTime dateTo)
        {
            var stats = await _context.Pricelists
                .Select(x => new PricelistStatsDTO
                {
                    namePricelist = x.Name, 
                    numPieces = x.CollectionItems.Sum(z => z.NumPieces), 
                    totalAmount = x.CollectionItems.Sum(z => z.NumPieces) * x.NumPieces.Value
                }).Where(x=>x.numPieces>0).OrderBy(x=>x.numPieces).ThenBy(x=>x.namePricelist).ToListAsync();

            if (stats == null)
            {
                return NotFound();
            }
            return stats;
        }

        // GET: api/stats/monthlyStats
        [Route("api/stats/monthlyStats")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MonthlyStatsDTO>>> GetMonthlyStats([FromQuery] DateTime dateFrom, [FromQuery] DateTime dateTo)
        {
            var rawStats = await _context.Collections
                .Where(x => x.CreatedAt >= dateFrom && x.CreatedAt <= dateTo)
                .GroupBy(x => new { x.CreatedAt.Year, x.CreatedAt.Month })
                .OrderBy(g => g.Key.Year)
                .ThenBy(g => g.Key.Month)
                .Select(g => new 
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalAmount = g.Sum(z => z.Total.Value),
                    TaxAmount = g.Sum(z => z.TaxAmount.Value),
                    BaseAmount = g.Sum(z => z.TaxBase.Value)
                }).ToListAsync();

            var stats = rawStats.Select(g => new MonthlyStatsDTO
            {
                dateName = GetMonthName(g.Month, g.Year),
                totalAmount = g.TotalAmount,
                taxAmount = g.TaxAmount,
                baseAmount = g.BaseAmount
            }).ToList();
            if (stats == null) { return NotFound(); }
            return stats;
        }

        public string GetMonthName(int month, int year)
        {

            string[] months = {
        /*0*/ "",
        /*1*/ "Gener",
        /*2*/ "Febrer",
        /*3*/ "Març",
        /*4*/ "Abril",
        /*5*/ "Maig",
        /*6*/ "Juny",
        /*7*/ "Juliol",
        /*8*/ "Agost",
        /*9*/ "Septembre",
        /*10*/ "Octubre",
        /*11*/ "Novembre",
        /*12*/ "Desembre"
    };

            if (month < 1 || month > 12)
                throw new ArgumentOutOfRangeException(nameof(month), "Debe estar entre 1 y 12.");

            return months[month] + " " + year.ToString();

        }

    }
}
