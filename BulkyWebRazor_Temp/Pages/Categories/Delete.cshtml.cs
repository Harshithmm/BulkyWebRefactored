using BulkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_Temp.Pages.Categories
{
    public class DeleteModel(ApplicationDbContext context) : PageModel
    {
        [BindProperty]
        public Category CategoryToDelete { get; set; }
        public void OnGet(int? id)
        {
            CategoryToDelete = context.Categories.Find(id);
        }
        public IActionResult OnPost()
        {

            context.Categories.Remove(CategoryToDelete);
            context.SaveChanges();
            return RedirectToPage("Index");

        }
    }
}
