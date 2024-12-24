namespace SertifikasiAPI.Model
{
    public class DetailModel
    {
        public int idsertifikat { get; set; }
        public int idProdi { get; set; }
        public string namaSerti { get; set; }
        public DateTime tanggal { get; set; }
        public string lembaga { get; set; }
        public string level { get; set; }
        public string buktipendukung { get; set; }
        public int kompeten { get; set; }
        public int tidakkompeten { get; set; }
        public int tidakhadir { get; set; }
        public int jumlah { get; set; }

        public string namaProdi { get; set; }
        public string status { get; set; }

    }
}
