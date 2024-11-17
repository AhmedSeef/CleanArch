using CleanArch.Domain.Core.Events;

namespace CleanArch.Domain.Core.Commands
{
    public class Command : Message
    {
        public DateTime TimeStamp { get; protected set; }
        public Command()
        {
            TimeStamp = DateTime.Now;
        }
    }
}
