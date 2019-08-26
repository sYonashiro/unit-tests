using System;
using System.Linq;
using System.Text.RegularExpressions;
using UnitTestsProject.Domain.ValueObjects;

namespace UnitTestsProject.Domain.Entities
{
    public class Customer : Entity
    {
        public Customer(Guid id, Name name, Document document, Email email, string phone) : base(id)
        {
            Name = name;
            Document = document;
            Email = email;
            Phone = phone;

            SetNotifications();
        }

        public Name Name { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public string Phone { get; private set; }

        private void SetNotifications()
        {
            if (Name.Invalid)
                AddNotification(Name.Notifications.First().Property, Name.Notifications.First().Message);

            else if (Document.Invalid)
                AddNotification(Document.Notifications.First().Property, Document.Notifications.First().Message);

            else if (Email.Invalid)
                AddNotification(Email.Notifications.First().Property, Email.Notifications.First().Message);

            else if (!IsValidPhone(Phone))
                AddNotification("Phone", "Telefone inválido.");
        }

        private bool IsValidPhone(string phone)
        {
            var regex = new Regex(@"(\(?\d{2}\)?\s)?(\d{4,5}\-\d{4})");

            return regex.Match(phone).Success;
        }
    }
}
