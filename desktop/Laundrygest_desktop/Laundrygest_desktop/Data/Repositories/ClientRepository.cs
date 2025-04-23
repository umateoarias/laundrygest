using Laundrygest_desktop.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable enable
namespace Laundrygest_desktop.Data
{
    public class ClientRepository : BaseRepository
    {
        public async Task<ObservableCollection<Client>> GetClients()
        {
            ObservableCollection<Client>? result = null;
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
            ObservableCollection<Client>? result = null;
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
            Client? c = null;
            try
            {
                c = await MakeRequest<Client>("client/", "POST", client);
            }
            catch { }
            return c;
        }

        public async Task<bool> PutClient(int client_code,Client client)
        {
            try
            {
                await MakeRequest<Client>("client/" + client_code, "PUT", client);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteClient(int client_code)
        {
            try
            {
                await MakeRequest<Client>("client/" + client_code, "DELETE", null);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
