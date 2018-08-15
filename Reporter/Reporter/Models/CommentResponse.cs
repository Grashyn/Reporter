using System;

namespace Reporter.Models
{
    public class CommentResponse
    {
        public Guid Id { get; set; }
        public DateTime PostDate { get; set; }
        public string Text { get; set; }
        public UserResponse User { get; set; }
    }
}