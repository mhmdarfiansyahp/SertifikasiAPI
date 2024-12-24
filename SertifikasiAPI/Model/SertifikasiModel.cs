using System.ComponentModel.DataAnnotations;

namespace SertifikasiAPI.Model
{
    public class SertifikasiModel
    {
        public int idsertifikat { get; set; }

        [RegularExpression(@"^(?!null$).*", ErrorMessage = "type of car to choose!")]
        public int idProdi { get; set; }

        [Required(ErrorMessage = "Nama Sertifikasi Wajib Diisi.")]
        [MaxLength(100, ErrorMessage = "Nama maksimal 100 karakter.")]
        public string namaSerti { get; set;}

        [DataType(DataType.DateTime, ErrorMessage = "Format tanggal tidak valid.")]
        public DateTime tanggal { get; set; }

        [Required(ErrorMessage = "Lembaga Wajib Diisi.")]
        [MaxLength(100, ErrorMessage = "Nama maksimal 100 karakter.")]
        public string lembaga { get; set; }

        [Required(ErrorMessage = "Level Sertifikasi Wajib Diisi.")]
        [MaxLength(100, ErrorMessage = "Nama maksimal 100 karakter.")]
        public string level { get; set; }

        [RegularExpression(@"^.+\.pdf$", ErrorMessage = "Bukti Pendukung harus berupa file PDF.")]
        public string buktipendukung { get; set; }

        [Required(ErrorMessage = "Peserta Kompetan Wajib Diisi.")]
        public int kompeten { get; set; }

        [Required(ErrorMessage = "Peserta Tidak Kompeten Wajib Diisi.")]
        public int tidakkompeten { get; set; }

        [Required(ErrorMessage = "Peserta Tidak Hadir Wajib Diisi.")]
        public int tidakhadir { get; set; }
        public int jumlah { get; set; }
        public string status { get; set; }

    }
}
