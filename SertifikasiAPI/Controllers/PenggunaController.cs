using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SertifikasiAPI.Model;

namespace SertifikasiAPI.Controllers
{
    //[Authorize]
    public class PenggunaController : Controller
    {
        private readonly Pengguna _penggunaRepo;
        private readonly IConfiguration _configuration;
        ResponseModel response = new ResponseModel();

        public PenggunaController(IConfiguration configuration)
        {
            _penggunaRepo = new Pengguna(configuration);
            _configuration = configuration;
        }

        [HttpGet("/GetAllPengguna", Name = "GetAllPengguna")]
        public IActionResult GetAllPengguna()
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                response.data = _penggunaRepo.getAllData();
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed";
            }
            return Ok(response);
        }

        [HttpPost("/TambahPengguna", Name = "TambahPengguna")]
        public IActionResult TambahPengguna([FromBody] penggunaModel penggunaModel)
        {
            try
            {
                var result = _penggunaRepo.TambahData(penggunaModel);
                response.status = 200;
                response.messages = "Success";
                response.data = result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = 500, Messages = "Terjadi Kesalahan Saat Menambah Data", Data = ex.Message });
            }
            return Ok(response);
        }

        [HttpPut("/UbahAkun", Name = "UbahAkun")]
        public IActionResult UbahAkun([FromBody] penggunaModel penggunaModel)
        {
            try
            {
                var result = _penggunaRepo.UbahData(penggunaModel);
                response.status = 200;
                response.messages = "Success";
                response.data = result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = 500, Messages = "Terjadi Kesalahan Saat Mengubah Data", Data = ex.Message });
            }
            return Ok(response);
        }

        [HttpDelete("/DeletePengguna", Name = "DeletePengguna")]
        public IActionResult DeletePengguna(int id)
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                _penggunaRepo.deleteData(id);
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex;
            }
            return Ok(response);
        }

        [HttpGet("/GetPengguna", Name = "GetPengguna")]
        public IActionResult GetPengguna(int id)
        {
            try
            {
                var result = _penggunaRepo.getData(id);
                if (result != null)
                {
                    return Ok(new { Status = 200, Messages = "Berhasil Mendapatkan Data", Data = result });
                }
                else
                {
                    return StatusCode(404, new { Status = 404, Messages = "Data Tidak Ditemukan", Data = result });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = 500, Messages = "Terjadi Kesalahan Saat Mendapatkan Data", Data = ex.Message });
            }
        }

        // Kode lainnya (tidak terdapat di cuplikan sebelumnya)

        // [HttpPost("/CreatePengguna", Name = "CreatePengguna")]
        // [HttpPut("/UpdatePengguna", Name = "UpdatePengguna")]
        // [HttpDelete("/HapusPengguna", Name = "HapusPengguna")]
    }
}






