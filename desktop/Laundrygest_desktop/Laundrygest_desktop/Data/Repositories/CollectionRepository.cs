using Laundrygest_desktop.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laundrygest_desktop.Data.Repositories
{
    public class CollectionRepository : BaseRepository
    {
        public async Task<ObservableCollection<Collection>> GetCollections(Client client)
        {
            ObservableCollection<Collection> c = null;
            try
            {
                c = await MakeRequest<ObservableCollection<Collection>>("collections/" + client.Code, "GET", null);
            }
            catch { }
            if(c == null)
            {
                c = new ObservableCollection<Collection>();
            }
            return c;
        }

        public async Task<ObservableCollection<Collection>> GetCollectionsInvoice(Client client)
        {
            ObservableCollection<Collection> c = null;
            try
            {
                c = await MakeRequest<ObservableCollection<Collection>>("collections/" + client.Code+"/invoices", "GET", null);
            }
            catch { }
            if (c == null)
            {
                c = new ObservableCollection<Collection>();
            }
            return c;
        }
        public async Task<Collection> PostCollection(Collection collection)
        {
            Collection c = null;
            try
            {
                c = await MakeRequest<Collection>("collections/", "POST", collection);
            }
            catch { }
            return c;
        }

        public async Task<bool> PutCollection(int collectionNumber,Collection collection)
        {
            try
            {
                await MakeRequest<Collection>("collections/" + collectionNumber, "PUT", collection);
                return true;
            }
            catch { 
                return false;
            }
        }
    }
}
