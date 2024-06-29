using Microsoft.AspNetCore.Mvc;

namespace AreaDoAluno.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
