using System;
using System.Collections.Generic;

namespace Reporter.Models
{
    public class ReportResponse
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public UserResponse Creator { get; set; }
        public UserResponse Assignee { get; set; }
        public IEnumerable<CommentResponse> Comments { get; set; }
    }
}