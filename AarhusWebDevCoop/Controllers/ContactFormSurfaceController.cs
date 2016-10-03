using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AarhusWebDevCoop.ViewModels;
using Umbraco.Web.Mvc;
using System.Net.Mail;
using Umbraco.Core.Models;

namespace AarhusWebDevCoop.Controllers
{
    public class ContactFormSurfaceController : SurfaceController
    {
        // GET: ContactFormSurfaceController
        public ActionResult Index()
        {
            return PartialView("ContactForm", new ContactForm());
        }

        [HttpPost]
        public ActionResult HandleFormSubmit(ContactForm form)
        {
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            MailMessage message = new MailMessage();
            message.To.Add("mymail@gmail.com");
            message.Subject = form.Subject;
            message.From = new MailAddress(form.Email, form.Name);
            message.Body = form.Message + "\n my email is: " + form.Email;

            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.Credentials = new NetworkCredential("mymail@gmail.com", "mypassword");
                smtp.EnableSsl = true;

                smtp.Send(message);
            }

            TempData["success"] = true;

            IContent comment = Services.ContentService.CreateContent(form.Subject, CurrentPage.Id, "Comment");

            comment.SetValue("commentName", form.Name);
            comment.SetValue("subject", form.Subject);
            comment.SetValue("email", form.Email);
            comment.SetValue("message", form.Message);

            //Save
            Services.ContentService.Save(comment);


            //Save & publish
            //Services.ContentService.SaveAndPublishWithStatus(comment);

            return RedirectToCurrentUmbracoPage();
        }
    }
}