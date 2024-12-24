using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data.Common;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SertifikasiAPI.Model
{
    public class Login
    {
        private readonly string _connectingString;
        private readonly SqlConnection _connection;
        private readonly IConfiguration _configuration;

        public Login(IConfiguration configuration)
        {
            _connectingString = configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(_connectingString);
            _configuration = configuration;
        }

        public string getLoginPengguna(string username, string password)
        {
            penggunaModel userModel = new penggunaModel();
            if (username == "" || password == "")
            {
                return ("Nama Pengguna dan Kata Sandi tidak boleh kosong");
            }
            else { 
                try
                {
                    string query = "SELECT * FROM tb_pengguna where username = @p1 AND password = @p2";
                    SqlCommand command = new SqlCommand(query, _connection);
                    command.Parameters.AddWithValue("@p1", username);
                    command.Parameters.AddWithValue("@p2", password);
                    _connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read()) { 

                        userModel.iduser = Convert.ToInt32(reader["id_user"].ToString());
                        userModel.nama = reader["nama"].ToString();
                        userModel.role = reader["role"].ToString();
                        userModel.status = reader["status"].ToString();
                        userModel.username = reader["username"].ToString();
                        userModel.password = reader["password"].ToString();

                        reader.Close();
                        _connection.Close();

                        var claims = new[] {
                            new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                            new Claim("username", userModel.username),
                            new Claim("name", userModel.nama),
                            new Claim("role", userModel.role)
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                            _configuration["Jwt:Issuer"],
                            _configuration["Jwt:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddHours(1),
                            signingCredentials: signIn);
                        return new JwtSecurityTokenHandler().WriteToken(token);
                    }
                    else
                    {
                        return "Nama Pengguna atau Kata Sandi tidak ditemukan";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return ex.Message;
                }
            }
            return "Login Gagal";
        }
    }
}