using BudgetSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSystem.InMemory
{
    public class PAPRepository
    {
        ObjectCache cache = MemoryCache.Default;

        List<MFOPAP> PAPs;

        public PAPRepository()
        {
            PAPs = cache["pap"] as List<MFOPAP>;
            if (PAPs == null)
            {
                PAPs = new List<MFOPAP>();
            }
        }

        public void Commit()
        {
            cache["pap"] = PAPs;
        }

        public void Insert(MFOPAP p)
        {
            PAPs.Add(p);
        }

        public void Update(MFOPAP pap)
        {
            MFOPAP UpdatePAP = PAPs.Find(p => p.Id == pap.Id);

            if (UpdatePAP != null)
            {
                UpdatePAP = pap;
            }
            else
            {
                throw new Exception("P/A/P not found!");
            }
        }

        public MFOPAP Find(int Id)
        {
            MFOPAP pap = PAPs.Find(p => p.Id == Id);

            if (pap != null)
            {
                return pap;
            }
            else
            {
                throw new Exception("Responsibility Center not found!");
            }
        }

        public IQueryable<MFOPAP> Collection()
        {
            return PAPs.AsQueryable();
        }

        public void Delete(int Id)
        {
            MFOPAP DeletePAP = PAPs.Find(p => p.Id == Id);

            if (DeletePAP != null)
            {
                PAPs.Remove(DeletePAP);
            }
            else
            {
                throw new Exception("Responsibility Center not found!");
            }
        }
    }
}
