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

            [Required]
            public int ContactID { get; set; }
            [Required]
            [MaxLength(50, ErrorMessage="No more than 50 characters")]
            public string FirstName { get; set; }
            [Required]
            [MaxLength(50, ErrorMessage = "No more than 50 characters")]
            public string LastName { get; set; }
            [Required]
            [DataType(DataType.EmailAddress,ErrorMessage="Not a valid email format")]
            public string EmailAddress { get; set; }
        }
    }
}