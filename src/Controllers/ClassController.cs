using Microsoft.AspNetCore.Mvc;

namespace AreaDoAluno.Controllers
{
    public class ClassController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
