using System;
using Logic;

namespace Sokoban.Client
{
    public class SokobanMoveLeft
        : SokobanCommand
    {
        public SokobanMoveLeft(Pusher pusher) : base(pusher)
        {
        }

        public override bool Execute()
        {
            return this.Pusher.MoveLeft();
        }

        public override void Undo()
        {
            this.Pusher.MoveRight();
        }
    }
}
