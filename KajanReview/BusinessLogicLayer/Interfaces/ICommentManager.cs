using BusinessLogicLayer.Entities;

namespace BusinessLogicLayer.Interfaces
{
    public interface ICommentManager
    {
        public Task CreateCommentAsync(Comment newComment);
        public Task<Comment> GetCommentByIDAsync(int commentID);
        public Task<List<Comment>> GetAllCommentsAsync();
        public Task UpdateCommentAsync(Comment comment);
        public Task DeleteCommentByIDAsync(int commentID);
    }
}
