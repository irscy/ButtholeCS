using Godot;
using Butthole;

namespace Butthole
{
    class WeaponAnchor : Node2D
    {
        Node2D Jinx;
        Node2D JinxSprite;
        Node2D StickSprite;

        public override void _Ready()
        {
            Jinx = GetParent<Node2D>();
            JinxSprite = Jinx.GetChild<Sprite>(0);
            StickSprite = GetChild<Sprite>(0);
        }
        public override void _PhysicsProcess(float delta)
        {
            if(!((Sprite)JinxSprite).FlipH)
            {
                Position = new Vector2(-14.972f, -38);
                ((Sprite)StickSprite).FlipH = true;
                RotationDegrees = 11;
            }
        }
    }
}