using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Core.Contracts.Repositories.Transaction
{
    public interface ITransactionRepository
    {
        Task<bool> TransactionBalanceAsync(int customerId, int expertId, decimal amount, CancellationToken cancellationToken);
    }
}
