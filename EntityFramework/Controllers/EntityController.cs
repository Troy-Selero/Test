using EntityFramework.Data;
using EntityFramework.Models;
using Microsoft.AspNetCore.Mvc;

namespace EntityFramework.Controllers
{
	public class EntityController : Controller
	{
		MVCContext _dbContext;

		public EntityController(MVCContext dbContext) {
			_dbContext = dbContext;
		}

		public IActionResult Index() {
			IEnumerable<EntityTPA> entityTPAs = _dbContext.EntityTPAs.Select(s => s).ToList();

			return View(entityTPAs);
		}

		public IActionResult Delete(long id) {
			EntityTPA entityTPA = _dbContext.EntityTPAs.FirstOrDefault(s => s.EntityTPAID == id);

			if (entityTPA != null) {
				_dbContext.Remove(entityTPA);
				_dbContext.SaveChanges();

				return RedirectToAction("Index");
			}

			return View();
		}
	}
}
