using AuthSystem.Model;

namespace AuthSystem.DAL
{
    public interface IAuthDL
    {
        public Task<SignupResponse> Signup (SignupRequest request);

        public Task<SignInResponse> SignIn(SignInRequest request);
    }
}
