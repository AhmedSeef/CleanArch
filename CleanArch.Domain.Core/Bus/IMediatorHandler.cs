using CleanArch.Domain.Core.Commands;

namespace CleanArch.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
    }
}
