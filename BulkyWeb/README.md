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
