using Microsoft.AspNetCore.Mvc;

namespace ApexTrustBankMVC.Controllers
{
  
    public class AccountController : Controller
    {
      
       
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }



        [HttpGet]
        public IActionResult List()
        {
            return View();
        }

    }

}
