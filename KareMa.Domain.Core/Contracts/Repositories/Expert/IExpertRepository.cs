using KareMa.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Core.Contracts.Repositories
{
    public interface IExpertRepository
    {
        Task<bool> Create(ExpertCreateDto expertCreateDto, CancellationToken cancellationToken);
        Task<bool> Update(ExpertUpdateDto expertUpdateDto, CancellationToken cancellationToken);
        Task<bool> Delete(int expertId, CancellationToken cancellationToken);
        Task<Expert> GetById(int expertId, CancellationToken cancellationToken);
        Task<List<Expert>> GetAll(CancellationToken cancellationToken);
    }

}
