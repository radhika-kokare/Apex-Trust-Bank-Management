using Microsoft.AspNetCore.Mvc;

namespace ApexTrustBankMVC.Controllers
{
    public class TransactionController : Controller
    {
        
        [HttpGet]
        public IActionResult Deposit()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Withdraw()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Transfer()
        {
            return View();
        }
        [HttpGet]
        public IActionResult History()
        {
            return View();
        }
    }
}
