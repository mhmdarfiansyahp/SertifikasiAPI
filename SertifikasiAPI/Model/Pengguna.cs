using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SertifikasiAPI.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SertifikasiAPI.Model
{
    public class Pengguna
    {
        private readonly string _connectingString;
        private readonly SqlConnection _connection;
        ResponseModel response = new ResponseModel();
        private readonly IConfiguration _configuration;

        public Pengguna(IConfiguration configuration)
        {
            _connectingString = configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(_connectingString);
            _configuration = configuration;
        }

        public List<penggunaModel> getAllData()
        {
            try
            {
                string query = "SELECT * FROM tb_pengguna";
                using (SqlConnection connection = new SqlConnection(_connectingString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        List<penggunaModel> penggunaList = new List<penggunaModel>();

                        while (reader.Read())
                        {
                            penggunaModel pengguna = new penggunaModel
                            {
                                iduser = Convert.ToInt32(reader["id_user"].ToString()),
                                username = reader["username"].ToString(),
                                password = reader["password"].ToString(),
                                nama = reader["nama"].ToString(),
                                role = reader["role"].ToString(),
                                status = reader["status"].ToString()
                            };

                            penggunaList.Add(pengguna);
                        }

                        reader.Close();
                        return penggunaList;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public ResponseModel TambahData(penggunaModel pengguna)
        {
            try
            {
                string query = "INSERT INTO tb_pengguna(username, password, nama, role, status) VALUES(@p2, @p3, @p4, @p5, @p6)";
                SqlCommand command = new SqlCommand(query, _connection);
                
                command.Parameters.AddWithValue("@p2", pengguna.username);
                command.Parameters.AddWithValue("@p3", pengguna.password);
                command.Parameters.AddWithValue("@p4", pengguna.nama);
                command.Parameters.AddWithValue("@p5", pengguna.role);
                command.Parameters.AddWithValue("@p6", pengguna.status);

                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();

                response.status = 200;
                response.messages = "Pengguna berhasil ditambahkan";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                response.status = 500;
                response.messages = "Terjadi kesalahan saat menambah pengguna = " + ex.Message;
            }
            return response;
        }

        public ResponseModel UbahData(penggunaModel pengguna)
        {
            try
            {
                string query = "UPDATE tb_pengguna SET password = @p2, status = @p3 WHERE id_user = @p1";
                SqlCommand command = new SqlCommand(query, _connection);

                command.Parameters.AddWithValue("@p1", pengguna.iduser);
                command.Parameters.AddWithValue("@p2", pengguna.password);
                command.Parameters.AddWithValue("@p3", pengguna.status);

                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();

                response.status = 200;
                response.messages = "Pengguna berhasil diubah";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                response.status = 500;
                response.messages = "Terjadi kesalahan saat mengubah pengguna = " + ex.Message;
            }
            return response;
        }
        public ResponseModel UbahStatus(penggunaModel pengguna)
        {
            try
            {
                string query = "UPDATE tb_pengguna SET status = @p3 WHERE username = @p1";
                SqlCommand command = new SqlCommand(query, _connection);

                command.Parameters.AddWithValue("@p1", pengguna.username);
                command.Parameters.AddWithValue("@p3", pengguna.status);

                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();

                response.status = 200;
                response.messages = "Pengguna berhasil diubah";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                response.status = 500;
                response.messages = "Terjadi kesalahan saat mengubah pengguna = " + ex.Message;
            }
            return response;
        }

        public penggunaModel getData(int id)
        {
            try
            {
                string query = "SELECT * FROM tb_pengguna WHERE id_user = @p1";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", id);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    penggunaModel pengguna = new penggunaModel
                    {
                        iduser = Convert.ToInt32(reader["id_user"].ToString()),
                        username = reader["username"].ToString(),
                        password = reader["password"].ToString(),
                        nama = reader["nama"].ToString(),
                        role = reader["role"].ToString(),
                        status = reader["status"].ToString()
                    };

                    reader.Close();
                    _connection.Close();
                    return pengguna;
                }
                else
                {
                    reader.Close();
                    _connection.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public void deleteData(int id)
        {
            try
            {
                string query = "UPDATE tb_pengguna SET status = 'TidakAktif' WHERE id_user = @p1";
                using SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", id);
                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /*public penggunaModel login(string username, string password)
        {
            penggunaModel pengguna = new penggunaModel();
            try
            {
                string query = "SELECT * FROM tb_pengguna WHERE username = @p1";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", username);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // User found, verify password
                    string hashedPasswordFromDb = reader["password"].ToString();

                    if (!string.IsNullOrEmpty(hashedPasswordFromDb))
                    {
                        bool verify = BCrypt.Net.BCrypt.Verify(username, hashedPasswordFromDb);

                        if (verify)
                        {
                            // Password is correct, populate Pengguna
                            pengguna = new penggunaModel
                            {
                                username = reader["username"].ToString(),
                                password = reader["password"].ToString(),
                                role = reader["role"].ToString()
                            };
                        }
                    }

                    reader.Close();
                    _connection.Close();
                    return pengguna;
                }
                else
                {
                    // User not found
                    reader.Close();
                    _connection.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public string CreateToken(penggunaModel pengguna)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!);
            var tokenDeskriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("username", pengguna.username),
                    new Claim(JwtRegisteredClaimNames.Name, pengguna.role),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
            };

            var token = jwtTokenHandler.CreateToken(tokenDeskriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }*/
    }
}
