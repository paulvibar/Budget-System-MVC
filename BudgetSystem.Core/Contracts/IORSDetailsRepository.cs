using BudgetSystem.Core.Models;
using System.Linq;

namespace BudgetSystem.Core.Contracts
{
    public interface IORSDetailsRepository
    {
        ORSDetailsInformation Find(int ORSId);
        IQueryable<ORSDetailsInformation> Collection();
    }
}