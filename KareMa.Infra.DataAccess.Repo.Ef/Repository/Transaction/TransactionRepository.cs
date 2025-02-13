using KareMa.Domain.Core.Contracts.Repositories.Transaction;
using KareMa.Infra.SqlServer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Infra.DataAccess.Repo.Ef.Repository.Transaction
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;
        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> TransactionBalanceAsync(int customerId, int expertId, decimal amount, CancellationToken cancellationToken)
        {
            using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var customer = await _context.Customers.FindAsync(customerId);
                var expert = await _context.Experts.FindAsync(expertId);

                if (customer == null || expert == null)
                {
                    throw new Exception("مشتری یا متخصص پیدا نشد. ");
                }
                if (customer.Balance < amount)
                {
                    throw new Exception("موجودی ناکافی است");
                }
                customer.Balance -= amount;
                _context.Customers.Update(customer);

                expert.Balance += amount;
                _context.Experts.Update(expert);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }
    }
}
