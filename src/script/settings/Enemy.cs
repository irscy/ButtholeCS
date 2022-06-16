using Butthole;
using Godot;

namespace Butthole.Settings
{
	class Enemy : Node2D
	{
		//children
		Sprite definedSprite;
		CollisionShape2D hitbox;
		AnimationPlayer deathAnim;

		//parents
		PathFollow2D path;
		Path2D pathParent;

		public override void _Ready()
		{
			definedSprite = GetChild<Sprite>(0);
			hitbox = GetChild<CollisionShape2D>(1);
			deathAnim = GetChild<AnimationPlayer>(2);

			path = GetParent<PathFollow2D>();
			pathParent = path.GetParent<Path2D>();

			FixTransform();
		}

		void FixTransform()
		{
			definedSprite.Offset = new Vector2(-112, -230);
			definedSprite.Centered = false;

			hitbox.Position = new Vector2(-6, -58);

			path.Position = Vector2.Zero;
			path.Offset = 0;
		}

		private void OnFloppaEnemyEnter(object area)
		{
			if(((Node2D)area).IsInGroup("Weapon"))
			{
				deathAnim.Play("Death");
			}
		}
	}
}


