using Logic;

namespace Sokoban.Client
{
    public class SokobanMoveDown
        : SokobanCommand
    {
        public SokobanMoveDown(Pusher pusher) : base(pusher)
        {
        }

        public override bool Execute()
        {
            return this.Pusher.MoveDown();
        }

        public override void Undo()
        {
            this.Pusher.MoveUp();
        }
    }
}
