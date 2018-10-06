namespace GameStore.Commands
{
    public interface ICommandManager
    {
        ICommand FindCommand(string commandName);
        string Execute(string commandLine);
    }
}