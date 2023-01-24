/// <summary>
/// A command that makes changes to the level stored within the level builder when exectuted
/// </summary>
public interface ILevelBuilderCommand
{
    /// <summary>
    /// Executes the command
    /// </summary>
    void Execute(LevelData levelObj);

    /// <summary>
    /// Undoes any changes made by this command
    /// </summary>
    void Undo(LevelData levelObj);

}
