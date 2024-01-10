﻿using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.ViewModels;
using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController(IUnitOfWork unitOfWork) : Controller
    {
        public IActionResult Index()
        {
            var categoryList = unitOfWork.Product.GetAll().ToList();
           
            return View(categoryList);
        }

        public IActionResult Create()
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
            ProductVM productVM = new ProductVM
            {
                Product = new Product(),
                CategoryList =categoryDropDown
            };
            return
                View(productVM); // OR return View(new Product);   no need to specify as the compiler will itself create a new object of Product in the model of create.cshtml
        }

        [HttpPost]
        public IActionResult Create(ProductVM obj)
        {

            if (ModelState.IsValid) /*it checks Range and MaxLength is Valid or not and display error on create.cshtml using <span asp-validation-for="Name" class="text-danger"></span>*/
            {
                unitOfWork.Product.Add(obj.Product);
                unitOfWork.Save();
                TempData["success"] = "Product created successfully"; //TempData is used to display message on the same page after redirecting to another page
                return RedirectToAction("Index", "Product"); //redirecting to index action of product controller ,can skip "product" as it is the same controller

            }

            ProductVM productVM = new ProductVM
            {
                Product = new Product(),
                CategoryList = unitOfWork.Category.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };
            return View(productVM);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryToEdit = unitOfWork.Product.Get(c => c.Id == id);  //Find() is used to find for only primary key
            if (categoryToEdit == null)
            {
                return NotFound();
            }
            return View(categoryToEdit); // OR return View(new Product);   no need to specify as the comiler will itself craete a new object of Product in the model of create.cshtml
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {


            if (ModelState.IsValid) /*it checks Range and MaxLength is Valid or not and display error on create.cshml using <span asp-validation-for="Name" class="text-danger"></span>*/
            {
                unitOfWork.Product.Update(product);
                unitOfWork.Save();
                TempData["success"] = "Product updated successfully"; //TempData is used to display message on the same page after redirecting to another page

                return
                    RedirectToAction("Index",
                        "Product"); //redirecting to index action of product controller ,can skip "product" as it is the same controller
            }

            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
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
    }
}
