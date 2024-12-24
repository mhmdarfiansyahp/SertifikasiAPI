using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SertifikasiAPI.Model;

namespace SertifikasiAPI.Controllers
{

    public class ProdiController : Controller
    {
        private readonly Prodi _prodiRepository;
        ResponseModel response = new ResponseModel();

        public ProdiController(IConfiguration configuration)
        {
            _prodiRepository = new Prodi(configuration);
        }

        [HttpGet("/GetAllProdi", Name = "GetAllProdi")]
        public IActionResult GetAllProdi()
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                response.data = _prodiRepository.getAllData();
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed";
            }
            return Ok(response);
        }

        [HttpGet("/GetProdi", Name = "GetProdi")]
        public IActionResult GetProdi(int id)
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                response.data = _prodiRepository.getData(id);
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex;
            }
            return Ok(response);
        }

        [HttpPost("/InsertProdi", Name = "InsertProdi")]
        public IActionResult InsertProdi([FromBody] ProdiModel prodiModel)
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                _prodiRepository.insertData(prodiModel);
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex;
            }
            return Ok(response);
        }

        [HttpPut("/UpdateProdi", Name = "UpdateProdi")]
        public IActionResult UpdateProdi([FromBody] ProdiModel prodiModel)
        {
            ProdiModel buku = new ProdiModel();
            buku.idProdi = prodiModel.idProdi;
            buku.namaProdi = prodiModel.namaProdi;
            buku.status = prodiModel.status;

            try
            {
                response.status = 200;
                response.messages = "Success";
                _prodiRepository.updateData(buku);
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex;
            }
            return Ok(response);
        }

        [HttpDelete("/DeleteProdi", Name = "DeleteProdi")]
        public IActionResult DeleteProdi(int id)
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                _prodiRepository.deleteData(id);
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
