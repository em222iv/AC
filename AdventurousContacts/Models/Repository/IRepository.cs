using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdventurousContacts.Models.Repository
{
    public interface IRepository : IDisposable
    {
         IEnumerable<Contact> GetContacts();
        Contact GetContactsById(int id);
        void InsertContact(Contact contact);
        void UpdateContact(Contact contact);
        void DeleteContact(Contact contact);
        void Save();
    }
}