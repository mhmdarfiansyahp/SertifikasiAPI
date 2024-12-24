using System.Data.SqlClient;

namespace SertifikasiAPI.Model
{
    public class Sertifikasi
    {
        private readonly string _connectionString;

        private readonly SqlConnection _connection;

        public Sertifikasi(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");

            _connection = new SqlConnection(_connectionString);
        }

        public List<DetailModel> getAllData()
        {
            List<DetailModel> bukuList = new List<DetailModel>();
            try
            {
                string query = "SELECT s.id_sertifikasi,s.id_prodi,p.nama_prodi,s.nama_sertifikasi,s.tanggal,s.lembaga,s.level,s.buktipendukung,s.kompeten,s.tidakkompeten,s.tidakhadir,s.jumlah, s.Status " +
                    "FROM tb_sertifikasi s " +
                    "JOIN tb_prodi p ON s.id_prodi = p.id_prodi";
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DetailModel buku = new DetailModel
                    {
                        idsertifikat = Convert.ToInt32(reader["id_sertifikasi"].ToString()),
                        idProdi = Convert.ToInt32(reader["id_prodi"].ToString()),
                        namaProdi = reader["nama_prodi"].ToString(),
                        namaSerti = reader["nama_sertifikasi"].ToString(),
                        tanggal = Convert.ToDateTime(reader["tanggal"].ToString()),
                        lembaga = reader["lembaga"].ToString(),
                        level = reader["level"].ToString(),
                        buktipendukung = reader["buktipendukung"].ToString(),
                        kompeten = Convert.ToInt32(reader["kompeten"].ToString()),
                        tidakkompeten = Convert.ToInt32(reader["tidakkompeten"].ToString()),
                        tidakhadir = Convert.ToInt32(reader["tidakhadir"].ToString()),
                        jumlah = Convert.ToInt32(reader["jumlah"].ToString()),
                        status = reader["Status"].ToString()

                    };
                    bukuList.Add(buku);
                }
                reader.Close();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return bukuList;
        }

        public SertifikasiModel getData(int id)
        {
            SertifikasiModel bukuModel = new SertifikasiModel();
            try
            {
                string query = "select * from tb_sertifikasi where id_sertifikasi = @p1";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", id);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                bukuModel.idsertifikat = Convert.ToInt32(reader["id_sertifikasi"].ToString());
                bukuModel.idProdi = Convert.ToInt32(reader["id_prodi"].ToString());
                bukuModel.namaSerti = reader["nama_sertifikasi"].ToString();
                bukuModel.tanggal = Convert.ToDateTime(reader["tanggal"].ToString());
                bukuModel.lembaga = reader["lembaga"].ToString();
                bukuModel.level = reader["level"].ToString();
                bukuModel.kompeten = Convert.ToInt32(reader["kompeten"].ToString());
                bukuModel.tidakkompeten = Convert.ToInt32(reader["tidakkompeten"].ToString());
                bukuModel.tidakhadir = Convert.ToInt32(reader["tidakhadir"].ToString());
                bukuModel.jumlah = Convert.ToInt32(reader["jumlah"].ToString());
                bukuModel.status = reader["Status"].ToString();
                reader.Close();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return bukuModel;
        }

        public void insertData(SertifikasiModel prodiModel)
        {
            try
            {
                string query = "insert into tb_sertifikasi values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", prodiModel.idProdi);
                command.Parameters.AddWithValue("@p2", prodiModel.namaSerti);
                command.Parameters.AddWithValue("@p3", prodiModel.tanggal);
                command.Parameters.AddWithValue("@p4", prodiModel.lembaga);
                command.Parameters.AddWithValue("@p5", prodiModel.level);
                command.Parameters.AddWithValue("@p6", prodiModel.buktipendukung);
                command.Parameters.AddWithValue("@p7", prodiModel.kompeten);
                command.Parameters.AddWithValue("@p8", prodiModel.tidakkompeten);
                command.Parameters.AddWithValue("@p9", prodiModel.tidakhadir);
                command.Parameters.AddWithValue("@p10", prodiModel.jumlah);
                command.Parameters.AddWithValue("@p11", prodiModel.status); 

                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void updateData(SertifikasiModel prodiModel)
        {
            try
            {
                string query = "update tb_sertifikasi " +
                "set id_prodi = @p2," +
                " nama_sertifikasi = @p3," +
                " tanggal = @p4," +
                " lembaga = @p5," +
                " level = @p6," +
                " buktipendukung = @p7," +
                " kompeten = @p8," +
                " tidakkompeten = @p9," +
                " tidakhadir = @p10," +
                " jumlah = @p11," +
                " Status = @p12," +
                " Where id_sertifikasi = @p1";
                using SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", prodiModel.idsertifikat);
                command.Parameters.AddWithValue("@p2", prodiModel.idProdi);
                command.Parameters.AddWithValue("@p3", prodiModel.namaSerti);
                command.Parameters.AddWithValue("@p4", prodiModel.tanggal);
                command.Parameters.AddWithValue("@p5", prodiModel.lembaga);
                command.Parameters.AddWithValue("@p6", prodiModel.level);
                command.Parameters.AddWithValue("@p7", prodiModel.buktipendukung);
                command.Parameters.AddWithValue("@p8", prodiModel.kompeten);
                command.Parameters.AddWithValue("@p9", prodiModel.tidakkompeten);
                command.Parameters.AddWithValue("@p10", prodiModel.tidakhadir);
                command.Parameters.AddWithValue("@p11", prodiModel.jumlah);
                command.Parameters.AddWithValue("@p12", prodiModel.status);
                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void deleteData(int id)
        {
            try
            {
                string query = "UPDATE tb_sertifikasi SET status = 'TidakAktif' WHERE id_sertifikasi = @p1";
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
    }
}
