using IntegratedSystems.Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Service.Interface
{
    public interface IExpansionService
    {
        List<Expansion> GetAllExpansion();
        Expansion GetDetailsForExpansion(Guid? id);
        void CreateNewExpansion(Expansion p);
        void UpdateExistingExpansion(Expansion p);
        void DeleteExpansion(Guid id);
    }
}
