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

			//fix transform of first spawn
			FixTransform();
		}

		public override void _PhysicsProcess(float delta)
		{
			//after death cycle is completed, destroy the enemy
			if (canPlayDeathAnim && isDead)
			{
				Free();
			}	
		}

		//change node values on spawn to make sure they spawn in the right place
		public void FixTransform()
		{
			definedSprite.Offset = new Vector2(-112, -230);
			definedSprite.Centered = false;
			Position = new Vector2(400, 400);

			hitbox.Position = new Vector2(-6, -58);

			path.Position = Vector2.Zero;
			((PathFollow2D)path).Offset = 0;
		}

		//when a weapon hits the enemy, play the death cycle
		private void OnFloppaEnemyEnter(object area)
		{
			var weaponAnim = ((Node2D)area).GetChild<AnimationPlayer>(1);
			
			if (((Node2D)area).IsInGroup("Weapon") && !canPlayDeathAnim)
			{
				deathAnim.Play("Death");
				isDead = true;
				deathAnimWait.Start();
			}
		}

		//after timer is complete, set canPlayDeathAnim back to true
		void OnDeathWaitComplete()
		{
			canPlayDeathAnim = true;
		}
	}
}
