using MVC_assignment.Models;
using MVC_assignment.Repositories;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVC_assignment.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IContactRepository _repo;

        // Parameterless constructor for classic ASP.NET MVC
        public ContactsController() : this(new ContactRepository())
        {
        }

        // Constructor for manual dependency injection (e.g., testing)
        public ContactsController(IContactRepository repo)
        {
            _repo = repo;
        }

        public async Task<ActionResult> Index()
        {
            var contacts = await _repo.GetAllAsync();
            return View(contacts);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                await _repo.CreateAsync(contact);
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(long id)
        {
            await _repo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
