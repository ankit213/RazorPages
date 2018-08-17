using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RazorPagesMoive.Model;

namespace RazorPagesMoive.Pages
{
	public class IndexModel : PageModel
	{
		private readonly AppDbContext _db;
		private readonly MyValues _myValues;
		public IndexModel(AppDbContext db, IOptionsMonitor<MyValues> values)
		{
			_db = db;
			_myValues = values.CurrentValue;
		}

		public IList<Customer> Customers { get; private set; }
		public string SnapshotOptions { get; private set; }

		public string FileContents { get; private set; }

		public async Task OnGetAsync()
		{
			SnapshotOptions = _myValues.DefaultValue;

			Customers = await _db.Customers.OrderBy(x=>x.Name).ToListAsync();
		}

		public async Task<IActionResult> OnPostDeleteAsync(int id)
		{
			var contact = await _db.Customers.FindAsync(id);

			if (contact != null)
			{
				_db.Customers.Remove(contact);
				await _db.SaveChangesAsync();
			}

			return RedirectToPage();
		}
	}
}
