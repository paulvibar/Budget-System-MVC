using BudgetSystem.Core.Models;
using BudgetSystem.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSystem.InMemory
{
    public class ORSDetailsRepository : ORSDetailsInformation, IORSDetailsRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ORSDetailsInformation> items;

        public ORSDetailsRepository()
        {
            items = cache["orsdetails"] as List<ORSDetailsInformation>;

            if (items == null)
            {
                items = new List<ORSDetailsInformation>();
            }
        }
        public ORSDetailsInformation Find(int ORSId)
        {
            ORSDetailsInformation ORSDetails = items.Find(i => i.ORSId == ORSId);
            if (ORSDetails != null)
            {
                return ORSDetails;
            }
            else
            {
                ORSDetails = new ORSDetailsInformation();
                return ORSDetails;
            }
        }

        public IQueryable<ORSDetailsInformation> Collection()
        {
            return items.AsQueryable();
        }
    }
}
