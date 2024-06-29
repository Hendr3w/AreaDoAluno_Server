using Microsoft.AspNetCore.Mvc;

namespace AreaDoAluno.Controllers
{
    public class SubjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
