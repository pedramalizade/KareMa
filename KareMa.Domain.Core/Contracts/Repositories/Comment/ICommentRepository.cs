using KareMa.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Core.Contracts.Repositories
{
    public interface ICommentRepository
    {
        Task<bool> Create(CommentCreateDto commentCreateDto, CancellationToken cancellationToken);
        Task<bool> Update(CommentUpdateDto commentUpdateDto, CancellationToken cancellationToken);
        Task<bool> Delete(int CommentId, CancellationToken cancellationToken);
        Task<Comment> GetById(int commentId, CancellationToken cancellationToken);
        Task<List<GetCommentsDto>> GetAll(CancellationToken cancellationToken);

    }
}
