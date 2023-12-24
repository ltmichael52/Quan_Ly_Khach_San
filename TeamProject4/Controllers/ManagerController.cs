// ManagerController.cs
using Microsoft.AspNetCore.Mvc;
using Team_Project_4.Filters;
using Microsoft.AspNetCore.Http;  // Add this using statement
using static Team_Project_4.Models.AuthorizationModel;

namespace Team_Project_4.Controllers
{
    [CustomAuthorization(UserRole.Manager)]
    public class ManagerController : Controller
    { 

        public IActionResult Index()
        {
            return View();
        }
    }
}
