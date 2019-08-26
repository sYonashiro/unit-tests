using Flunt.Notifications;
using Flunt.Validations;

namespace UnitTestsProject.Domain.ValueObjects
{
    public class Email : Notifiable
    {
        public Email(string address)
        {
            Address = address;

            AddNotifications(new Contract()
                .IsEmail(Address, $"{nameof(Email)}.{nameof(Address)}", "E-mail inválido."));
        }

        public string Address { get; private set; }
    }
}
