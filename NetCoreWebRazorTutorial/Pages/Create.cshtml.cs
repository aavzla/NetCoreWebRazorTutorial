using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace NetCoreWebRazorTutorial.Pages
{
    public class CreateModel : PageModel
    {
		//We want to have the DB protected from the view. we set up private for access and readonly for setting only one time and just be readable.
		private readonly AppDBContext _appDBContext;

		//This property will be a temp data for the PageModel but is not part of the DB.
		//We will use this for show a message to the user after the creation of a customer.
		//Its important to have the TempData, so the value will be pass on to the next view.
		//And also, the properties in this page and the next page should have the same name so it will bind.
		[TempData]
		public string Message { get; set; }

		//We set up the DB Model as a property for access purposes.
		//BindProperty data attribute allows to bind the property of the model to the razor page form.
		[BindProperty]
		public Customer Customer { get; set; }

		//This is the private logger for this page.
		private ILogger<CreateModel> _log;

		//We create the constructor, so we can have set up the DB.
		public CreateModel(AppDBContext appDBContext, ILogger<CreateModel> log)
		{
			_appDBContext = appDBContext;
			_log = log;
		}

		public async Task<IActionResult> OnPostAsync()
        {
			if (!ModelState.IsValid)
			{
				return Page();
			}

			_appDBContext.Customers.Add(Customer);
			await _appDBContext.SaveChangesAsync();
			Message = $"Customer {Customer.Name} added!";
			_log.LogInformation(Message);
			return RedirectToPage("/Index");
        }
    }
}