using Microsoft.AspNetCore.Mvc;
using PhotoUploader.Models;
using System.Diagnostics;

namespace PhotoUploader.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        //[DisableRequestSizeLimit]
        //[RequestSizeLimit(1_000_000_000)]
        public async Task<IActionResult> Index(FileUploadModel model){

            if (!ModelState.IsValid)
            {
                //return View(model);
            }

            string directoryPath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", model.Name);
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            UploadSuccessModel uploadSuccessModel = new UploadSuccessModel();
            uploadSuccessModel.Name = model.Name;

            foreach (var file in model.Files)
            {
                if (file.Length > 0)
                {
                    string filePath = Path.Combine(directoryPath, file.FileName);

                    using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                        uploadSuccessModel.ImageCount++;
                    }
                }                
            }

            return View("UploadSuccess", uploadSuccessModel);
        }
    }
}