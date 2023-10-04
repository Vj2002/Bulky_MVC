using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        //ApplicationDbContext db = new ApplicationDbContext();
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IWebHostEnvironment _webHostEnvironment;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //_webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            
            return View(objCompanyList);
        }
        // Update Insert
        public IActionResult Upsert(int? id)
        {
            // IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            // {
            //     Text = u.Name,
            //     Value = u.Id.ToString()
            // });
            //// ViewBag.CategoryList = CategoryList;
            // ViewData["CategoryList"] = CategoryList;
            //CompanyVM CompanyVM = new()
            //{
            //    CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            //    {
            //        Text = u.Name,
            //        Value = u.Id.ToString()
            //    }),
            //    Company = new Company()
            //};
            if(id==null || id==0)
            {
                //create
            return View(new Company());
            }
            else
            {
                //update
                Company companyObj = _unitOfWork.Company.Get(u => u.Id == id);
                return View(companyObj);
            }
        }
        [HttpPost]
        public IActionResult Upsert(Company CompanyObj)
        {
            //if (obj.Name == obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            //}
            //if (obj.Name != null && obj.Name.ToLower() == "test")  
            //{
            //    ModelState.AddModelError("", "Test is an invalid value");
            //}
            if (ModelState.IsValid)
            {
               // string wwwRootPath = _webHostEnvironment.WebRootPath;
                //if(file!=null)
                //{
                //    string fileName=Guid.NewGuid().ToString()+Path.GetExtension(file.FileName);
                //    string CompanyPath = Path.Combine(wwwRootPath, @"images\Company");

                //    if(!string.IsNullOrEmpty(CompanyVM.Company.ImageUrl))
                //    {
                //        //delete the old image
                //        var oldImagePath = Path.Combine(wwwRootPath, CompanyVM.Company.ImageUrl.TrimStart('\\'));

                //        if(System.IO.File.Exists(oldImagePath))
                //        {
                //            System.IO.File.Delete(oldImagePath);

                //        }
                //    }

                //    using (var fileStream = new FileStream(Path.Combine(CompanyPath, fileName),FileMode.Create))
                //    {
                //        file.CopyTo(fileStream);
                //    }
                //    CompanyVM.Company.ImageUrl = @"\images\Company\" + fileName;
                //}

                if (CompanyObj.Id==0)
                {
                _unitOfWork.Company.Add(CompanyObj);
                }
                else
                {
                    _unitOfWork.Company.Update(CompanyObj);
                }
                _unitOfWork.Save();
                TempData["success"] = "Company created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                //CompanyVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                //{
                //    Text = u.Name,
                //    Value = u.Id.ToString()
                //});
                
            return View(CompanyObj);
            }
        }

        //public IActionResult Edit(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Company? CompanyFromDb = _unitOfWork.Company.Get(u => u.Id == id);
        //    //Company? CompanyFromDb11 = _db.Categories.FirstOrDefault(u=>u.Id==id);
        //    //Company? CompanyFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();
        //    if (CompanyFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(CompanyFromDb);
        //}
        //[HttpPost]
        //public IActionResult Edit(Company obj)
        //{
        //    //if (obj.Name == obj.DisplayOrder.ToString())
        //    //{
        //    //    ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
        //    //}
        //    //if (obj.Name != null && obj.Name.ToLower() == "test")
        //    //{
        //    //    ModelState.AddModelError("", "Test is an invalid value");
        //    //}
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Company.Update(obj);
        //        _unitOfWork.Save();
        //        TempData["success"] = "Company updated successfully";
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}

        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Company? CompanyFromDb = _unitOfWork.Company.Get(u => u.Id == id);
        //    //Company? CompanyFromDb11 = _db.Categories.FirstOrDefault(u=>u.Id==id);
        //    //Company? CompanyFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();
        //    if (CompanyFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(CompanyFromDb);
        //}
        //[HttpPost, ActionName("Delete")]

        //public IActionResult DeletePOST(int? id)
        //{
        //    //if (obj.Name == obj.DisplayOrder.ToString())
        //    //{
        //    //    ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
        //    //}
        //    //if (obj.Name != null && obj.Name.ToLower() == "test")
        //    //{
        //    //    ModelState.AddModelError("", "Test is an invalid value");
        //    //}
        //    Company obj = _unitOfWork.Company.Get(u => u.Id == id);
        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }
        //    _unitOfWork.Company.Remove(obj);
        //    _unitOfWork.Save();
        //    TempData["success"] = "Company deleted successfully";
        //    return RedirectToAction("Index");

        //}

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            return Json(new { data = objCompanyList });
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var CompanyToBeDeleted = _unitOfWork.Company.Get(u => u.Id == id);
            if(CompanyToBeDeleted==null)
            {
                return Json(new
                {
                    success=false,
                    message="Error while deleting"
                });
            }
            //var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, CompanyToBeDeleted.ImageUrl.TrimStart('\\'));

            //if (System.IO.File.Exists(oldImagePath))
            //{
            //    System.IO.File.Delete(oldImagePath);

            //}
            _unitOfWork.Company.Remove(CompanyToBeDeleted);
            _unitOfWork.Save();
            return Json(new
            {
                success = true,
                message = "Delete Successful"
            });
        }
        #endregion

    }
}
