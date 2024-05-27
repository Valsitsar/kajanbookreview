using BusinessLogicLayer.Entities;

namespace BusinessLogicLayer.Interfaces
{
    public interface IUserPost
    {
        int ID { get; }
        User Poster { get; }
        string Body { get; }
        int UpvoteCount { get; }
        int DownvoteCount { get; }
        DateTime PostDate { get; }
    }
}
