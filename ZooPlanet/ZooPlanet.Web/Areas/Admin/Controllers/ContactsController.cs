namespace ZooPlanet.Web.Areas.Admin.Controllers
{
    using ZooPlanet.Common.Constants;
    using ZooPlanet.Services.Admin;
    using ZooPlanet.Web.Areas.Admin.Models.Contacts;

    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using ZooPlanet.Web.Infrastructure.Filters;
    using ZooPlanet.Web.Infrastructure.Extensions;
    using ZooPlanet.Web.Controllers;
    using ZooPlanet.Web.Models;

    public class ContactsController : BaseAdminController
    {
        private readonly IContactAdminService contacts;

        public ContactsController(IContactAdminService contacts)
        {
            this.contacts = contacts;
        }

        [HttpGet]
        public async Task<IActionResult> All(int id = 1)
        {
            var contacts = await this.contacts.All(id);

            var contactsCount = await this.contacts.CountAsync();

            var model = new ContactsListingViewModel
            {
                Contacts = contacts,
                CurrentPage = id,
                ContactsCount = contactsCount,
                PagesCount = (int)Math.Ceiling(contactsCount / (decimal)WebConstants.ContactsPerPage)
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var contact = await this.contacts.ById(id);

            if (contact == null)
            {
                return NotFound();
            }

            var model = new ContactViewModel
            {
                Id = contact.Id,
                Title = contact.Title,
                Message = contact.Message,
                ContactType = contact.ContactType
            };

            return View(model);
        }

        [HttpPost]
        [Log]
        public async Task<IActionResult> Edit(ContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var contact = await this.contacts.ById(model.Id);

            if (contact == null)
            {
                return NotFound();
            }

            await this.contacts.Edit(model.Id, model.Title, model.Message, model.ContactType);

            TempData.AddSuccessMessage($"Successfully edited contact {contact.Title}.");

            return this.RedirectToCustomAction(
                nameof(HomeController.Index),
                nameof(HomeController),
                new { area = string.Empty });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var contact = await this.contacts.ById(id);

            if (contact == null)
            {
                return NotFound();
            }

            var model = new DeleteContactViewModel
            {
                Id = id,
                Title = contact.Title
            };

            return View(model);
        }

        [HttpPost]
        [Log]
        public async Task<IActionResult> Destroy(int id)
        {
            var contact = await this.contacts.ById(id);

            if (contact == null)
            {
                return NotFound();
            }

            await this.contacts.Delete(id);

            TempData.AddSuccessMessage("Successfully deleted a contact.");

            return this.RedirectToCustomAction(
               nameof(HomeController.Index),
               nameof(HomeController),
               new { area = string.Empty });
        }
    }
}