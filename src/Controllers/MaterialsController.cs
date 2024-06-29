using Microsoft.AspNetCore.Mvc;

namespace AreaDoAluno.Controllers
{
    public class MaterialsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
