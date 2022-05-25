using System.ComponentModel.DataAnnotations;

namespace AuthSystem.Model
{
    public class SignInRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }
    }

    public class SignInResponse
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }
    }

}
