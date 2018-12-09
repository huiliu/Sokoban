using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logic;

namespace Sokoban.Client
{
    public class SokobanStartGame
        : SokobanCommand
    {
        public SokobanStartGame(Pusher pusher) : base(pusher)
        {
        }

        public override bool Execute()
        {
            return true;
        }

        public override void Undo()
        {
        }
    }
}
