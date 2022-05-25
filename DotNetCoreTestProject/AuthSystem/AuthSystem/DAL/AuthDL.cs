using AuthSystem.Model;
using System.Data;
using System.Data.SqlClient;

namespace AuthSystem.DAL
{
    public class AuthDL : IAuthDL
    {
        public readonly IConfiguration _configuration;

        public readonly SqlConnection _sqlconnection;
        public AuthDL(IConfiguration configuration)
        {
            _configuration = configuration;
            _sqlconnection = new SqlConnection(_configuration.GetConnectionString("MySQLDBConnection"));

        }
        public async Task<SignupResponse> Signup(SignupRequest request)
        {
            SignupResponse response = new SignupResponse();
            response.IsSuccess = true;
            if (!request.Password.Equals(request.ConfirmPassword))
            {
                response.IsSuccess = false;
                response.Message = "Password not mached";
                return response;
            }

            try
            {
                await _sqlconnection.OpenAsync();
                string query = @"INSERT INTO[dbo].[Users] ([UserName],[Password],[Role]) VALUES(@UserName, @Password, @Role)";
                using (SqlCommand cmd = new SqlCommand(query, _sqlconnection))
                {
                    cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = request.UserName;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = request.Password;
                    cmd.Parameters.Add("@Role", SqlDbType.VarChar, 50).Value = request.Role;
                    int status = await cmd.ExecuteNonQueryAsync();
                    if (status <= 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "Something went wrong";
                    }
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            finally
            {
                await _sqlconnection.CloseAsync();
                await _sqlconnection.DisposeAsync();
            }
            return response;
        }
        public async Task<SignInResponse> SignIn(SignInRequest request)
        {
            SignInResponse response = new SignInResponse();
            response.IsSuccess = true;
           

            try
            {
                await _sqlconnection.OpenAsync();
                string query = @"select * from Users where UserName = @UserName AND Password = @Password AND Role = @Role";
                using (SqlCommand cmd = new SqlCommand(query, _sqlconnection))
                {
                    cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = request.UserName;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = request.Password;
                    cmd.Parameters.Add("@Role", SqlDbType.VarChar, 50).Value = request.Role;
                    using (SqlDataReader dataReader = await cmd.ExecuteReaderAsync())
                    {
                        if(dataReader.HasRows)
                        {
                            response.Message = "Login Successfully";
                        }
                        else
                        {
                            response.IsSuccess = false;
                            response.Message = "Login Failed";
                        }
                    }
                   
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            finally
            {
                await _sqlconnection.CloseAsync();
                await _sqlconnection.DisposeAsync();
            }
            return response;
        }
    }
}
