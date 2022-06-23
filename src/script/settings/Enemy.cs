using Butthole;
using Godot;

namespace Butthole.Settings
{
	class Enemy : Node2D
	{
		//fields
		AudioStreamPlayer p;
		Timer DeathAnimWait;
		Timer HitCooldown;
		Node2D LookTarget;
		bool CanPlayDeathAnim = false;
		bool IsDead;
		bool Flipped;
		bool CanGetHit = true;
		int HP;
		[Export] public string LookTargetPath;

		//children
		Sprite DefinedSprite;
		CollisionShape2D Hitbox;
		AnimationPlayer Anims;
		Area2D CoreNPC;
		Node2D Path;
		Control UI;
		Label HPCounter;


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
			UpdateHPCounter();

			//after death cycle is completed, destroy the enemy
			if (CanPlayDeathAnim && IsDead)
			{
				Free();
			}	

			if(HP < 0)
			{
				HP = 0;
			}

			//play death cycle when epic die
			if(HP <= 0 && !IsDead)
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

			Hitbox.Position = new Vector2(-9, 0);

			CoreNPC.RotationDegrees = 0;

			Path.Position = Vector2.Zero;
			((PathFollow2D)Path).Offset = 0;
		}

		//when a weapon hits the enemy, play the death cycle
		private void OnFloppaEnemyEnter(object area)
		{
			var WeaponAnim = ((Node2D)area).GetChild<AnimationPlayer>(1);
			
			if (((Node2D)area).IsInGroup("Weapon") && !CanPlayDeathAnim && CanGetHit)
			{
				HP -= 1;
				HitCooldown.Start();
				CanGetHit = false;
				GD.Print(HP);
			}
		}

		//look at the target (pretty obvious)
		void LookAtTarget()
		{
			if(LookTarget.Position.x > Position.x && !IsDead)
			{
				Scale = new Vector2(-1.5f, 1.5f);
				Flipped = true;
			}
			else if(LookTarget.Position.x < Position.x && !IsDead)
			{
				Scale = new Vector2(1.5f, 1.5f);
				Flipped = false;
			}

			if(Flipped)
			{
				HPCounter.RectScale = new Vector2(-1, 1);
				HPCounter.RectPosition = new Vector2(10.982f, -87);
			}
			else if(!Flipped)
			{
				HPCounter.RectScale = new Vector2(1, 1);
				HPCounter.RectPosition = new Vector2(-27, -87);
			}
		}

		void PlayDeathCycle()
		{
			Anims.Play("Death");
			IsDead = true;
			DeathAnimWait.Start();
		}

		void UpdateHPCounter()
		{
			HPCounter.Text = $"{HP.ToString()}";
		}

		//after timer is complete, set CanPlayDeathAnim back to true
		void OnDeathWaitComplete()
		{
			CanPlayDeathAnim = true;
		}

		//enemy gets to have a cooldown before it can get hit again
		void OnHitWaitComplete()
		{
			CanGetHit = true;
		}

		//set values of objects and fields (it's here for cleanliness)
		void SetObjectValues()
		{
			//children
			Path = GetChild<Node2D>(0);
			CoreNPC = Path.GetChild<Area2D>(0);
			DefinedSprite = CoreNPC.GetChild<Sprite>(0);
			Hitbox = CoreNPC.GetChild<CollisionShape2D>(1);
			Anims = CoreNPC.GetChild<AnimationPlayer>(2);
			p = CoreNPC.GetChild<AudioStreamPlayer>(3);
			UI = Path.GetChild<Control>(1);
			HPCounter = UI.GetChild<Label>(0);
			
			//other
			LookTarget = GetNode<Node2D>(LookTargetPath);
			HP = 4;

			//timer shit
			DeathAnimWait = new Timer();
			DeathAnimWait.WaitTime = 1.8f;
			DeathAnimWait.Connect("timeout", this, "OnDeathWaitComplete");
			AddChild(DeathAnimWait);

			HitCooldown = new Timer();
			HitCooldown.WaitTime = 0.3f;
			HitCooldown.OneShot = true;
			HitCooldown.Connect("timeout", this, "OnHitWaitComplete");
			AddChild(HitCooldown);
		}
	}
}
