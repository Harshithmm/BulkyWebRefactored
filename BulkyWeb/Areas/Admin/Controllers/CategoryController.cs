using Bulky.DataAccess.Repository.IRepository;
using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController(IUnitOfWork unitOfWork) : Controller
    {
        public IActionResult Index()
        {
            var categoryList = unitOfWork.Category.GetAll().ToList();
            return View(categoryList);
        }

        public IActionResult Create()
        {
            return
                View(); // OR return View(new Category);   no need to specify as the comiler will itself craete a new object of Category in the model of create.cshtml
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name",
                    "Name and Display Order cannot be same"); //where Name says display under Name field
            }

            if (ModelState.IsValid) /*it checks Range and MaxLength is Valid or not and display error on create.cshml using <span asp-validation-for="Name" class="text-danger"></span>*/
            {
                unitOfWork.Category.Add(category);
                unitOfWork.Category.Save();
                TempData["success"] = "Category created successfully"; //TempData is used to display message on the same page after redirecting to another page
                return RedirectToAction("Index", "Category"); //redirecting to index action of category controller ,can skip "category" as it is the same controller

            }

            return View();
        }


        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryToEdit = unitOfWork.Category.Get(c => c.Id == id);  //Find() is used to find for only primary key
            if (categoryToEdit == null)
            {
                return NotFound();
            }
            return View(categoryToEdit); // OR return View(new Category);   no need to specify as the comiler will itself craete a new object of Category in the model of create.cshtml
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name",
                    "Name and Display Order cannot be same"); //where Name says display under Name field
            }

            if (ModelState.IsValid) /*it checks Range and MaxLength is Valid or not and display error on create.cshml using <span asp-validation-for="Name" class="text-danger"></span>*/
            {
                unitOfWork.Category.Update(category);
                unitOfWork.Category.Save();
                TempData["success"] = "Category updated successfully"; //TempData is used to display message on the same page after redirecting to another page

                return
                    RedirectToAction("Index",
                        "Category"); //redirecting to index action of category controller ,can skip "category" as it is the same controller
            }

            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryToDelete = unitOfWork.Category.Get(c => c.Id == id);  //Find() is used to find for only primary key
            if (categoryToDelete == null)
            {
                return NotFound();
            }
            return View(categoryToDelete); // OR return View(new Category);   no need to specify as the comiler will itself craete a new object of Category in the model of create.cshtml
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(Category category) //if we put Delete then name are same hence we need to put ActionName("Delete") so that form
        //will treat DeletePost method as Delete
        {
            unitOfWork.Category.Remove(category);
            unitOfWork.Category.Save();
            TempData["success"] = "Category deleted successfully"; //TempData is used to display message on the same page after redirecting to another page

            return RedirectToAction("Index", "Category");
        }
    }
}
