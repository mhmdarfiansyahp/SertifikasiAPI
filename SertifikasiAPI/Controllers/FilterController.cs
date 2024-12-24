using Microsoft.AspNetCore.Mvc;
using SertifikasiAPI.Model;

namespace SertifikasiAPI.Controllers
{
    public class FilterController : Controller
    {
        private readonly filter _sertiRepository;
        ResponseModel response = new ResponseModel();

        public FilterController(IConfiguration configuration)
        {
            _sertiRepository = new filter(configuration);
        }

        [HttpGet("/GetAllData", Name = "GetAllData")]
        public IActionResult GetAllData()
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                response.data = _sertiRepository.getAllData();
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed";
            }
            return Ok(response);
        }

        /*[HttpGet("/GetAllDataTahun", Name = "GetAllDetailDataTahun")]
        public IActionResult GetAllDataTahun(int year)
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                response.data = _sertiRepository.getAllDataTahun(year);
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed";
            }
            return Ok(response);
        }

        [HttpGet("/GetAllDataSertifikasi", Name = "GetAllDetailDataSertifikasi")]
        public IActionResult GetAllDataSertifikasi(int prodi)
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                response.data = _sertiRepository.getAllDataSertifikasi(prodi);
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed";
            }
            return Ok(response);
        }

        [HttpGet("/GetAllDataProditahun", Name = "GetAllDataProditahun")]
        public IActionResult GetAllDataProditahun(int prodi, int year)
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                response.data = _sertiRepository.getAllDataProditahun( prodi, year);
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed";
            }
            return Ok(response);
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/GetTotalSertifikasiById", Name = "GetTotalSertifikasiById")]
        public IActionResult GetTotalSertifikasiById(int sertif)
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                response.data = _sertiRepository.GetTotalSertifikasiById(sertif);
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed";
            }
            return Ok(response);
        }*/
    }
}
