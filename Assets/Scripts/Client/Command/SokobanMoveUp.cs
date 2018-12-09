using Logic;

namespace Sokoban.Client
{
    public class SokobanMoveUp
        : SokobanCommand
    {
        public SokobanMoveUp(Pusher pusher) : base(pusher)
        {
        }

        public override bool Execute()
        {
            return this.Pusher.MoveUp();
        }

        public override void Undo()
        {
            this.Pusher.MoveDown();
        }
    }
}
