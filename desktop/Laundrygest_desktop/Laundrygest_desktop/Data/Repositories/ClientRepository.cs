using Laundrygest_desktop.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laundrygest_desktop.Data
{
    public class ClientRepository : BaseRepository
    {
        public async Task<ObservableCollection<Client>> GetClients()
        {
            ObservableCollection<Client> result = null;
            try
            {
                result = await MakeRequest<ObservableCollection<Client>>("clients", "GET", null);
            }
            catch { }
            if (result == null)
            {
                result = new ObservableCollection<Client>();
            }
            return result;
        }

        public async Task<ObservableCollection<Client>> GetClients(string filter)
        {
            ObservableCollection<Client> result = null;
            try
            {
                result = await MakeRequest<ObservableCollection<Client>>("clients/"+filter, "GET", null);
            }
            catch { }
            if (result == null)
            {
                result = new ObservableCollection<Client>();
            }
            return result;
        }

        public async Task<Client?> PostClient(Client client)
        {
            Client c = null;
            try
            {
                c = await MakeRequest<Client>("client/", "POST", client);
            }
            catch { }
            return c;
        }
    }
}
