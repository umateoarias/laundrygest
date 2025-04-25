using Laundrygest_desktop.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laundrygest_desktop.Data.Repositories
{
    public class DeliveryRepository : BaseRepository
    {
        public async Task<Delivery> PostDelivery(Delivery delivery)
        {
            Delivery d = null;
            try
            {
                d = await MakeRequest<Delivery>("delivery/", "POST", delivery);
            }
            catch { }
            return d;
        }

        public async Task<bool> PutDelivery(int number, Delivery delivery)
        {
            try
            {
                await MakeRequest<Delivery>("delivery/"+number, "PUT", delivery);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
