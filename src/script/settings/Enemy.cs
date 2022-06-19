using Butthole;
using Godot;

namespace Butthole.Settings
{
	class Enemy : Node2D
	{
		//fields
		Timer DeathAnimWait;
		bool CanPlayDeathAnim = false;
		bool IsDead;
		[Export] string LookTarget;
		AudioStreamPlayer p;

		//children
		Sprite definedSprite;
		CollisionShape2D hitbox;
		AnimationPlayer deathAnim;
		Area2D coreNPC;
		Node2D path;

		public override void _Ready()
		{
			//set values of fields lol
			SetObjectValues();
			deathAnim.Stop(true);

			//fix transform of first spawn
			FixTransform();
		}

		public override void _PhysicsProcess(float delta)
		{
			//after death cycle is completed, destroy the enemy
			if (CanPlayDeathAnim && IsDead)
			{
				Free();
			}	
		}

		//change node values on spawn to make sure they spawn in the right place
		public void FixTransform()
		{
			definedSprite.Offset = new Vector2(-112, -230);
			definedSprite.Centered = false;
			definedSprite.Texture = ResourceLoader.Load<Texture>("res://src/sprite/static/floppaNPC/npc_floppa.png");

			Position = new Vector2(400, 400);

			hitbox.Position = new Vector2(-6, -58);

			coreNPC.RotationDegrees = 0;

			path.Position = Vector2.Zero;
			((PathFollow2D)path).Offset = 0;
		}

		//when a weapon hits the enemy, play the death cycle
		private void OnFloppaEnemyEnter(object area)
		{
			var weaponAnim = ((Node2D)area).GetChild<AnimationPlayer>(1);
			
			if (((Node2D)area).IsInGroup("Weapon") && !CanPlayDeathAnim)
			{
				deathAnim.Play("Death");
				IsDead = true;
				DeathAnimWait.Start();
			}
		}

		//after timer is complete, set CanPlayDeathAnim back to true
		void OnDeathWaitComplete()
		{
			CanPlayDeathAnim = true;
		}

		void SetObjectValues()
		{
			//children
			path = GetChild<Node2D>(0);
			coreNPC = path.GetChild<Area2D>(0);
			definedSprite = coreNPC.GetChild<Sprite>(0);
			hitbox = coreNPC.GetChild<CollisionShape2D>(1);
			deathAnim = coreNPC.GetChild<AnimationPlayer>(2);
			p = coreNPC.GetChild<AudioStreamPlayer>(3);

			//timer shit
			DeathAnimWait = new Timer();
			DeathAnimWait.WaitTime = 1.8f;
			DeathAnimWait.Connect("timeout", this, "OnDeathWaitComplete");
			AddChild(DeathAnimWait);
		}
	}
}
