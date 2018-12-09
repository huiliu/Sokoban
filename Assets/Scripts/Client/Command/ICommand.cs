
namespace Sokoban.Client
{
    public interface ICommand
    {
        bool Execute();
    }

    public interface IUndoCommand : ICommand
    {
        void Undo();
    }
}
