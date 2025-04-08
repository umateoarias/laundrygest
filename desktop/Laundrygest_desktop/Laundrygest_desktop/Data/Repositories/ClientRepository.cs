using Laundrygest_desktop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laundrygest_desktop.Data
{
    public class ClientRepository : BaseRepository
    {
        public async Task<List<Client>> GetClients()
        {
            List<Client> result = null;
            try
            {
                result = await MakeRequest<List<Client>>("Clients", "GET", null);
                result = result.OrderBy(x => x.FirstName).ThenBy(x => x.LastName).ThenBy(x => x.Telephone).ToList();
            }
            catch { }
            if (result == null)
            {
                result = new List<Client>();
            }
            return result;
        }

        public async Task<List<Client>> GetClients(string filter)
        {
            List<Client> result = null;
            try
            {
                result = await MakeRequest<List<Client>>("Clients/"+filter, "GET", null);
                result = result.OrderBy(x => x.FirstName).ThenBy(x => x.LastName).ThenBy(x => x.Telephone).ToList();
            }
            catch { }
            if (result == null)
            {
                result = new List<Client>();
            }
            return result;
        }

        public async Task<Client?> PostClient(Client client)
        {
            Client c = null;
            try
            {
                c = await MakeRequest<Client>("Client/", "POST", client);
            }
            catch { }
            return c;
        }
    }
}
