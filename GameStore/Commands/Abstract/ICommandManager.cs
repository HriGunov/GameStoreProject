namespace GameStore.Commands.Abstract
{
    public interface ICommandManager
    {
        ICommand FindCommand(string commandName);
        string Execute(string commandLine);
    }
}