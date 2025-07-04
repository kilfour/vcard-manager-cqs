using VCardManager.Core.Abstractions;

namespace VCardManager.Core;

public interface IDispatcher
{
    void Dispatch<TCommand>(TCommand command) where TCommand : ICommand;
}


public class Dispatcher : IDispatcher
{
    private readonly Dictionary<Type, IHandler> handlers = [];

    public Dispatcher Register<TCommand>(IHandler<TCommand> handler)
        where TCommand : ICommand
    {
        handlers[typeof(TCommand)] = handler;
        return this;
    }



    public void Dispatch<TCommand>(TCommand command) where TCommand : ICommand
    {
        ((IHandler<TCommand>)handlers[typeof(TCommand)]).ExecuteCommand(command);
    }
}
