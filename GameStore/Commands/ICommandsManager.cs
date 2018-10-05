namespace GameStore.Commands
{
    public interface ICommandsManager
    {
        ICommand FindCommand(string commandName);
        string Execute(string commandLine);
    }
}