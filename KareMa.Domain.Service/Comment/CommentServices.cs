using KareMa.Domain.Core.Contracts.Repositories;
using KareMa.Domain.Core.Contracts.Service;
using KareMa.Domain.Core.DTOs.CommentDTO;
using KareMa.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Service
{
    public class CommentServices : ICommentServices
    {
        private readonly ICommentRepository _commentRepository;
        public CommentServices(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public async Task AcceptComment(int commentId, CancellationToken cancellationToken)
  => await _commentRepository.AcceptComment(commentId, cancellationToken);
        public async Task<int> CommentCount(CancellationToken cancellationToken)
         => await _commentRepository.CommentCount(cancellationToken);
        public async Task<bool> Create(CommentCreateDto commentCreateDto, CancellationToken cancellationToken)
     => await _commentRepository.Create(commentCreateDto, cancellationToken);
        public async Task<bool> Delete(int CommentId, CancellationToken cancellationToken)
  => await _commentRepository.Delete(CommentId, cancellationToken);
        public async Task<List<GetCommentsDto>> GetAll(CancellationToken cancellationToken)
         => await _commentRepository.GetAll(cancellationToken);
        public async Task<Comment> GetById(int commentId, CancellationToken cancellationToken)
     => await _commentRepository.GetById(commentId, cancellationToken);
        public async Task<List<RecentCommentDto>> GetRecentComments(int count, CancellationToken cancellationToken)
          => await _commentRepository.GetRecentComments(count, cancellationToken);
        public async Task RejectComment(int commentId, CancellationToken cancellationToken)
          => await _commentRepository.RejectComment(commentId, cancellationToken);
        public async Task<bool> SetScore(int expertId, int score, CancellationToken cancellationToken)
          => await _commentRepository.SetScore(expertId, score, cancellationToken);
        public async Task<bool> Update(CommentUpdateDto commentUpdateDto, CancellationToken cancellationToken)
          => await _commentRepository.Update(commentUpdateDto, cancellationToken);
    }
}
