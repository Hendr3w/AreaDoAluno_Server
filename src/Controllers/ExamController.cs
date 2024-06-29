using Microsoft.AspNetCore.Mvc;

namespace AreaDoAluno.Controllers
{
    public class ExamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
