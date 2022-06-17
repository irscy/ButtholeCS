using Butthole;
using Godot;

namespace Butthole.Settings
{
	class Enemy : Node2D
	{
		//fields
		Timer deathAnimWait;
		bool canPlayDeathAnim = false;
		public bool isDead;
		AudioStreamPlayer p;

		//children
		Sprite definedSprite;
		CollisionShape2D hitbox;
		AnimationPlayer deathAnim;
		Area2D floppa;
		Node2D path;

		public override void _Ready()
		{
			//children
			path = GetChild<Node2D>(0);
			floppa = path.GetChild<Area2D>(0);
			definedSprite = floppa.GetChild<Sprite>(0);
			hitbox = floppa.GetChild<CollisionShape2D>(1);
			deathAnim = floppa.GetChild<AnimationPlayer>(2);
			p = floppa.GetChild<AudioStreamPlayer>(3);

			//timer shit
			deathAnimWait = new Timer();
			deathAnimWait.WaitTime = 1.649f;
			deathAnimWait.Connect("timeout", this, "OnDeathWaitComplete");
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

		public void FixTransform()
		{
			definedSprite.Offset = new Vector2(-112, -230);
			definedSprite.Centered = false;
			Position = new Vector2(500, 400);

			hitbox.Position = new Vector2(-6, -58);

			path.Position = Vector2.Zero;
			((PathFollow2D)path).Offset = 0;
		}

		private void OnFloppaEnemyEnter(object area)
		{
			if(((Node2D)area).IsInGroup("Weapon") && !canPlayDeathAnim)
			{
				deathAnim.Play("Death");
				isDead = true;
				p.Play();
				deathAnimWait.Start();
			}
		}

		void OnDeathWaitComplete()
		{
			canPlayDeathAnim = true;
		}
	}
}
