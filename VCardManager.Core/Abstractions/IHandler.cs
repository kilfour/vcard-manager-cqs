namespace VCardManager.Core.Abstractions;


public interface ICommand { }

public interface IHandler { }

public interface IHandler<TCommand> : IHandler
    where TCommand : ICommand
{
    void ExecuteCommand(TCommand command);
}



