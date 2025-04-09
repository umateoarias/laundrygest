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
    }
}
