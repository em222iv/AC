using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdventurousContacts.Models
{
    [MetadataType(typeof(Contact_Metadata))]
    public partial class Contact
    {
        public class Contact_Metadata
        {
            public int ContactID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }

            public string EmailAddress { get; set; }
        }
    }
}