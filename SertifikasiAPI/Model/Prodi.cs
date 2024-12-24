using System.Data.SqlClient;

namespace SertifikasiAPI.Model
{
    public class Prodi
    {
        private readonly string _connectionString;

        private readonly SqlConnection _connection;

        public Prodi(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");

            _connection = new SqlConnection(_connectionString);
        }

        public List<ProdiModel> getAllData()
        {
            List<ProdiModel> bukuList = new List<ProdiModel>();
            try
            {
                string query = "select * from tb_prodi";
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ProdiModel buku = new ProdiModel
                    {
                        idProdi = Convert.ToInt32(reader["id_prodi"].ToString()),
                        namaProdi = reader["nama_prodi"].ToString(),
                        status = reader["status"].ToString(),
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

        public ProdiModel getData(int id)
        {
            ProdiModel bukuModel = new ProdiModel();
            try
            {
                string query = "SELECT * FROM tb_prodi WHERE id_prodi = @p1";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", id);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    bukuModel.idProdi = Convert.ToInt32(reader["id_prodi"].ToString());
                    bukuModel.namaProdi = reader["nama_prodi"].ToString();
                    bukuModel.status = reader["status"].ToString();
                }

                reader.Close();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return bukuModel;
        }

        public void insertData(ProdiModel prodiModel)
        {
            try
            {
                string query = "insert into tb_prodi values(@p1,@p2)";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", prodiModel.namaProdi);
                command.Parameters.AddWithValue("@p2", prodiModel.status);
                
                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void updateData(ProdiModel prodiModel)
        {
            try
            {
                string query = "update tb_prodi " +
                "set nama_prodi = @p2," +
                " status = @p3" +
                " Where id_prodi = @p1";
                using SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", prodiModel.idProdi);
                command.Parameters.AddWithValue("@p2", prodiModel.namaProdi);
                command.Parameters.AddWithValue("@p3", prodiModel.status);
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
                string query = "UPDATE tb_Prodi SET status = 'TidakAktif' WHERE id_prodi = @p1";
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
