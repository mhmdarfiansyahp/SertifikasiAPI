using Microsoft.AspNetCore.Mvc;
using SertifikasiAPI.Model;

namespace SertifikasiAPI.Controllers
{
    public class DetailSertifikasiController : Controller
    {
        private readonly DetailSertifikasi _sertiRepository;
        ResponseModel response = new ResponseModel();

        public DetailSertifikasiController(IConfiguration configuration)
        {
            _sertiRepository = new DetailSertifikasi(configuration);
        }

        [HttpGet("/GetAllDetailSertifikasi", Name = "GetAllDetailSertifikasi")]
        public IActionResult GetAllSertifikasi()
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

        [HttpGet("/GetDetailSertifikasi", Name = "GetDetailSertifikasi")]
        public IActionResult GetSertifikasi(int id)
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                response.data = _sertiRepository.getData(id);
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex;
            }
            return Ok(response);
        }

        [HttpPost("/InsertDetailSertifikasi", Name = "InsertDetailSertifikasi")]
        public IActionResult InsertSertifikasi([FromBody] DetailSertifikasiModel sertiModel)
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                _sertiRepository.insertData(sertiModel);
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex;
            }
            return Ok(response);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
