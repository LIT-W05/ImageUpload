using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using ImageUpload.Data;

namespace WebApplication20.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Upload(string text, HttpPostedFileBase imageFile)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);

            imageFile.SaveAs(Server.MapPath("~/UploadedImages/") + fileName);

            new Db(Properties.Settings.Default.ConStr).Add(fileName, text);

            return RedirectToAction("Index");
        }

        public ActionResult ViewAll()
        {
            return View(new Db(Properties.Settings.Default.ConStr).GetAll());
        }

        public ActionResult ViewSingle(int imageId)
        {
            var image = new Db(Properties.Settings.Default.ConStr).GetAll().FirstOrDefault(i => i.Id == imageId);
            if (image == null)
            {
                return RedirectToAction("Index");
            }
            return View(image);
        }
    }
}