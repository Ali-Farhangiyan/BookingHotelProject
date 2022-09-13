using Application.Pagination;
using Application.Services.CommentServices.CommentFacadeService;
using Application.Services.CommentServices.ManagementCommentForAdmin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Pages.Comment
{
    public class IndexModel : PageModel
    {
        private readonly ICommentService commentService;

        public IndexModel(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        public PaginatedList<ShowCommentAdminDto> Comments { get; set; }
        public async Task OnGet(RequestShowAdminDto request)
        {
            Comments = await commentService.ManageComment.ShowComment(request);
        }
    }
}
