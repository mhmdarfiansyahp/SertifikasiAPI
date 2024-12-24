using System.ComponentModel.DataAnnotations;

namespace SertifikasiAPI.Model
{
    public class ProdiModel
    {
        public int idProdi { get; set; }

        [Required(ErrorMessage = "Nama Prodi is required.")]
        [StringLength(50, ErrorMessage = "Nama Prodi cannot exceed 50 characters.")]
        public string namaProdi { get; set; }
        public string status { get; set; }

    }
}
