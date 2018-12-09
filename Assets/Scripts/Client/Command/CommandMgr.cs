using System.Collections.Generic;

namespace Sokoban.Client
{
    public class CommandMgr
    {
        public int CommandCount { get; private set; }
        private Stack<IUndoCommand> CommandStack;
        public CommandMgr()
        {
            this.CommandStack = new Stack<IUndoCommand>();
        }

        public void Execute(IUndoCommand cmd)
        {
            var r = cmd.Execute();
            if (r)
            {
                ++this.CommandCount;
                this.CommandStack.Push(cmd);
            }
        }

        public void Undo()
        {
            ++this.CommandCount;
            var cmd = this.CommandStack.Pop();
            cmd.Undo();
        }
    }
}
