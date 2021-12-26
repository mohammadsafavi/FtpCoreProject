using FTPCore;
using LocalFTPUploadProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LocalFTPUploadProject.Controllers
{
    public class UploadController : Controller
    {
        // GET: UploadController
        public ActionResult Index()
        {
            var list = new List<FileListDirectoryViewModel>();
            for (int i = 1; i <= 10; i++)
            {
                list.Add(new FileListDirectoryViewModel() { Id = i, Name = "File - " + i, Size = i, Type = "" });
            }
            return View(list);
        }

        // GET: UploadController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UploadController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UploadController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UploadFileViewModelcs model)
        {
            try
            {
               // FTPManager.UploadFile(model.FormFile);
                model.FormFile.SaveAsToLocalAndFTP();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UploadController/Edit/5
        public ActionResult Edit(string fileName)
        {
            return View();
        }

        // POST: UploadController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string fileName, string NewfileName)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UploadController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UploadController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string fileName)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
