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
    public class AdminAppServices : IAdminAppServices
    {
        private readonly IAdminServices _adminServices;

        public AdminAppServices(IAdminServices adminServices)
        {
            _adminServices = adminServices;
        }
        public async Task<bool> Create(AdminCreateDto adminCreateDto, CancellationToken cancellationToken)
         => await _adminServices.Create(adminCreateDto, cancellationToken);
        public async Task<bool> Delete(int adminId, CancellationToken cancellationToken)
         => await _adminServices.Delete(adminId, cancellationToken);
        public async Task<List<Admin>> GetAll(CancellationToken cancellationToken)
       => await _adminServices.GetAll(cancellationToken);
        public async Task<Admin> GetById(int adminId, CancellationToken cancellationToken)
   => await _adminServices.GetById(adminId, cancellationToken);
        public async Task<bool> Update(AdminUpdateDto adminUpdateDto, CancellationToken cancellationToken)
          => await _adminServices.Update(adminUpdateDto, cancellationToken);
    }
}
