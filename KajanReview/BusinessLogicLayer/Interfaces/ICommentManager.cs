using BusinessLogicLayer.Entities;

namespace BusinessLogicLayer.Interfaces
{
    public interface ICommentManager
    {
        public void CreateComment(Comment newComment);
        public Comment GetCommentByID(int commentID);
        public List<Comment> GetAllComments();
        public void UpdateComment(Comment comment);
        public void DeleteCommentByID(int commentID);
    }
}
