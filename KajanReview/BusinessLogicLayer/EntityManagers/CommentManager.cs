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
            _commentDataAccess = commentDataAccess;
        }

        public void CreateComment(Comment newComment)
        {
            try { _commentDataAccess.CreateComment(newComment); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public Comment GetCommentByID(int commentID)
        {
            try { return _commentDataAccess.GetCommentByID(commentID); }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<Comment> GetAllComments()
        {
            try { return _commentDataAccess.GetAllComments(); }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public void UpdateComment(Comment comment)
        {
            try { _commentDataAccess.UpdateComment(comment); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public void DeleteCommentByID(int commentID)
        {
            try { _commentDataAccess.DeleteCommentByID(commentID); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
    }
}