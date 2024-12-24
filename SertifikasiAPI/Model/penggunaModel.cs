using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SertifikasiAPI.Model
{
    public class penggunaModel
    {
        public int iduser { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string nama { get; set; }
        public string role { get; set; }
        public string status { get; set; }
    }
}
