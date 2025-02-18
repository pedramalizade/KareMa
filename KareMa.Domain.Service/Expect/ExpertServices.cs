using KareMa.Domain.Core.Contracts.Repositories;
using KareMa.Domain.Core.Contracts.Service;
using KareMa.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Service.Expect
{
    public class ExpertServices : IExpertServices
    {
        private readonly IExpertRepository _expertRepository;
        public ExpertServices(IExpertRepository expertRepository)
        {
            _expertRepository = expertRepository;
        }
        public async Task<bool> Create(ExpertCreateDto expertCreateDto, CancellationToken cancellationToken)
          => await _expertRepository.Create(expertCreateDto, cancellationToken);
        public async Task<bool> Delete(int expertId, CancellationToken cancellationToken)
          => await _expertRepository.Delete(expertId, cancellationToken);
        public async Task<int> ExpertCount(CancellationToken cancellationToken)
        {
            var c = await _expertRepository.ExpertCount(cancellationToken);
            return c;
        }
        public async Task<List<Expert>> GetAll(CancellationToken cancellationToken)
          => await _expertRepository.GetAll(cancellationToken);
        public async Task<Expert> GetById(int expertId, CancellationToken cancellationToken)
          => await _expertRepository.GetById(expertId, cancellationToken);
        public async Task<bool> Update(ExpertUpdateDto expertUpdateDto, CancellationToken cancellationToken)
          => await _expertRepository.Update(expertUpdateDto, cancellationToken);
    }
}
