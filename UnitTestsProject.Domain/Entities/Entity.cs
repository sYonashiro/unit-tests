using Flunt.Notifications;
using System;

namespace UnitTestsProject.Domain.Entities
{
    public abstract class Entity : Notifiable
    {
        protected Entity(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
