﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesMoive.Model;

namespace RazorPagesMoive.Pages
{
	public class ContactModel : PageModel
    {

		private readonly AppDbContext _db;

		public ContactModel(AppDbContext db)
		{
			_db = db;
		}

		[BindProperty]
		public Customer Customer { get; set; }

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			_db.Customers.Add(Customer);
			await _db.SaveChangesAsync();
			return RedirectToPage("/Index");
		}
    }
}
