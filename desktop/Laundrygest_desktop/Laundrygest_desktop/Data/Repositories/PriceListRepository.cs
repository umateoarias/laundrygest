using Laundrygest_desktop.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laundrygest_desktop.Data
{
    public class PriceListRepository : BaseRepository
    {

        public async Task<ObservableCollection<Pricelist>> GetPricelists(int collectionType)
        {
            ObservableCollection<Pricelist> result = null;
            try
            {
                result = await MakeRequest<ObservableCollection<Pricelist>>("pricelists/" + collectionType, "GET", null);
            }
            catch { }
            if (result == null) {
                result = new ObservableCollection<Pricelist>();
            }
            return result;
        }

        public async Task<bool> PutPricelist(int pricelist_code, Pricelist pricelist)
        {
            Pricelist pl = null;
            try
            {
                pl = await MakeRequest<Pricelist>("pricelists/" + pricelist_code, "PUT", pricelist);
                return true;
            }
            catch {
                return false;
            }
        }

        public async Task<Pricelist> PostPricelist(Pricelist pricelist)
        {
            Pricelist pl = null;
            try
            {
                pl = await MakeRequest<Pricelist>("pricelists", "POST", pricelist);
            }
            catch { }
            return pl;
        }

        public async Task<bool> DeletePricelist(int pricelist_code)
        {
            try
            {
                var x = await MakeRequest<Pricelist>("pricelists/"+pricelist_code, "DELETE",null);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
