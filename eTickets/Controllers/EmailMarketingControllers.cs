using eTickets.Models;
using eTickets.Services;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class EmailMarketingController : Controller
    {
        private readonly EmailService _emailService;

        public EmailMarketingController(EmailService emailService)
        {
            _emailService = emailService;
        }
        public IActionResult SendMarketingEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMarketingEmail(EmailMarketingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Templates/Template.cshtml");
            string link = "https://your-marketing-link.com";

            string emailContent = await _emailService.LoadEmailTemplateAsync(templatePath, model.Name, link);
            await _emailService.SendEmailAsync(model.Email, "Uu dai dac biet cho ban", emailContent);

            ViewBag.Message = "Email da duoc gui thanh cong";
            return View();
        }
    }
}
