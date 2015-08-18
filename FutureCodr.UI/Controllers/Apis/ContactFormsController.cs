namespace FutureCodr.UI.Controllers.Apis
{
    using FutureCodr.Data.Interfaces;
    using FutureCodr.Models;
    using FutureCodr.UI.Models.Angular.contactForms;
    using System;
    using System.Collections.Generic;
    using System.Web.Http;

    public class ContactFormsController : ApiController
    {
        private readonly IContactFormRepository _contactFormRepo;

        public ContactFormsController(IContactFormRepository contactFormRepo)
        {
            _contactFormRepo = contactFormRepo;
        }

        public void Delete(int id)
        {
            _contactFormRepo.DeleteContactForm(id);
        }

        //gets all contact forms, converts their format and add to the 
        //view model to be displayed in the table
        public IEnumerable<ContactFormAng> Get()
        {
            List<ContactForm> allContactForms = _contactFormRepo.GetAllContactForms();
            List<ContactFormAng> model = new List<ContactFormAng>();
            foreach (ContactForm form in allContactForms)
            {
                ContactFormAng item = new ContactFormAng
                {
                    Name = form.Name,
                    Email = form.Email,
                    FullMessage = form.Message,
                    ContactFormID = form.ContactFormID
                };

                //cut message (only for display purposes) if it longer than 
                //50 chars
                if (form.Message.Length > 50)
                {
                    item.CutMessage = form.Message.Substring(0, 50);
                }
                else
                {
                    item.CutMessage = form.Message;
                }
                model.Add(item);
            }
            return model;
        }
    }
}
