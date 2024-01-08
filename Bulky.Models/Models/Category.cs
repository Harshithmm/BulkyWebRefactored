
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models
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
//for range we need to add the below code in ApplicationDBContext.cs

//protected override void OnModelCreating(ModelBuilder modelBuilder)
//{
//    modelBuilder.Entity<Category>(entity =>
//    {
//        entity.Property(e => e.Name)
//            .HasMaxLength(30)
//            .IsRequired();

//        entity.Property(e => e.DisplayOrder)
//            .HasConstraintName("CK_Category_DisplayOrder_Range")
//            .HasCheckConstraint("CK_Category_DisplayOrder_Range", "[DisplayOrder] BETWEEN 1 AND 100");
//    });
//}
