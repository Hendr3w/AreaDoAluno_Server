using Microsoft.AspNetCore.Mvc;

namespace AreaDoAluno.Controllers
{
    public class EnrollmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
