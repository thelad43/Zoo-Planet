﻿namespace ZooPlanet.Web.Areas.Admin.Controllers
{
    using ZooPlanet.Common.Constants;
    using ZooPlanet.Data.Models.Enums;
    using ZooPlanet.Services.Admin;
    using ZooPlanet.Web.Areas.Admin.Models.Contacts;
    using ZooPlanet.Web.Controllers;
    using ZooPlanet.Web.Infrastructure.Extensions;
    using ZooPlanet.Web.Infrastructure.Filters;

    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    public class ContactsController : BaseAdminController
    {
        private readonly IContactAdminService contacts;

        public ContactsController(IContactAdminService contacts)
        {
            this.contacts = contacts;
        }

        [HttpGet]
        public async Task<IActionResult> All(int id = 1, ContactFilter filter = ContactFilter.All)
        {
            var contacts = await this.contacts.AllAsync(id, filter);

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
            var contact = await this.contacts.ByIdAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            var model = new ContactAdminViewModel
            {
                Id = contact.Id,
                Title = contact.Title,
                Message = contact.Message,
                ContactType = contact.ContactType,
                IsAnswered = contact.IsAnswered
            };

            return View(model);
        }

        [HttpPost]
        [Log]
        public async Task<IActionResult> Edit(ContactAdminViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var contact = await this.contacts.ByIdAsync(model.Id);

            if (contact == null)
            {
                return NotFound();
            }

            await this.contacts.EditAsync(model.Id, model.Title, model.Message, model.ContactType, model.IsAnswered);

            TempData.AddSuccessMessage($"Successfully edited contact {contact.Title}.");

            return this.RedirectToCustomAction(
                nameof(HomeController.Index),
                nameof(HomeController),
                new { area = string.Empty });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var contact = await this.contacts.ByIdAsync(id);

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
            var contact = await this.contacts.ByIdAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            await this.contacts.DeleteAsync(id);

            TempData.AddSuccessMessage("Successfully deleted a contact.");

            return this.RedirectToCustomAction(
               nameof(HomeController.Index),
               nameof(HomeController),
               new { area = string.Empty });
        }
    }
}