using System.Net.Mime;
using BulkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;

namespace BulkyWebRazor_Temp.Pages.Categories
{
    public class IndexModel(ApplicationDbContext context) : PageModel
    {
        public List<Category> CategoryList { get; set; }

        public void OnGet()
        {
            CategoryList = context.Categories.ToList(); //no return needed it will automatically bind this by using @model BulkyWebRazor_Temp.Pages.Categories.IndexModel in index.cshtml
        }

   
    }
}
