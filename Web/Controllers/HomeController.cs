using LocalFTPUploadProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Globalization;

namespace LocalFTPUploadProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            TreeNode[] initialNodes = new[]
               {
                    new TreeNode(  1, null, "Computers" ),
                    new TreeNode(  2,    1, "Desktops" ),
                    new TreeNode(  3,    1, "Notebooks" ),
                    new TreeNode(  4,    1, "Software" ),
                    new TreeNode(  5, null, "Electronics" ),
                    new TreeNode(  6,    5, "Camera photo" ),
                    new TreeNode(  7,    5, "Cell phones" ),
                    new TreeNode(  8,    5, "Others" ),
                    new TreeNode(  9, null, "Apparel" ),
                    new TreeNode( 10,    9, "Shoes" ),
                    new TreeNode( 11,    9, "Clothing" ),
                    new TreeNode( 12,   11, "Shirt" ),
                    new TreeNode( 13,   11, "TShirt" ),
                    new TreeNode( 14,    9, "Accessories" ),
                    new TreeNode( 15, null, "Downloads" ),
                    new TreeNode( 16, null, "Books" ),
                    new TreeNode( 17, null, "Jewelry" ),
                    new TreeNode( 18, null, "Gift Cards" ),
                        new TreeNode( 19, 7, "Gift Cards" )
                 };

            TreeGraph graph = new TreeGraph(initialNodes);

            // graph.EnumerateDepthFirst().Dump();

            ViewBag.Category = graph.AsSelectListItems().ToList();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

public record TreeNode(Int64 id, Int64? parentId, String name);

public class TreeGraph
{
    private readonly IReadOnlyDictionary<Int64, (TreeNode node, List<TreeNode> children)> nodesById;

    public TreeGraph(IEnumerable<TreeNode> initialNodes)
    {
        Dictionary<Int64, (TreeNode node, List<TreeNode> children)> nodesByIdMut = initialNodes
            .ToDictionary(n => n.id, n => (node: n, children: new List<TreeNode>()));

        foreach (TreeNode child in initialNodes.Where(n => n.parentId.HasValue))
        {
            nodesByIdMut[child.parentId!.Value].children.Add(child);
        }

        this.nodesById = nodesByIdMut;
    }

    private String GetPath(TreeNode node)
    {
        if (node.parentId == null) return node.name;

        Stack<TreeNode> stack = new Stack<TreeNode>();

        TreeNode? n = node;
        while (n != null)
        {
            stack.Push(n);

            n = n.parentId.HasValue ? (nodesById[n.parentId.Value].node) : null;
        }

        return String.Join(separator: " > ", stack.Select(n2 => n2.name));
    }

    private IEnumerable<TreeNode> GetRoots()
    {
        return this.nodesById.Values
            .Where(t => t.node.parentId == null)
            .OrderBy(t => t.node.name)
            .Select(t => t.node);
    }

    private IEnumerable<TreeNode> GetChildren(TreeNode node)
    {
        return this.nodesById[node.id].children;
    }

    public IEnumerable<(TreeNode n, String path)> EnumerateDepthFirst()
    {
        foreach (TreeNode root in this.GetRoots())
        {
            foreach ((TreeNode descendant, String path) pair in this.EnumerateDepthFirstFrom(root))
            {
                yield return pair;
            }
        }
    }

    private IEnumerable<(TreeNode n, String path)> EnumerateDepthFirstFrom(TreeNode root)
    {
        Stack<TreeNode> stack = new Stack<TreeNode>();
        stack.Push(root);

        while (stack.Count > 0)
        {
            TreeNode n = stack.Pop();
            foreach (TreeNode c in this.GetChildren(n))
            {
                stack.Push(c);
            }

            String path = this.GetPath(n);

            yield return (n, path);
        }
    }

    public IEnumerable<SelectListItem> AsSelectListItems()
    {
        return this.EnumerateDepthFirst()
            .Select(pair => new SelectListItem()
            {
                Text = pair.path,
                Value = pair.n.id.ToString(CultureInfo.InvariantCulture)
            });
    }
}