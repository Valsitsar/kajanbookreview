using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
