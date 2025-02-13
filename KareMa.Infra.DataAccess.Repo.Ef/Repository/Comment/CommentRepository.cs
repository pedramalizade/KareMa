using KareMa.Domain.Core.Entities;
using KareMa.Infra.SqlServer.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Infra.DataAccess.Repo.Ef.Repository
{
    public class CommentRepository : KareMa.Domain.Core.Contracts.Repositories.ICommentRepository
    {

        private readonly AppDbContext _context;
        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(CommentCreateDto commentCreateDto, CancellationToken cancellationToken)
        {
            var newModel = new Comment()
            {
                Title = commentCreateDto.Title,
                Description = commentCreateDto.Description,
                Score = commentCreateDto.Score,
                CustomerId = commentCreateDto.CustomerId,
                ExpertId = commentCreateDto.ExpertId,
            };
            await _context.Comments.AddAsync(newModel, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> Delete(int commentId, CancellationToken cancellationToken)
        {
            var targetModel = await FindComment(commentId, cancellationToken);
            targetModel.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<List<GetCommentsDto>> GetAll(CancellationToken cancellationToken)
        {
            var comments = await _context.Comments.AsNoTracking()
                 .Select(c => new GetCommentsDto
                 {
                     Id = c.Id,
                     Title = c.Title,
                     CustomerName = c.Customer.FirstName,
                     CustomerFamily = c.Customer.LastName,
                     CustomerId = c.CustomerId,
                     ExpertName = c.Expert.FirstName,
                     ExpertFamily = c.Expert.LastName,
                     ExpertId = c.ExpertId,
                     Description = c.Description,
                     IsActive = c.IsAccept,
                     IsDeleted = c.IsDeleted,
                     Score = c.Score

                 }).ToListAsync(cancellationToken);
            return comments;
        }


        public async Task<Comment> GetById(int commentId, CancellationToken cancellationToken)
       => await FindComment(commentId, cancellationToken);

        public async Task<bool> Update(CommentUpdateDto commentUpdateDto, CancellationToken cancellationToken)
        {
            var targetModel = await FindComment(commentUpdateDto.Id, cancellationToken);

            targetModel.Title = commentUpdateDto.Title;
            targetModel.Description = commentUpdateDto.Description;
            targetModel.Score = commentUpdateDto.Score;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
        private async Task<Comment> FindComment(int id, CancellationToken cancellationToken)
     => await _context.Comments.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }
}
