using System.Data.SqlClient;
using System.Data;

namespace SertifikasiAPI.Model
{
    public class filter
    {
        private readonly string _connectionString;

        private readonly SqlConnection _connection;

        public filter(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");

            _connection = new SqlConnection(_connectionString);
        }

        public List<filteralldatamodel> getAllData()
        {
            List<filteralldatamodel> bukuList = new List<filteralldatamodel>();
            try
            {
                string query = "GetTotalSertifikasi";
                SqlCommand command = new SqlCommand(query, _connection);
                command.CommandType = CommandType.StoredProcedure;
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    filteralldatamodel buku = new filteralldatamodel
                    {
                        idSertifikasi = Convert.ToInt32(reader["id_sertifikasi"].ToString()),
                        namaSertifikasi= reader["nama_sertifikasi"].ToString(),
                        tahunSertifikasi = Convert.ToInt32(reader["tahun"].ToString()),
                        idProdi = Convert.ToInt32(reader["id_prodi"].ToString()),
                        namaProdi = reader["nama_prodi"].ToString(),
                        kompeten = Convert.ToInt32(reader["total_kompeten"].ToString()),
                        tidakkompeten = Convert.ToInt32(reader["total_tidakkompeten"].ToString()),
                        tidakhadir = Convert.ToInt32(reader["total_tidakhadir"].ToString()),
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

        /*public List<filteralldatamodel> getAllDataTahun(int tahun)
        {
            List<filteralldatamodel> bukuList = new List<filteralldatamodel>();
            try
            {
                string query = "GetDatabyTahun";
                SqlCommand command = new SqlCommand(query, _connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@selectedYear", tahun);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    filteralldatamodel buku = new filteralldatamodel
                    {
                        idProdi = Convert.ToInt32(reader["id_prodi"].ToString()),
                        namaProdi = reader["nama_prodi"].ToString(),
                        kompeten = Convert.ToInt32(reader["total_kompeten"].ToString()),
                        tidakkompeten = Convert.ToInt32(reader["total_tidakkompeten"].ToString()),
                        tidakhadir = Convert.ToInt32(reader["total_tidakhadir"].ToString()),
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

        public List<filterbyProdimodel> getAllDataSertifikasi(int prodi)
        {
            List<filterbyProdimodel> bukuList = new List<filterbyProdimodel>();
            try
            {
                string query = "GetTotalSertifikasiByProdi";
                SqlCommand command = new SqlCommand(query, _connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@prodi_id", prodi);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    filterbyProdimodel buku = new filterbyProdimodel
                    {
                        idProdi = Convert.ToInt32(reader["id_prodi"].ToString()),
                        namaSerti = reader["nama_sertifikasi"].ToString(),
                        kompeten = Convert.ToInt32(reader["total_kompeten"].ToString()),
                        tidakkompeten = Convert.ToInt32(reader["total_tidakkompeten"].ToString()),
                        tidakhadir = Convert.ToInt32(reader["total_tidakhadir"].ToString()),
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

        public List<filterproditahunmodel> getAllDataProditahun(int prodi, int year)
        {
            List<filterproditahunmodel> bukuList = new List<filterproditahunmodel>();
            try
            {
                string query = "GetSertifikasiByProdiAndYear";
                SqlCommand command = new SqlCommand(query, _connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@prodi_id", prodi);
                command.Parameters.AddWithValue("@tahun", year);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    filterproditahunmodel buku = new filterproditahunmodel
                    {
                        idsertifikat = Convert.ToInt32(reader["id_sertifikasi"].ToString()),
                        namaSerti = reader["nama_sertifikasi"].ToString(),
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

        public List<FilterBySertifikasiModel> GetTotalSertifikasiById(int sertif)
        {
            List<FilterBySertifikasiModel> bukuList = new List<FilterBySertifikasiModel>();
            try
            {
                string query = "GetTotalSertifikasiById";
                SqlCommand command = new SqlCommand(query, _connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id_Sertif", sertif);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    FilterBySertifikasiModel buku = new FilterBySertifikasiModel
                    {
                        idProdi = Convert.ToInt32(reader["id_prodi"].ToString()),
                        namaSerti = reader["nama_sertifikasi"].ToString(),
                        kompeten = Convert.ToInt32(reader["total_kompeten"].ToString()),
                        tidakkompeten = Convert.ToInt32(reader["total_tidakkompeten"].ToString()),
                        tidakhadir = Convert.ToInt32(reader["total_tidakhadir"].ToString()),
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
        }*/
    }
}
