using Godot;
using Butthole;

namespace Butthole
{
    class WeaponAnchor : Node2D
    {
        Node2D Jinx;
        Node2D JinxSprite;

        public override void _Ready()
        {
            Jinx = GetParent<Node2D>();
            JinxSprite = Jinx.GetChild<Sprite>(0);
        }
        public override void _Process(float delta)
        {
            
        }
    }
}