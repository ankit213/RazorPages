using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RazorPagesMoive.Model;
using RazorPagesMoive.Service;

namespace RazorPagesMoive.Pages
{
	public class IndexModel : PageModel
	{
		private readonly AppDbContext _db;
		//private readonly IConfiguration _config;
		//private readonly IConfigurationMonitor _monitor;
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

			Customers = await _db.Customers.AsNoTracking().ToListAsync();
			//var fileContent = await _fileService.GetFileContents("poem.txt");
			//FileContents = fileContent.Replace("\r\n", "<br>");

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


		//#region snippet2
		//public IActionResult OnPostStartMonitoring()
		//{
		//	_monitor.MonitoringEnabled = true;
		//	_monitor.CurrentState = "Monitoring!";

		//	return RedirectToPage();
		//}

		//public IActionResult OnPostStopMonitoring()
		//{
		//	_monitor.MonitoringEnabled = false;
		//	_monitor.CurrentState = "Not monitoring";

		//	return RedirectToPage();
		//}
		//#endregion
	}
}
