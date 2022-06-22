using Butthole;
using Godot;

namespace Butthole.Settings
{
	class Enemy : Node2D
	{
		//fields
		Timer DeathAnimWait;
		Node2D LookTarget;
		bool CanPlayDeathAnim = false;
		bool IsDead;
		int hp;
		[Export] public string LookTargetPath;
		AudioStreamPlayer p;

		//children
		Sprite DefinedSprite;
		CollisionShape2D Hitbox;
		AnimationPlayer Anims;
		Area2D CoreNPC;
		Node2D Path;

		public override void _Ready()
		{
			//set values of fields
			SetObjectValues();
			Anims.Stop(true);
			

			//fix transform of first spawn
			FixTransform();
		}

		public override void _PhysicsProcess(float delta)
		{
			LookAtTarget();
			//after death cycle is completed, destroy the enemy
			if (CanPlayDeathAnim && IsDead)
			{
				Free();
			}	

			if(hp <= 0 && !IsDead)
			{
				PlayDeathCycle();
			}
		}

		//change node values on spawn to make sure they spawn in the right place
		public void FixTransform()
		{
			DefinedSprite.Offset = new Vector2(0, 0);
			DefinedSprite.Centered = true;
			DefinedSprite.Texture = ResourceLoader.Load<Texture>("res://src/sprite/static/floppaNPC/npc_floppa.png");

			Position = new Vector2(512, 400);

			Hitbox.Position = new Vector2(-6, -58);

			CoreNPC.RotationDegrees = 0;

			Path.Position = Vector2.Zero;
			((PathFollow2D)Path).Offset = 0;
		}

		//when a weapon hits the enemy, play the death cycle
		private void OnFloppaEnemyEnter(object area)
		{
			var WeaponAnim = ((Node2D)area).GetChild<AnimationPlayer>(1);
			
			if (((Node2D)area).IsInGroup("Weapon") && !CanPlayDeathAnim)
			{
				hp -= 1;
				GD.Print(hp);
			}
		}

		void LookAtTarget()
		{
			if(LookTarget.Position.x > Position.x && !IsDead)
			{
				Scale = new Vector2(-1.5f, 1.5f);
			}
			else if(LookTarget.Position.x < Position.x && !IsDead)
			{
				Scale = new Vector2(1.5f, 1.5f);
			}
		}

		void PlayDeathCycle()
		{
			Anims.Play("Death");
			IsDead = true;
			DeathAnimWait.Start();
		}

		//after timer is complete, set CanPlayDeathAnim back to true
		void OnDeathWaitComplete()
		{
			CanPlayDeathAnim = true;
		}

		void SetObjectValues()
		{
			//children
			Path = GetChild<Node2D>(0);
			CoreNPC = Path.GetChild<Area2D>(0);
			DefinedSprite = CoreNPC.GetChild<Sprite>(0);
			Hitbox = CoreNPC.GetChild<CollisionShape2D>(1);
			Anims = CoreNPC.GetChild<AnimationPlayer>(2);
			p = CoreNPC.GetChild<AudioStreamPlayer>(3);
			
			//other
			LookTarget = GetNode<Node2D>(LookTargetPath);
			hp = 4;

			//timer shit
			DeathAnimWait = new Timer();
			DeathAnimWait.WaitTime = 1.8f;
			DeathAnimWait.Connect("timeout", this, "OnDeathWaitComplete");
			AddChild(DeathAnimWait);
		}
	}
}
