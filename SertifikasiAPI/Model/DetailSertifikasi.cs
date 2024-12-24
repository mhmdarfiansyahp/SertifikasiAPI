using System.Data.SqlClient;

namespace SertifikasiAPI.Model
{
    public class DetailSertifikasi
    {
        private readonly string _connectionString;

        private readonly SqlConnection _connection;

        public DetailSertifikasi(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");

            _connection = new SqlConnection(_connectionString);
        }

        public List<DetailSertifikasiModel> getAllData()
        {
            List<DetailSertifikasiModel> bukuList = new List<DetailSertifikasiModel>();
            try
            {
                string query = "select * from tb_detail_sertifikasi s join tb_sertifikasi p on s.id_sertifikasi=p.id_sertifikasi";
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DetailSertifikasiModel buku = new DetailSertifikasiModel
                    {
                        iddetail = Convert.ToInt32(reader["id_detail_sertifikasi"].ToString()),
                        idsertifikasi = Convert.ToInt32(reader["id_sertifikasi"].ToString()),
                        namaSerti = reader["nama_sertifikasi"].ToString(),
                        tanggal = Convert.ToDateTime(reader["tanggal"].ToString()),
                        lembaga = reader["lembaga"].ToString(),
                        level = reader["Level"].ToString(),
                        levelKKNI = Convert.ToInt32(reader["level_KKNI"].ToString()),
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

        public DetailSertifikasiModel getData(int id)
        {
            DetailSertifikasiModel bukuModel = new DetailSertifikasiModel();
            try
            {
                string query = "select * from tb_detail_sertifikasi where id_detail_sertifikasi = @p1";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", id);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                bukuModel.iddetail = Convert.ToInt32(reader["id_detail_sertifikasi"].ToString());
                bukuModel.idsertifikasi = Convert.ToInt32(reader["id_sertifikasi"].ToString());
                bukuModel.namaSerti = reader["nama_sertifikasi"].ToString();
                bukuModel.tanggal = Convert.ToDateTime(reader["tanggal"].ToString());
                bukuModel.lembaga = reader["lembaga"].ToString();
                bukuModel.level = reader["Level"].ToString();
                bukuModel.levelKKNI = Convert.ToInt32(reader["level_KKNI"].ToString());
                reader.Close();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return bukuModel;
        }

        public void insertData(DetailSertifikasiModel prodiModel)
        {
            try
            {
                string query = "insert into tb_detail_sertifikasi values(@p1,@p2,@p3,@p4,@p5)";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", prodiModel.idsertifikasi);
                command.Parameters.AddWithValue("@p2", prodiModel.tanggal);
                command.Parameters.AddWithValue("@p3", prodiModel.lembaga);
                command.Parameters.AddWithValue("@p4", prodiModel.level);
                command.Parameters.AddWithValue("@p5", prodiModel.levelKKNI);

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
