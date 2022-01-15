using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionEx
{
    public class ContactInfo : IEquatable<ContactInfo>
    {
        public string EmailAddress { get; set; }
        public string PhoneNo { get; set; }
        public bool Equals(ContactInfo other)
        {
            return other == null ? false : EmailAddress == other.EmailAddress && PhoneNo == other.PhoneNo;
        }
    }
}
