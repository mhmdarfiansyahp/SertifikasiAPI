using System.ComponentModel.DataAnnotations;

namespace SertifikasiAPI.Model
{
    public class LoginModel
    {
        [Key]
        public string username { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
    }
}
