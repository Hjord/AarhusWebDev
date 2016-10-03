using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AarhusWebDevCoop.ViewModels;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;

namespace AarhusWebDevCoop.Controllers
{
    public class MessageBoardFormSurfaceController : SurfaceController
    {
        // GET: MessageBoardFormSurface
        public ActionResult Index()
        {
            return PartialView("MessageBoardForm", new Message());
        }

        [HttpPost]
        public ActionResult HandleSubmit(Message model)
        {
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            IContent message = Services.ContentService.CreateContent(model.Name, CurrentPage.Id, "Message");
            message.SetValue("messageName", model.Name);
            message.SetValue("messageBody", model.Msg);

            Services.ContentService.SaveAndPublishWithStatus(message);




            return RedirectToCurrentUmbracoPage();
        }


    }
}