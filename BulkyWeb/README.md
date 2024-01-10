wwwroot:all the static contents are added here including images etc

Views have a folder called Home the same name as the controller name here controller name is Home

_Layout.cshtml:  here @RenderBody()  is a helper method that is used to display something that is coming from the controller

parial views are somthing that cannot be displayed by themselves and they are used inside some kind of main view

the application knows which is the master view by _ViewStart.cshtml file where master view is defined

whenever u add something to the service in builder.service in pgm.cs we are that way we are adding it to dependency injection it will create objects etc and give the context

public IActionResult Index()
{
    var categoryList=context.Categories.ToList();
    return View(categoryList);
}
in here we are passing categoryList to the view and in index.cshtml of view

@model List<Category>

where we are receiving the model model should be lowercase and we can use c# here like
@model List<Category>
<h1>Category List</h1>

<table class="table table-bordered table-striped">
    <thead>
    <tr>
        <th>
            Category Name
        </th>
        <th>
            Display Order
        </th>
    </tr>
    </thead>
    <tbody>
    @foreach (var obj in Model)
    {
        <tr>
            <td>@obj.Name</td>
            <td>@obj.DisplayOrder</td>
        </tr>
    }
    </tbody>
</table>

where NOTE:<td> should have c# code to display obj.Name hence @ is USED.


[DisplayName("Category Name")]   //USED FOR CLIENT SIDE DISPLAY OF NAMES used in category/index.cshtml in asp-for=Name here it will display the DisplayName value

return RedirectToAction("Index","Category");  //redirecting to index action of category controller

for singleton,transient and scoped 4:45:00 in video

    internal class CategoryRepository : Repository<Category>, ICategoryRepository  //HERE Repository<Category> is used so that it doesnot show implement missing members
                                                                                   //error bcz of ICategoryRepository interface as it has been implemented in  Repository<Category> and  then 
                                                                                    //we get another error bcz Repository<Category> needs ApplicationDbContext context in its constructor


@model Category 
@*@model BulkyWeb.Models.Category or move _ViewImports.cshtml  if its the last commit and  error bcz it doesnot know where is the category as its defined in the _ViewImports.cshtml *@



******************************************************************************************************************************

USE VIEWBAG AND VIEWDATA TO DISPLAY DROPDOWN LIST IN CREATE.CSHTML

         Using ViewBag
        <div class="mb-3 row p-1">
            <label asp-for="Product.CategoryId" class="p-0"></label> @* used Display Order bcz no space was there in asp-for="DisplayOrder" *@

            <select asp-for="Product.CategoryId" asp-items="ViewBag.CategoryDropDown" class="form-select border-0 shadow">
                @* ViewBag name should be same*@
                <option disabled selected>--Select Category--</option>
            </select>
            <span asp-validation-for="Product.CategoryId" class="text-danger"></span>
        </div>
        Using ViewData
        <div class="mb-3 row p-1">
            <label asp-for="Product.CategoryId" class="p-0"></label> @* used Display Order bcz no space was there in asp-for="DisplayOrder" *@

            <select asp-for="Product.CategoryId" asp-items="@ViewData["CategoryList"] as IEnumerable<SelectListItem>" class="form-select border-0 shadow">
                @* ViewBag name should be same*@
                <option disabled selected>--Select Category--</option>
            </select>
            <span asp-validation-for="Product.CategoryId" class="text-danger"></span>
        </div>



        IN PRODUCT CONTROLLER 

                    ViewBag.CategoryDropDown = categoryDropDown;
                                //OR            
            ViewData["CategoryList"]= categoryDropDown;



******************************************************************************************************************************

<form method="post" class="row" enctype="multipart/form-data">          @* enctype="multipart/form-data" is used for uploading the image otherwise it won't work*@

******************************************************************************************************************************

            <h2 class="text-primary"> @(Model.Product.Id!=0?"Update":"Create")Create Product</h2>
used to display create or update based on the id of the product dynamically