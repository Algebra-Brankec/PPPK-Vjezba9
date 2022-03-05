using System.Web.Mvc;

namespace ApartmentsManger.Controllers
{
    public class FileController : Controller
    {
        private readonly ModelContainer db = new ModelContainer();
        public ActionResult Index(int id)
        {
            var uploadedFile = db.UploadedFiles.Find(id);
            return File(uploadedFile.Content, uploadedFile.ContentType);
        }

        public ActionResult Delete(int id)
        {
            db.UploadedFiles.Remove(db.UploadedFiles.Find(id));
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.AbsolutePath);
        }
    }
}