using System;

namespace Reporter.Models
{
    public class UserResponseExtended : UserResponse
    {
        public Guid Id { get; set; }
        public byte Rights { get; set; }
        public string Token { get; set; }
    }
}