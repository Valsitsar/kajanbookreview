using BusinessLogicLayer.Entities;

namespace BusinessLogicLayer.Interfaces
{
    public interface ICommentDataAccess
    {
        public Task CreateCommentAsync(Comment newComment);
        public Task<Comment> GetCommentByIDAsync(int commentID);
        public Task<List<Comment>> GetAllCommentsAsync();
        public Task UpdateCommentAsync(Comment comment);
        public Task DeleteCommentByIDAsync(int commentID);
    }
}
