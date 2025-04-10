using Laundrygest_desktop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laundrygest_desktop.Data.Repositories
{
    public class CollectionRepository : BaseRepository
    {
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
    }
}
