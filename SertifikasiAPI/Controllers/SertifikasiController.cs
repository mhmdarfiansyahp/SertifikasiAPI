using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SertifikasiAPI.Model;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace SertifikasiAPI.Controllers
{
    public class SertifikasiController : Controller
    {
        private readonly Sertifikasi _sertiRepository;
        ResponseModel response = new ResponseModel();
        private IHostingEnvironment _environment;

        public SertifikasiController(IConfiguration configuration, IHostingEnvironment environment)
        {
            _sertiRepository = new Sertifikasi(configuration);
            _environment = environment;
        }

        [HttpGet("/GetAllSertifikasi", Name = "GetAllSertifikasi")]
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

        [HttpGet("/GetSertifikasi", Name = "GetSertifikasi")]
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


        [HttpPost("/InsertSertifikasi", Name = "InsertSertifikasi")]
        public IActionResult InsertSertifikasi()
        {
            try
            {
                string path = Path.Combine(this._environment.ContentRootPath, "Uploads\\");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                //Fetch the File.
                IFormFile postedFile = Request.Form.Files[0];

                //Fetch the File Name.
                string fileName = Request.Form["fileName"];

/*                string idSerti = Request.Form["as"];
*/              string idProdi = Request.Form["idprodi"];
                string namaSerti = Request.Form["namasertifikat"];
                string tanggal = Request.Form["tanggal"];
                string lembaga = Request.Form["lembagaserti"];
                string level = Request.Form["levelserti"];
                string buktipendukung = fileName;
                string kompeten = Request.Form["kompeten"];
                string tidakkompeten = Request.Form["tidakkompeten"];
                string tidakhadir = Request.Form["tidakhadir"];
                string jumlah = Request.Form["jumlah"];
                string status = Request.Form["Status"];

                //Save the File.
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                SertifikasiModel sertiModel = new SertifikasiModel();
/*                sertiModel.idsertifikat = Convert.ToInt32(idSerti);
*/              sertiModel.idProdi = Convert.ToInt32(idProdi);
                sertiModel.namaSerti = namaSerti;
                sertiModel.tanggal = Convert.ToDateTime(tanggal);
                sertiModel.lembaga = lembaga;
                sertiModel.level = level;
                sertiModel.buktipendukung = buktipendukung;
                sertiModel.kompeten = Convert.ToInt32(kompeten);
                sertiModel.tidakkompeten = Convert.ToInt32(tidakkompeten);
                sertiModel.tidakhadir = Convert.ToInt32(tidakhadir);
                sertiModel.jumlah = Convert.ToInt32(jumlah);
                sertiModel.status = status;

                response.status = 200;
                response.messages = "Success to upload File";

                _sertiRepository.insertData(sertiModel);
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex.Message.ToString();
            }
            return Ok(response);
        }

        [HttpPut("/UpdateSertifikasi", Name = "UpdateSertifikasi")]
        public IActionResult UpdateSertifikasi()
        {
            try
            {
                string path = Path.Combine(this._environment.ContentRootPath, "Uploads\\");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                //Fetch the File.
                IFormFile postedFile = Request.Form.Files[0];

                //Fetch the File Name.
                string fileName = Request.Form["fileName"];

                /*                string idSerti = Request.Form["as"];
                */
                string idProdi = Request.Form["idprodi"];
                string namaSerti = Request.Form["namasertifikat"];
                string tanggal = Request.Form["tanggal"];
                string lembaga = Request.Form["lembagaserti"];
                string level = Request.Form["levelserti"];
                string buktipendukung = fileName;
                string kompeten = Request.Form["kompeten"];
                string tidakkompeten = Request.Form["tidakkompeten"];
                string tidakhadir = Request.Form["tidakhadir"];
                string jumlah = Request.Form["jumlah"];
                string status = Request.Form["Status"];

                //Save the File.
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                SertifikasiModel sertiModel = new SertifikasiModel();
                
                sertiModel.idProdi = Convert.ToInt32(idProdi);
                sertiModel.namaSerti = namaSerti;
                sertiModel.tanggal = Convert.ToDateTime(tanggal);
                sertiModel.lembaga = lembaga;
                sertiModel.level = level;
                sertiModel.buktipendukung = buktipendukung;
                sertiModel.kompeten = Convert.ToInt32(kompeten);
                sertiModel.tidakkompeten = Convert.ToInt32(tidakkompeten);
                sertiModel.tidakhadir = Convert.ToInt32(tidakhadir);
                sertiModel.jumlah = Convert.ToInt32(jumlah);
                sertiModel.status = status;

                response.status = 200;
                response.messages = "Success to upload File";
                _sertiRepository.updateData(sertiModel);
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.messages = "Failed, " + ex;
            }
            return Ok(response);
        }

        [HttpPost("/DeleteSertifikasi", Name = "DeleteSertifikasi")]
        public IActionResult DeleteSertifikasi(int id)
        {
            try
            {
                response.status = 200;
                response.messages = "Success";
                _sertiRepository.deleteData(id);
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

        [HttpGet("/DownloadPdf", Name = "DownloadPdf")]
        public IActionResult DownloadPdf(string fileName)
        {
            try
            {
                var path = Path.Combine(_environment.ContentRootPath, "Uploads\\");

                // Set file path
                var filePath = Path.Combine(path, fileName);

                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound(); // or handle the case where the file is not found
                }

                // Read the file content
                var fileBytes = System.IO.File.ReadAllBytes(filePath);

                // Set file content type
                var contentType = "application/pdf";

                // Kembalikan file sebagai response
                return File(fileBytes, contentType, fileName);
            }
            catch (Exception ex)
            {
                // Handle error jika ada
                // Anda dapat menyesuaikan penanganan kesalahan sesuai kebutuhan Anda
                Console.WriteLine("Error: " + ex.Message);
                return RedirectToAction("Index");  // Ganti dengan action yang sesuai
            }
        }

    }
}
