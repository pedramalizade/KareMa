using KareMa.Domain.Core.Contracts.Repositories;
using KareMa.Domain.Core.DTOs.CommentDTO;
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
    public class CommentRepository : ICommentRepository
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
        public async Task<bool> SetScore(int expertId, int score, CancellationToken cancellationToken)
        {
            var targetModel = await _context.Comments.FirstOrDefaultAsync(c => c.ExpertId == expertId, cancellationToken);
            targetModel.Score = score;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        public async Task AcceptComment(int commentId, CancellationToken cancellationToken)
        {
            var targetModel = await FindComment(commentId, cancellationToken);
            targetModel.IsAccept = true;
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task RejectComment(int commentId, CancellationToken cancellationToken)
        {
            var targetModel = await FindComment(commentId, cancellationToken);
            targetModel.IsAccept = false;
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<List<RecentCommentDto>> GetRecentComments(int count, CancellationToken cancellationToken)
        {
            var recentComments = await _context.Comments.
                Select(c => new RecentCommentDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    Score = c.Score,
                    Expert = c.Expert,
                    CreateAt = c.CreatedAt,
                })
                .OrderByDescending(c => c.CreateAt)
                .Take(count)
                .ToListAsync(cancellationToken);
            return recentComments;
        }
        public async Task<int> CommentCount(CancellationToken cancellationToken)
          => await _context.Comments.CountAsync(cancellationToken);

        private async Task<Comment> FindComment(int id, CancellationToken cancellationToken)
     => await _context.Comments.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }
}
