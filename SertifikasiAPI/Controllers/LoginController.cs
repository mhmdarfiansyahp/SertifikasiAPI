using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SertifikasiAPI.Context;
using SertifikasiAPI.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SertifikasiAPI.Controllers
{
    [Route("api/token")]
    public class LoginController : Controller
    {
        public IConfiguration _configuration;
        public Login _login;
        ResponseModel response = new ResponseModel();

        public LoginController(IConfiguration config)
        {
            _configuration = config;
            _login = new Login(config);
        }

        [HttpPost("submit")]

        public IActionResult Post([FromBody] LoginModel _userData)
        {

            if (_userData != null && _userData.username != null && _userData.password != null)
            {
                try
                {
                    response.data = _login.getLoginPengguna(_userData.username, _userData.password);
                    if (response.data == "Nama Pengguna atau Kata Sandi tidak ditemukan")
                    {
                        response.status = 210;
                        response.messages = "Nama Pengguna atau Kata Sandi tidak ditemukan.";
                    }
                    else if (response.data == "Login Gagal")
                    {
                        response.status = 220;
                        response.messages = "Terjadi Kesalahan saat Login.";
                    }
                    else
                    {
                        response.status = 200;
                        response.messages = "Berhasil Login.";
                    }
                }
                catch (Exception ex)
                {
                    response.status = 500;
                    response.messages = "Failed : " + ex.Message;
                }
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }



        /*        ---------------------------------------------*/
        /*private readonly Pengguna _userRepo;
            private readonly Login _loginRepo;
*/
           /* public LoginController(IConfiguration configuration)
            {
                _userRepo = new Pengguna(configuration);
                _loginRepo = new Login(configuration);
                _configuration = configuration;
            }*/

/*
        [HttpPost("/loginAkun", Name = "loginAkun")]
        //[AllowAnonymous]
        public IActionResult loginAkun([FromBody] LoginModel login)
        {
            penggunaModel pgn = _userRepo.login(login.username, login.password);
            try
            {
                if (pgn != null)
                {
                    if (pgn.password != null)
                    {
                        // login.password is correct, login successful
                        var token = _loginRepo.GenerateJwtToken(pgn);

                        // Set JWT token in cookies
                        Response.Cookies.Append("token", token, new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,  // Set to true in a production environment (requires HTTPS)
                            SameSite = SameSiteMode.None,  // Adjust according to your needs
                            Expires = DateTime.Now.AddMinutes(10)  // Set the expiration time as needed
                        });

                        if (pgn.role == "Userti")
                        {
                            Response.Cookies.Append("Role", "Userti", new CookieOptions
                            {
                                HttpOnly = false,
                                Secure = true,  // Set to true in a production environment (requires HTTPS)
                                SameSite = SameSiteMode.None,  // Adjust according to your needs
                                Expires = DateTime.Now.AddMinutes(10)  // Set the expiration time as needed
                            });

                            return Ok(new { Status = 200, Messages = "Login berhasil", Data = pgn, Role = "Userti", Token = token });
                        }
                        else if (pgn.role == "Sekretaris Prodi")
                        {

                            Response.Cookies.Append("Role", "Sekretaris Prodi", new CookieOptions
                            {
                                HttpOnly = false,
                                Secure = true,  // Set to true in a production environment (requires HTTPS)
                                SameSite = SameSiteMode.None,  // Adjust according to your needs
                                Expires = DateTime.Now.AddMinutes(10)  // Set the expiration time as needed
                            });

                            return Ok(new { Status = 200, Messages = "Login berhasil", Data = pgn, Role = "Sekretaris Prodi", Token = token });
                        }
                        
                        return Ok(new { Status = 200, Messages = "Login berhasil", Data = pgn, Role = "Unknown", Token = token });

                    }
                    else
                    {
                        // login.password is incorrect
                        return Unauthorized(new { Status = 401, Messages = "Kata Sandi Salah", Data = new Object() });
                    }
                }

                else
                {
                    // Account not found
                    return NotFound(new { Status = 404, Messages = "Akun Tidak Ditemukan", Data = new Object() });
                }
            }
            catch (Exception ex)
            {
                // General error
                return StatusCode(500, new { Status = 500, Messages = "Terjadi Kesalahan Saat Login = " + ex.Message, Data = new Object() });
            }
        }


        [HttpPost("/ValidateToken", Name = "ValidateToken")]
            public IActionResult ValidateToken([FromHeader] string token)
            {
                if (token == null)
                {
                    return BadRequest(new { Status = 401, Message = "Token is missing or empty." });
                }

                bool isValid = _loginRepo.ValidateJwtToken(token);

                if (isValid)
                {
                    return Ok(new { Status = 200, Message = "Authorized." });
                }
                else
                {
                    return Unauthorized(new { Status = 401, Message = "Unauthorized." });
                }
            }
        public IActionResult Index()
        {
            return View();
        }*/
    }
}
