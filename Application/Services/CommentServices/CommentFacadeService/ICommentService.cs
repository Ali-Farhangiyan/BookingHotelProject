using Application.ContextInterfaces;
using Application.Services.CommentServices.AddComment;
using Application.Services.CommentServices.ManagementCommentForAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CommentServices.CommentFacadeService
{
    public interface ICommentService
    {
        public IAddCommentService AddComment { get; }

        public IManagementCommentForAdminService ManageComment { get; }
    }

    public class CommentService : ICommentService
    {
        private readonly IDatabaseContext db;

        public CommentService(IDatabaseContext db)
        {
            this.db = db;
        }



        private IAddCommentService addComment;
        public IAddCommentService AddComment =>
            addComment ?? new AddCommentService(db);


        private IManagementCommentForAdminService manageComment;
        public IManagementCommentForAdminService ManageComment =>
            manageComment ?? new ManagementCommentForAdminService(db);
    }
}
