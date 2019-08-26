using Flunt.Notifications;
using Flunt.Validations;

namespace UnitTestsProject.Domain.ValueObjects
{
    public class Name : Notifiable
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            AddNotifications(new Contract()
                .IsNotNullOrEmpty(FirstName, $"{nameof(Name)}.{nameof(FirstName)}", "Nome inválido.")
                .IsNotNullOrEmpty(LastName, $"{nameof(Name)}.{nameof(LastName)}", "Sobrenome inválido."));
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
    }
}
