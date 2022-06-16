using Butthole;
using Godot;

namespace Butthole.Settings
{
	class Enemy : Node2D
	{
		//fields
		Timer deathAnimWait;
		bool canPlayDeathAnim = false;
		bool isDead;

		//children
		Sprite definedSprite;
		CollisionShape2D hitbox;
		AnimationPlayer deathAnim;

		//parents
		PathFollow2D path;
		Path2D pathParent;

		public override void _Ready()
		{
			//assignments to nodes
			definedSprite = GetChild<Sprite>(0);
			hitbox = GetChild<CollisionShape2D>(1);
			deathAnim = GetChild<AnimationPlayer>(2);

			path = GetParent<PathFollow2D>();
			pathParent = path.GetParent<Path2D>();

			//timer shit
			deathAnimWait = new Timer();
			deathAnimWait.WaitTime = 0.7f;
			deathAnimWait.Connect("timeout", this, "OnTimeoutComplete");
			AddChild(deathAnimWait);

			deathAnim.Stop(true);

			FixTransform();
		}

		public override void _PhysicsProcess(float delta)
		{
			if(canPlayDeathAnim && isDead)
			{
				Free();
			}
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
			if(((Node2D)area).IsInGroup("Weapon") && !canPlayDeathAnim)
			{
				deathAnim.Play("Death");
				isDead = true;
				deathAnimWait.Start();
			}
		}

		void OnTimeoutComplete()
		{
			canPlayDeathAnim = true;
		}
	}
}


