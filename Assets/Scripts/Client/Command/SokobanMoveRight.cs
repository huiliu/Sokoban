using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logic;

namespace Sokoban.Client
{
    public class SokobanMoveRight
        : SokobanCommand
    {
        public SokobanMoveRight(Pusher pusher) : base(pusher)
        {
        }

        public override bool Execute()
        {
            return this.Pusher.MoveRight();
        }

        public override void Undo()
        {
            this.Pusher.MoveLeft();
        }
    }
}
