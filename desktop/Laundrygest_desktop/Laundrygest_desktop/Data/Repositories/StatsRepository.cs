#nullable enable
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laundrygest_desktop.Model;

namespace Laundrygest_desktop.Data.Repositories
{
    public class StatsRepository : BaseRepository
    {

        public async Task<ObservableCollection<MonthlyStatsDTO>> GetMonthlyStats(DateTime fromDate, DateTime toDate)
        {
            ObservableCollection<MonthlyStatsDTO>? result = null;
            
            try
            {
                result = await MakeRequest<ObservableCollection<MonthlyStatsDTO>>(
                    $"stats/monthlyStats?dateFrom={fromDate:O}&dateTo={toDate:O}", "GET", null);
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }

            if (result != null) return result;
            return new ObservableCollection<MonthlyStatsDTO>();
        }

        public async Task<ObservableCollection<PricelistStatsDTO>> GetPricelistStats(DateTime fromDate, DateTime toDate)
        {
            ObservableCollection<PricelistStatsDTO>? result = null;
            try
            {
                result = await MakeRequest<ObservableCollection<PricelistStatsDTO>>(
                    $"stats/pricelistStats?dateFrom={fromDate:O}&dateTo={toDate:O}", "GET", null);
            }catch{}

            if (result != null) return result;
            return new ObservableCollection<PricelistStatsDTO>();
        }
    }
}
