using System.ComponentModel.DataAnnotations;

namespace AuthSystem.Model
{
    public class SignupRequest
    {
        [Required]
        public string UserName { get; set; }
        
        [Required]
        public string Password { get; set; }
        
        [Required]
        public string ConfirmPassword { get; set; }
        
        [Required]
        public string Role { get; set; }


    }

    public class SignupResponse
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }
    }
}
