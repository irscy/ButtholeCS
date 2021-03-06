using Godot;
using Butthole;

namespace Butthole
{
	partial class HitboxDebug :  Sprite2D
	{
		CollisionShape2D Hitbox;

		public override void _Ready()
		{
			Hitbox = GetParent<CollisionShape2D>();
		}

		public override void _PhysicsProcess(float delta)
		{
			if(Hitbox.Disabled)
			{
				Visible = false;
			}
			else if(!Hitbox.Disabled)
			{
				Visible = true;
			}
		}
	}
}
