using System;

namespace Reporter.Models
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public byte Rights { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}