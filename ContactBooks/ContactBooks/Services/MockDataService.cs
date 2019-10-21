using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBooks
{
    public class MockDataService : IContactDataService
    {
        private IEnumerable<Contact> _contacts;
        public MockDataService()
        {
            _contacts = new List<Contact>()
            {
                new Contact
                {
                    Name = "Emircan Aydın",
                    PhoneNumbers = new string[]
                    {
                        "555-111-1111",
                        "555-222-222"
                    },
                    Emails = new string[]
                    {
                        "aydnemrcn5@gmail.com",
                        "emircan.aydin@menulux.com"
                    },
                    Locations = new string[]
                    {
                        "Teknopark",
                        "Kent Konut 3"
                    }

                },

                new Contact
                {
                    Name = "Ümit Emre Aydın",
                    PhoneNumbers = new string[]
                    {
                        "555-111-0000",
                        "555-222-4444"
                    },
                    Emails = new string[]
                    {
                        "umitemre9@gmail.com",
                        "emre.aydin@menulux.com"
                    },
                    Locations = new string[]
                    {
                        "Teknopark",
                        "Kent Konut 3"
                    }

                }
            };
        }

        public IEnumerable<Contact> GetContacts()
        {
            return _contacts;
        }

        public void Save(IEnumerable<Contact> contacts)
        {
            _contacts = contacts;
        }
    }
}
