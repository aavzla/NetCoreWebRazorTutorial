using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace NetCoreWebRazorTutorial.Pages
{
	public class IndexModel : PageModel
	{
		//We want to have the DB protected from the view. we set up private for access and readonly for setting only one time and just be readable.
		private readonly AppDBContext _appDBContext;

		//This property will be a temp data for the PageModel but is not part of the DB.
		//We will use this for show a message to the user after the creation of a customer.
		[TempData]
		public string Message { get; set; }

		public IList<Customer> Customers { get; private set; }

		//We create the constructor, so we can have set up the DB.
		public IndexModel(AppDBContext appDBContext)
		{
			_appDBContext = appDBContext;
			Customers = new List<Customer>();
		}

		//The methods have the prefix On<Verb><KeyWord>.
		//The Verb is the HTTP protocol used: Get, Post, etc.
		//The KeyWord is the Key for identify witch method is targeted.
		//If the process is Async, it should be on the end.

		public async Task OnGetAsync()
		{
			Customers = await _appDBContext.Customers.AsNoTracking().ToListAsync();
		}

		//In this case, we use the post protocol and use the delete as a keyword.
		public async Task<IActionResult> OnPostDeleteAsync(int id)
		{
			Customer customer = await _appDBContext.Customers.FindAsync(id);
			if (customer != null)
			{
				_appDBContext.Customers.Remove(customer);
				await _appDBContext.SaveChangesAsync();
			}
			return RedirectToPage();
		}
	}
}
