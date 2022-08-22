using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class MoviesController : Controller
    {
        public IActionResult Details(int id)
        {
            //go to db and get the movie info by movid id
            //and send the data(Model) to the view
            //ADO.NET
            //Dapper StackOverflow -> Micro ORM
            //Entity Framework Core => Full ORM for Microsoft

            //Select * from Movies where id = 12;
            //Code is Maintenable, Reusable, Readable, Extensible, Testable
            //layers => Layered architecture
            //Onion, Clean Architecture
            

            return View();
        }
    }
}
