using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace NetCoreWebRazorTutorial.Pages
{
    public class EditModel : PageModel
    {
		//We want to have the DB protected from the view. we set up private for access and readonly for setting only one time and just be readable.
		private readonly AppDBContext _appDBContext;

		//We set up the DB Model as a property for access purposes.
		//BindProperty data attribute allows to bind the property of the model to the razor page form.
		[BindProperty]
		public Customer Customer { get; set; }

		//We create the constructor, so we can have set up the DB.
		public EditModel(AppDBContext appDBContext)
		{
			_appDBContext = appDBContext;
		}

		public async Task<IActionResult> OnGetAsync(int id)
		{
			Customer = await _appDBContext.Customers.FindAsync(id);
			if (Customer == null)
			{
				return RedirectToPage("/Index");
			}
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			_appDBContext.Attach(Customer).State = EntityState.Modified;

			try
			{
				await _appDBContext.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException ex)
			{
				throw new Exception($"The customer {Customer.Id} not found!", ex);
			}

			return RedirectToPage("/Index");
		}
    }
}