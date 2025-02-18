using KareMa.Domain.Core.Contracts.AppService;
using KareMa.Domain.Core.Contracts.Service;
using KareMa.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.AppService
{
    public class ExpertAppServices : IExpertAppServices
    {
        private readonly IExpertServices _expertServices;
        public ExpertAppServices(IExpertServices expertServices)
        {
            _expertServices = expertServices;
        }
        public async Task<bool> Create(ExpertCreateDto expertCreateDto, CancellationToken cancellationToken)
          => await _expertServices.Create(expertCreateDto, cancellationToken);
        public async Task<bool> Delete(int expertId, CancellationToken cancellationToken)
          => await _expertServices.Delete(expertId, cancellationToken);
        public async Task<int> ExpertCount(CancellationToken cancellationToken)
        {
            var c = await _expertServices.ExpertCount(cancellationToken);
            return c;
        }
        public async Task<List<Expert>> GetAll(CancellationToken cancellationToken)
           => await _expertServices.GetAll(cancellationToken);
        public async Task<Expert> GetById(int expertId, CancellationToken cancellationToken)
           => await _expertServices.GetById(expertId, cancellationToken);
        public async Task<bool> Update(ExpertUpdateDto expertUpdateDto, CancellationToken cancellationToken)
          => await _expertServices.Update(expertUpdateDto, cancellationToken);
    }
}
