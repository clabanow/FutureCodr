using FutureCodr.Models;
using System;
using System.Collections.Generic;

namespace FutureCodr.Data.Interfaces
{
    public interface IContactFormRepository
    {
        List<ContactForm> GetAllContactForms();
        ContactForm GetContactFormById(int id);
        ContactForm AddContactForm(ContactForm form);
        void DeleteContactForm(int id);
    }
}
