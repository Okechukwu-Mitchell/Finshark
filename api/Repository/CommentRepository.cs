using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateAsync(Comment commentModel)
        {
             await _context.Comments.AddAsync(commentModel);
             await _context.SaveChangesAsync();
             return commentModel;
        }
		public async Task<List<Comment>> GetAllAsync(CommentQueryObject queryObject)
        {
            var comments = _context.Comments.Include(a => a.AppUser).AsQueryable();

            if(string.IsNullOrWhiteSpace(queryObject.Symbol))
            {
                comments = comments.Where(a => a.Stock.Symbol == queryObject.Symbol);
            };
            if (queryObject.IsDescending == true)
            {
                comments = comments.OrderByDescending(a => a.CreatedOn);
            }

            return await comments.ToListAsync();
        }
        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task<Comment?> UpdateAsync(int id, Comment commentModel)
        {
            var existingComment = await _context.Comments.Include(a => a.AppUser).FirstOrDefaultAsync(c => c.Id == id);

            if (existingComment == null)
            {
                return null;
            }
            existingComment.Title = commentModel.Title;
            existingComment.Content = commentModel.Content;

            await _context.SaveChangesAsync();
            return existingComment;
        }

		public async Task<Comment?> DeleteAsync(int id)
		{
			var commentModel = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
			if (commentModel == null)
			{
				return null;
			}
			_context.Comments.Remove(commentModel);
			await _context.SaveChangesAsync();
			return commentModel;
		}

		public Task<bool> CommentExists(int id)
		{
			return _context.Comments.AnyAsync(s => s.Id == id);
		}
	}
}