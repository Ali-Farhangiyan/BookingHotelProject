using Application.Services.CommentServices.CommentFacadeService;
using Application.Services.CommentServices.ManagementCommentForAdmin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Pages.Comment
{
    public class EditCommentModel : PageModel
    {
        private readonly ICommentService commentService;

        public EditCommentModel(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        public CommentAdminDto Comment { get; set; }

        [BindProperty]

        public PostCommentDto PostComment { get; set; }
        public async Task OnGet(int id)
        {
            Comment = await commentService.ManageComment.GetCommentById(id);
        }

        public async Task<IActionResult> OnPost()
        {
            var result = await commentService.ManageComment.ChangeStautsComment(PostComment);
            if (result)
            {
                return RedirectToPage("/comment/index");
            }
            return Page();
        }
    }
}
