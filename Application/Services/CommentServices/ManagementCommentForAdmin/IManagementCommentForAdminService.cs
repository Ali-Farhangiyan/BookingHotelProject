using Application.ContextInterfaces;
using Application.Pagination;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CommentServices.ManagementCommentForAdmin
{
    public interface IManagementCommentForAdminService
    {
        Task<PaginatedList<ShowCommentAdminDto>> ShowComment(RequestShowAdminDto request);

        Task<CommentAdminDto> GetCommentById(int id);

        Task<bool> ChangeStautsComment(PostCommentDto postComment);
    }

    public class ManagementCommentForAdminService : IManagementCommentForAdminService
    {
        private readonly IDatabaseContext db;

        public ManagementCommentForAdminService(IDatabaseContext db)
        {
            this.db = db;
        }

        public async Task<PaginatedList<ShowCommentAdminDto>> ShowComment(RequestShowAdminDto request)
        {
            var query = db.Comments
                .Include(c => c.Hotel)
                .ThenInclude(h => h.Rooms)
                .AsQueryable();
            query = ApplyRequestComment(request, query);

            var comments = await query.Select(c => new ShowCommentAdminDto
            {
                HotelId = c.HotelId,
                HotelName = c.Hotel.Name,
                Id = c.Id,
                Status = c.StatusComment.ToString(),
                RoomName = c.Hotel.Rooms.FirstOrDefault(r => r.HotelId == c.HotelId).Name
            }).ToListAsync();

            return PaginatedList<ShowCommentAdminDto>.Create(comments, request.PageSize, request.PageIndex);

        }

        public async Task<CommentAdminDto> GetCommentById(int id)
        {
            var comment = await db.Comments
                .Include(c => c.Hotel)
                .ThenInclude(h => h.Rooms)
                .Where(c => c.Id == id)
                .Select(c => new CommentAdminDto
                {
                    Body = c.Strength + " " + c.WeakPoints,
                    HotelName = c.Hotel.Name,
                    UserName = c.UserName
                }).FirstOrDefaultAsync();

            return comment;
        }

        public async Task<bool> ChangeStautsComment(PostCommentDto postComment)
        {
            var comment = await db.Comments.FirstOrDefaultAsync(c => c.Id == postComment.Id);

            comment.ChangeStatusComment(postComment.StatusComment);
            var result = await db.SaveChangesAsync();
            if (result > 0) return true;

            return false;
        }

        private static IQueryable<Comment> ApplyRequestComment(RequestShowAdminDto request, IQueryable<Comment> query)
        {
            if (request.SortComments == SortComments.Newest)
            {
                query = query.OrderBy(c => c.Id);
            }

            if (request.SortComments == SortComments.Oldest)
            {
                query = query.OrderByDescending(c => c.Id);
            }

            if (request.SortComments == SortComments.Waiting)
            {
                query = query.Where(c => c.StatusComment == Domain.Entites.StatusComment.Waiting);
            }

            if (request.SortComments == SortComments.Rejected)
            {
                query = query.Where(c => c.StatusComment == Domain.Entites.StatusComment.Rejected);

            }

            if (request.SortComments == SortComments.Accepted)
            {
                query = query.Where(c => c.StatusComment == Domain.Entites.StatusComment.Accepted);

            }

            return query;
        }

        
    }
    public class CommentAdminDto
    {
        public string Body { get; set; }

        public string UserName { get; set; }

        public string HotelName { get; set; }
    }
    public class ShowCommentAdminDto
    {
        public int Id { get; set; } 

        public int HotelId { get; set; }

        public string HotelName { get; set; } = null!;

        public string RoomName { get; set; } = null!;

        public string Status { get; set; } = null!;
    }

    public class RequestShowAdminDto
    {
        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 2;

        public SortComments SortComments { get; set; } = SortComments.Newest;
    }

    public class PostCommentDto
    {
        public int Id { get; set; }

        public StatusComment StatusComment { get; set; }
    }

    public enum SortComments
    {
        Newest = 1,
        Oldest = 2,
        Waiting = 3, 
        Accepted = 4,
        Rejected = 5
    }
}
