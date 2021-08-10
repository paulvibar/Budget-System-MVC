using BudgetSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSystem.InMemory
{
    public class RCRepository
    {
        ObjectCache cache = MemoryCache.Default;

        List<ResponsibilityCenter> RCs;

        public RCRepository()
        {
            RCs = cache["rc"] as List<ResponsibilityCenter>;
            if(RCs == null)
            {
                RCs = new List<ResponsibilityCenter>();
            }
        }

        public void Commit()
        {
            cache["rc"] = RCs;
        }

        public void Insert(ResponsibilityCenter r)
        {
            RCs.Add(r);
        }

        public void Update(ResponsibilityCenter responsibilitycenter)
        {
            ResponsibilityCenter rcToUpdate = RCs.Find(r => r.Id == responsibilitycenter.Id);

            if(rcToUpdate != null)
            {
                rcToUpdate = responsibilitycenter;
            }
            else
            {
                throw new Exception("Responsibility Center not found!");
            }
        }

        public ResponsibilityCenter Find(int Id)
        {
            ResponsibilityCenter rc = RCs.Find(r => r.Id == Id);

            if (rc != null)
            {
                return rc;
            }
            else
            {
                throw new Exception("Responsibility Center not found!");
            }
        }

        public IQueryable<ResponsibilityCenter> Collection()
        {
            return RCs.AsQueryable();
        }

        public void Delete(int Id)
        {
            ResponsibilityCenter rcToDelete = RCs.Find(r => r.Id == Id);

            if (rcToDelete != null)
            {
                RCs.Remove(rcToDelete);
            }
            else
            {
                throw new Exception("Responsibility Center not found!");
            }
        }
    }
}
