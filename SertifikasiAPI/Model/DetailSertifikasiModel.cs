namespace SertifikasiAPI.Model
{
    public class DetailSertifikasiModel
    {
        public int iddetail { get; set; }
        public int idsertifikasi { get; set; }
        public DateTime tanggal { get; set; }
        public string lembaga { get; set; }
        public string level { get; set; }
        public int levelKKNI { get; set; }

        public string namaSerti { get; set; }

    }
}
