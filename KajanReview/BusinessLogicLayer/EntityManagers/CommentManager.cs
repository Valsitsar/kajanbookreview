using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.EntityManagers
{
    public class CommentManager : ICommentManager
    {
        private ICommentDataAccess _commentDataAccess;

        public CommentManager(ICommentDataAccess commentDataAccess)
        {
            _commentDataAccess = commentDataAccess ?? throw new ArgumentNullException(nameof(_commentDataAccess));
        }

        public void CreateComment(Comment newComment)
        {
            _commentDataAccess.CreateComment(newComment);
        }

        public Comment GetCommentByID(int commentID)
        {
            return _commentDataAccess.GetCommentByID(commentID);
        }

        public List<Comment> GetAllComments()
        {
            return _commentDataAccess.GetAllComments();
        }

        public void UpdateComment(Comment comment)
        {
            _commentDataAccess.UpdateComment(comment);
        }

        public void DeleteCommentByID(int commentID)
        {
            _commentDataAccess.DeleteCommentByID(commentID);
        }
    }
}