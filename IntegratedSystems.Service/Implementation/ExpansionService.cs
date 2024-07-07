using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Repository.Interface;
using IntegratedSystems.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Service.Implementation
{
    public class ExpansionService : IExpansionService
    {
        private readonly IRepository<Expansion> _expansionRepository;

        public ExpansionService(IRepository<Expansion> expansionRepository)
        {
            _expansionRepository = expansionRepository;
        }

        public void CreateNewExpansion(Expansion p)
        {
            _expansionRepository.Insert(p);
        }

        public void DeleteExpansion(Guid id)
        {
            Expansion expansion = _expansionRepository.Get(id);
            _expansionRepository.Delete(expansion);
        }

        public List<Expansion> GetAllExpansion()
        {
            return _expansionRepository.GetAll().ToList();
        }

        public Expansion GetDetailsForExpansion(Guid? id)
        {
            return _expansionRepository.Get(id);
        }

        public void UpdateExistingExpansion(Expansion p)
        {
            _expansionRepository.Update(p);
        }
    }
}
