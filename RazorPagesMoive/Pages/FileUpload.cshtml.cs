using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesMoive.Model;

namespace RazorPagesMoive.Pages
{
	public class FileUploadModel : PageModel
    {
        public void OnGet()
        {
        }


		public async Task<IActionResult> OnPostAsync()
		{
			// Perform an initial check to catch FileUpload class attribute violations.
			if (!ModelState.IsValid)
			{
				return Page();
			}

			var filePath = "";

			//using (var fileStream = new FileStream(filePath, FileMode.Create))
			//{
			//	await FileUpload.UploadPublicSchedule.CopyToAsync(fileStream);
			//}

			return RedirectToPage("./Index");
		}

	}
}