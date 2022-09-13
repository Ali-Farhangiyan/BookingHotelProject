using Application.ContextInterfaces;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CommentServices.AddComment
{
    public interface IAddCommentService
    {
        Task<bool> ExecuteAsync(AddCommentDto comment);

    }

    public class AddCommentService : IAddCommentService
    {
        private readonly IDatabaseContext db;

        public AddCommentService(IDatabaseContext db)
        {
            this.db = db;
        }

        public async Task<bool> ExecuteAsync(AddCommentDto comment)
        {
            var newComment = new Comment(comment.HotelId, comment.UserId, comment.Strength, comment.WeakPoints, comment.UserName, comment.AverageRateUser);

            await db.Comments.AddAsync(newComment);
            var rateAverage = await db.Comments.Include(c => c.Hotel).Where(c => c.HotelId == comment.HotelId).AverageAsync(c => c.AverageRateUser);
            var hotel = await db.Hotels.FirstOrDefaultAsync(c => c.Id == comment.HotelId);

            hotel.SetRate(rateAverage);
            var result = await db.SaveChangesAsync();

            
            if (result > 0) return true;
            return false;
        }
    }

    public class AddCommentDto
    {
        public string Strength { get;  set; } = null!;

        public string WeakPoints { get;  set; } = null!;

        public string UserId { get;  set; } = null!;
        public string UserName { get;  set; } = null!;

        public int HotelId { get;  set; }

        public double AverageRateUser { get; set; }
    }
}
