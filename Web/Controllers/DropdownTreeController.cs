using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class DropdownTreeController : Controller
    {
        public IActionResult Index()
        {

            List<GetDropDownTreeSelect> initialNodes = new List<GetDropDownTreeSelect>();
            initialNodes.Add(new GetDropDownTreeSelect { Id = 1 , Text = "Computers", parentId = null,Selected = false, Disabled = false});
            initialNodes.Add(new GetDropDownTreeSelect { Id = 2, Text = "Desktops", parentId = null, Selected = false, Disabled = false });
            initialNodes.Add(new GetDropDownTreeSelect { Id = 3, Text = "Notebooks", parentId = null, Selected = false, Disabled = false });
            initialNodes.Add(new GetDropDownTreeSelect { Id = 4, Text = "Software", parentId = null, Selected = false, Disabled = false });
            initialNodes.Add(new GetDropDownTreeSelect { Id = 5, Text = "Electronics", parentId = null, Selected = false, Disabled = false });
            initialNodes.Add(new GetDropDownTreeSelect { Id = 6, Text = "Camera photo", parentId = null, Selected = false, Disabled = false });
            initialNodes.Add(new GetDropDownTreeSelect { Id = 7, Text = "Cell phones", parentId = null, Selected = false, Disabled = false });
            initialNodes.Add(new GetDropDownTreeSelect { Id = 8, Text = "Others", parentId = null, Selected = false, Disabled = false });
            initialNodes.Add(new GetDropDownTreeSelect { Id = 9, Text = "Apparel", parentId = null, Selected = false, Disabled = false });
            initialNodes.Add(new GetDropDownTreeSelect { Id = 10, Text = "Shoes", parentId = null, Selected = false, Disabled = false });
            initialNodes.Add(new GetDropDownTreeSelect { Id = 11, Text = "Clothing", parentId = null, Selected = false, Disabled = false });
            initialNodes.Add(new GetDropDownTreeSelect { Id = 12, Text = "Shirt", parentId = null, Selected = false, Disabled = false });
            initialNodes.Add(new GetDropDownTreeSelect { Id = 13, Text = "TShirt", parentId = null, Selected = false, Disabled = false });
            initialNodes.Add(new GetDropDownTreeSelect { Id = 14, Text = "Accessories", parentId = null, Selected = false, Disabled = false });
            initialNodes.Add(new GetDropDownTreeSelect { Id = 15, Text = "Downloads", parentId = null, Selected = false, Disabled = false });
            initialNodes.Add(new GetDropDownTreeSelect { Id = 16, Text = "Books", parentId = null, Selected = false, Disabled = false });
            initialNodes.Add(new GetDropDownTreeSelect { Id = 17, Text = "Jewelry", parentId = null, Selected = false, Disabled = false });
            initialNodes.Add(new GetDropDownTreeSelect { Id = 18, Text = "Gift Cards", parentId = null, Selected = false, Disabled = false });
            initialNodes.Add(new GetDropDownTreeSelect { Id = 19, Text = "Gift Cards", parentId = null, Selected = false, Disabled = false });
         


            return View();
        }
    }
    public class GetDropDownTreeSelect
    {
        public int Id { get; set; }
        public Nullable<int> parentId { get; set; }
        public string Text { get; set; }
        public bool Selected { get; set; }
        public bool Disabled { get; set; }


    }

}
