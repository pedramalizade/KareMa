using KareMa.Domain.Core.Contracts.AppService;
using KareMa.Domain.Core.Contracts.Service;
using KareMa.Domain.Core.DTOs.CommentDTO;
using KareMa.Domain.Core.Entities;
using KareMa.Domain.Core.Entities.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.AppService
{
    public class CommentAppServices : ICommentAppServices
    {
        private readonly ICommentServices _commentServices;
        //private readonly SiteSettings _siteSettings;
        public CommentAppServices(ICommentServices commentServices)
        {
            _commentServices = commentServices;
            //_siteSettings = siteSettings;
        }
        public async Task AcceptComment(int commentId, CancellationToken cancellationToken)
           => await _commentServices.AcceptComment(commentId, cancellationToken);
        public async Task<bool> Create(CommentCreateDto commentCreateDto, CancellationToken cancellationToken)
  => await _commentServices.Create(commentCreateDto, cancellationToken);
        public async Task<bool> Delete(int CommentId, CancellationToken cancellationToken)
        => await _commentServices.Delete(CommentId, cancellationToken);
        public async Task<List<GetCommentsDto>> GetAll(CancellationToken cancellationToken)
       => await _commentServices.GetAll(cancellationToken);
        public async Task<Comment> GetById(int commentId, CancellationToken cancellationToken)
          => await _commentServices.GetById(commentId, cancellationToken);
        public async Task<bool> SetScore(int expertId, int score, CancellationToken cancellationToken)
      => await _commentServices.SetScore(expertId, score, cancellationToken);
        public async Task<bool> Update(CommentUpdateDto commentUpdateDto, CancellationToken cancellationToken)
          => await _commentServices.Update(commentUpdateDto, cancellationToken);
        public async Task<int> CommentCount(CancellationToken cancellationToken)
          => await _commentServices.CommentCount(cancellationToken);
        //public async Task<List<RecentCommentDto>> GetRecentComments(CancellationToken cancellationToken)
        //{
        //    var resentCount = _siteSettings.CommentConfiguration.RecentCount;
        //    return await _commentServices.GetRecentComments(resentCount, cancellationToken);
        //}
        public async Task RejectComment(int commentId, CancellationToken cancellationToken)
         => await _commentServices.RejectComment(commentId, cancellationToken);
    }
}
