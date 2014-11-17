using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data;
using System.Linq;
using System.Web;

namespace AdventurousContacts.Models.Repository
{
    public class EFRepository : IRepository
    {

        private readonly Entities _entities = new Entities();

            public IEnumerable<Contact> GetContacts()
            {
                return _entities.Contact.ToList();
            }

            public Contact GetContactsById(int id)
            {
                return _entities.Contact.Find(id);
            }

            public void InsertContact(Contact contact)
            {
                _entities.Contact.Add(contact);
            }

            public void UpdateContact(Contact contact)
            {
                if (_entities.Entry(contact).State == EntityState.Detached)
                {
                    _entities.Contact.Attach(contact);
                }

                _entities.Entry(contact).State = EntityState.Modified;
            }

            public void DeleteContact(Contact contact)
            {
                if (_entities.Entry(contact).State == EntityState.Detached)
                {
                    _entities.Contact.Attach(contact);


                    _entities.Contact.Remove(contact);
                }
            }

            public void Save()
            {
                _entities.SaveChanges();
            }

            #region IDisposable

            private bool disposed = false;

            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {
                        _entities.Dispose();
                    }
                }
                this.disposed = true;
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            #endregion
        }
    }
