using BulkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_Temp.Pages.Categories
{
    public class EditModel(ApplicationDbContext context) : PageModel
    {

        [BindProperty]
        public Category categoryToEdit { get; set; }
        public void OnGet(int? id)
        {
            if (id != null || id != 0)
            {
                categoryToEdit = context.Categories.Find(id);
            }
            
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                
                context.Categories.Update(categoryToEdit);
                context.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
