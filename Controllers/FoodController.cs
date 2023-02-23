using Microsoft.AspNetCore.Mvc;
using Food_Blog.Models;

using Food_Blog.Data;
using Newtonsoft.Json;

namespace Food_Blog.Controllers
{
    public class FoodController : Controller
    {
        public readonly Food_Blog.Data.ApplicationDbContext _db;
        public FoodController(ApplicationDbContext db)
        {
            _db = db;
        }
        // HttpClientHandler _clientHandler = new HttpClientHandler();
        //public readonly Data.ApplicationDbContext _db;

        // public FoodController(ApplicationDbContext db)
        // {
        //     _db = db;
        //     _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
        //     {
        //         return true;
        //     };
        // }
        //List<Food> list = new List<Food>();

        public IActionResult Index()
        {
            var list = _db.Foods.ToList();
            return View(list);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Food b)
        {

            if (ModelState.IsValid)
            {
                _db.Add(b);
                _db.ChangeTracker.DetectChanges();
                System.Console.WriteLine(_db.ChangeTracker.DebugView.LongView);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(b);
        }

        public IActionResult AddIngredient()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddIngredient(Ingredient i)
        {
            if (ModelState.IsValid)
            {
                _db.Add(i);
                _db.ChangeTracker.DetectChanges();
                System.Console.WriteLine(_db.ChangeTracker.DebugView.LongView);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(i);
        }
        //  public IActionResult IngredientPage(int? id){
        //      if (id == null || id == 0)
        //     {
        //         return NotFound();
        //     }
        //     var selectedBook = _db.Ingredients.FirstOrDefault(x => x.FoodId == id);
        //     if (selectedBook == null)
        //     {
        //         return RedirectToAction("AddIngredient");
        //     }

        //     return View(selectedBook);
        // }

        //[HttpGet]
        HttpClient client = new HttpClient();
        //[HttpGet("{id}")]
        public async Task<IActionResult> IngredientPage(int? id)
        {
            Ingredient list = new Ingredient();
            client.BaseAddress = new Uri("http://localhost:5130/api/Ingredient");
            HttpResponseMessage response = await client.GetAsync("Ingredient/"+id);
            
            // var test = response.Result;
            if (response.IsSuccessStatusCode)
            {
                String display = await response.Content.ReadAsStringAsync();
                list = JsonConvert.DeserializeObject<Ingredient>(display);
                // display.Wait();
                // list = display.Result;
            }
            return View(list);
        }
    
    public IActionResult Update(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        // var selectedFood = _db.Foods.FirstOrDefault(x => x.Id == id);      
        var selectedFood = _db.Foods.Find(id);
        if (selectedFood == null)
        {
            return NotFound();
        }

        return View(selectedFood);
    }

    //Post
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(Food b)
    {

        if (ModelState.IsValid)
        {
            _db.Foods.Update(b);
            _db.SaveChanges();
            // var obj = list.FirstOrDefault(x => x.Id == b.Id);
            // if (obj != null) 
            //     obj.Name = b.Name;
            // foreach(var i in list)
            //    System.Console.WriteLine(i.Name);
            TempData["success"] = "Recipe Updated SuccessFully!";
            return RedirectToAction("Index");
        }
        return View(b);
    }
    //Get
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var selectedBook = _db.Foods.Find(id);
        if (selectedBook == null)
        {
            return NotFound();
        }

        return View(selectedBook);
    }

    //Post
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(Food b)
    {

        if (ModelState.IsValid)
        {
            _db.Remove(b);
            _db.SaveChanges();
            // var obj = list.FirstOrDefault(x => x.Id == b.Id);
            // if (obj != null) 
            //     obj.Name = b.Name;
            //foreach(var i in list)
            //    System.Console.WriteLine(i.Name);
            TempData["success"] = "Book Deleted SuccessFully!";
            return RedirectToAction("Index");
        }
        return View(b);
    }
    public IActionResult AddMore(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var selectedBook = _db.Ingredients.Find(id);
        if (selectedBook == null)
        {
            return NotFound();
        }

        return View(selectedBook);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddMore(Ingredient b)
    {

        if (ModelState.IsValid)
        {
            _db.Ingredients.Update(b);
            _db.SaveChanges();
            // var obj = list.FirstOrDefault(x => x.Id == b.Id);
            // if (obj != null) 
            //     obj.Name = b.Name;
            //foreach(var i in list)
            //    System.Console.WriteLine(i.Name);
            TempData["success"] = "Recipe Updated SuccessFully!";
            return RedirectToAction("Index");
        }
        return View(b);
    }
    public IActionResult GetRecipe(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var selectedBook = _db.Foods.Find(id);
        if (selectedBook == null)
        {
            return NotFound();
        }

        return View(selectedBook);
    }

}


}
