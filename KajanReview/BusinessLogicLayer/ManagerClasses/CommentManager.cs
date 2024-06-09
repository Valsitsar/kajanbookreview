using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;

namespace BusinessLogicLayer.ManagerClasses
{
    public class CommentManager : ICommentManager
    {
        private readonly ICommentDataAccess _commentDataAccess;

        public CommentManager(ICommentDataAccess commentDataAccess)
        {
            _commentDataAccess = commentDataAccess ?? throw new ArgumentNullException(nameof(_commentDataAccess));
        }

        public async Task CreateCommentAsync(Comment newComment)
        {
            await _commentDataAccess.CreateCommentAsync(newComment);
        }

        public async Task<Comment> GetCommentByIDAsync(int commentID)
        {
            return await _commentDataAccess.GetCommentByIDAsync(commentID);
        }

        public async Task<List<Comment>> GetAllCommentsAsync()
        {
            return await _commentDataAccess.GetAllCommentsAsync();
        }

        public async Task UpdateCommentAsync(Comment comment)
        {
            await _commentDataAccess.UpdateCommentAsync(comment);
        }

        public async Task DeleteCommentByIDAsync(int commentID)
        {
            await _commentDataAccess.DeleteCommentByIDAsync(commentID);
        }
    }
}