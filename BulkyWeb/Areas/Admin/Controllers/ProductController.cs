using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.ViewModels;
using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController(IUnitOfWork unitOfWork,IWebHostEnvironment webHostEnvironment) : Controller   //using dependency injection webHostEnvironment is used to get the path of wwwroot folder
    {
        public IActionResult Index()
        {
            var productList = unitOfWork.Product.GetAll(includeProperties:"Category").ToList();  // the name ""Category"" should be same as in Repository.cs
           
            return View(productList);
        }

        public IActionResult Upsert(int? id)         //for update and create  
        {
            IEnumerable<SelectListItem> categoryDropDown = unitOfWork.Category.GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
            //                  //OR
            //ViewBag.CategoryDropDown = categoryDropDown;
            //                    //OR            
            //ViewData["CategoryList"]= categoryDropDown;
            //                    //OR
            ProductVM productVM = new()
            {
                Product = new Product(),
                CategoryList =categoryDropDown
            };
            if (id is null or 0)
            {
                return View(productVM);  // OR return View(new Product);   no need to specify as the compiler will itself create a new object of Product in the model of create.cshtml
            }
            productVM.Product = unitOfWork.Product.Get(c => c.Id == id); 
            return View(productVM);
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productVM,IFormFile? file)  //here file should have same name as in upsert.cshtml  name="file" and IFormFile is used to upload image
        {
            string webRootPath = webHostEnvironment.WebRootPath; //getting the path of wwwroot folder
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName); //generating unique name for image
                string productPath = Path.Combine(webRootPath, @"images\product"); //creating path for image

                if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                {
                    var oldImagePath = Path.Combine(webRootPath, productVM.Product.ImageUrl.TrimStart('\\')); //getting the path of image to delete it
                    if (System.IO.File.Exists(oldImagePath)) //checking if the image exists or not
                    {
                        System.IO.File.Delete(oldImagePath); //deleting the image
                    }
                }
                using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create)) //creating filestream to copy image to the path
                {
                    file.CopyTo(fileStream);
                }
                productVM.Product.ImageUrl = @"\images\product\" + fileName; //storing the path of image in database
            }

            if (productVM.Product.Id == 0)
            {
                unitOfWork.Product.Add(productVM.Product);

            }
            else
            {
                unitOfWork.Product.Update(productVM.Product);
            }
            //The above is written there because if we update the product then the image will be null and it will throw an error as we didnt set it to [ValidateNever] in Product.cs
            if (ModelState.IsValid) /*it checks Range and MaxLength is Valid or not and display error on create.cshtml using <span asp-validation-for="Name" class="text-danger"></span>*/
            {
                unitOfWork.Save();
                TempData["success"] = "Product created successfully"; //TempData is used to display message on the same page after redirecting to another page
                return RedirectToAction("Index", "Product"); //redirecting to index action of product controller ,can skip "product" as it is the same controller
            }

            productVM.CategoryList = unitOfWork.Category.GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
            return View(productVM);
        }

        public IActionResult Delete(int? id)
        {
            if (id is null or 0)
            {
                return NotFound();
            }
            var categoryToDelete = unitOfWork.Product.Get(c => c.Id == id);  //Find() is used to find for only primary key
            if (categoryToDelete == null)
            {
                return NotFound();
            }
            return View(categoryToDelete); // OR return View(new Product);   no need to specify as the comiler will itself craete a new object of Product in the model of create.cshtml
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(Product product) //if we put Delete then name are same hence we need to put ActionName("Delete") so that form
        //will treat DeletePost method as Delete
        {
            unitOfWork.Product.Remove(product);
            unitOfWork.Save();
            TempData["success"] = "Product deleted successfully"; //TempData is used to display message on the same page after redirecting to another page

            return RedirectToAction("Index", "Product");
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = productList });
        }


        #endregion
    }
}
