using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logic;

namespace Sokoban.Client
{
    public abstract class SokobanCommand
        : IUndoCommand
    {
        protected Pusher Pusher;
        public SokobanCommand(Pusher pusher)
        {
            this.Pusher = pusher;
        }

        public abstract bool Execute();
        public abstract void Undo();
    }
}
