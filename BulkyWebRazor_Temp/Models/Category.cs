using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BulkyWebRazor_Temp.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }  //If we use Id or CategoryId, it will be automatically treated as primary key no need to use [Key] attribute
        [Required]
        [MaxLength(30)] //max characters in name is 30 not in the database level for database level if migrated then at the Db level  [Column(TypeName = "nvarchar(30)")]
        [DisplayName("Category Name")]   //USED FOR CLIENT SIDE DISPLAY OF NAMES used in category/create.cshtml in asp-for=Name here it will display the DisplayName value

        public string Name { get; set; }

        [DisplayName("Display Order")]
        [Range(1, 100)]  //custom error message[Range(1, 100, ErrorMessage = "Display Order for category must be between 1 and 100")]e can be given in the view page
        public int DisplayOrder { get; set; }
    }
}
