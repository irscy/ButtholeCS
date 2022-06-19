using Godot;
using Butthole;

namespace Butthole.Settings
{
	class Weapon : Node2D
	{
		//fields
		Node2D SpriteChild;
		Node2D WeaponHolder;

		AnimatedSprite WeaponHolderSprite;

		AnimationPlayer SwingAnim;

		Timer SwingTimer;
		Timer AnimBoxEnable;

		CollisionShape2D Hitbox;

		float SwingDelay = 0.4f;
		int SwingIndex;
		bool CanSwing = true;
		bool HitboxEnabled = false;

		public override void _Ready()
		{
			//setting object values
			SwingAnim = GetChild<AnimationPlayer>(1);
			SwingIndex = 1;
			SpriteChild = GetChild<Sprite>(0);
			WeaponHolder = GetParent<Node2D>();
			WeaponHolderSprite = WeaponHolder.GetChild<AnimatedSprite>(0);
			Hitbox = GetChild<CollisionShape2D>(2);

			//timer shit
			SwingTimer = new Timer();
			SwingTimer.OneShot = true;
			SwingTimer.WaitTime = SwingDelay;
			SwingTimer.Connect("timeout", this, "OnSwingTimeoutComplete");
			AddChild(SwingTimer);

			AnimBoxEnable = new Timer();
			AnimBoxEnable.OneShot = true;
			AnimBoxEnable.WaitTime = SwingTimer.WaitTime; //big brain move btw
			AnimBoxEnable.Connect("timeout", this, "OnHitboxEnableComplete");
			AddChild(AnimBoxEnable);

			//default position
			Position = new Vector2(-24, -40);
			RotationDegrees = 11;
		}

		public override void _PhysicsProcess(float delta)
		{
			//calling methods with float delta
			RunSwing(delta);

			if(SwingAnim.IsPlaying())
			{
				Hitbox.Disabled = false;
			}
			else
			{
				Hitbox.Disabled = true;
			}

			if(HitboxEnabled)
			{
				Hitbox.Disabled = false;
			}
			else
			{
				Hitbox.Disabled = true;
			}
		}

		//on timer timeout complete
		void OnSwingTimeoutComplete()
		{
			CanSwing = true;
		}

		void OnHitboxEnableComplete()
		{
			HitboxEnabled = false;
		}

		//used to see if swing index is even or odd, then swing in the appropriate direction
		void RunSwing(float delta)
		{			
			if(Input.IsActionJustPressed("Swing Weapon") && CanSwing && !HitboxEnabled && SwingIndex % 2 == 0)
			{
				SwingAnim.Stop(true);
				SwingAnim.Play("SwingDown");
				SwingIndex += 1;
				CanSwing = false;	
				HitboxEnabled = true;
				AnimBoxEnable.Start();
				SwingTimer.Start();				
			}
			else if(Input.IsActionJustPressed("Swing Weapon") && CanSwing && !HitboxEnabled && SwingIndex % 2 != 0)
			{
				SwingAnim.Stop(true);
				SwingAnim.Play("SwingUp");
				SwingIndex += 1;
				CanSwing = false;	
				HitboxEnabled = true;
				SwingTimer.Start();
				AnimBoxEnable.Start();
			}
			if(SwingIndex > 6)
			{
				SwingIndex = 1;
			}
		}
	}
}








