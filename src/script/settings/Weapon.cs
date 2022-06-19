using Godot;
using Butthole;

namespace Butthole.Settings
{
	class Weapon : Node2D
	{
		//fields
		Node2D spriteChild;
		Node2D weaponHolder;

		AnimatedSprite weaponHolderSprite;

		AnimationPlayer swingAnim;

		Timer swingTimer;
		Timer animBoxEnable;

		CollisionShape2D hitbox;

		float swingDelay = 0.4f;
		int swingIndex;
		bool canSwing = true;
		bool hitboxEnabled = false;

		public override void _Ready()
		{
			//setting object values
			swingAnim = GetChild<AnimationPlayer>(1);
			swingIndex = 1;
			spriteChild = GetChild<Sprite>(0);
			weaponHolder = GetParent<Node2D>();
			weaponHolderSprite = weaponHolder.GetChild<AnimatedSprite>(0);
			hitbox = GetChild<CollisionShape2D>(2);

			//timer shit
			swingTimer = new Timer();
			swingTimer.OneShot = true;
			swingTimer.WaitTime = swingDelay;
			swingTimer.Connect("timeout", this, "OnSwingTimeoutComplete");
			AddChild(swingTimer);

			animBoxEnable = new Timer();
			animBoxEnable.OneShot = true;
			animBoxEnable.WaitTime = swingTimer.WaitTime; //big brain move btw
			animBoxEnable.Connect("timeout", this, "OnHitboxEnableComplete");
			AddChild(animBoxEnable);

			//default position
			Position = new Vector2(-24, -40);
			RotationDegrees = 11;
		}

		public override void _PhysicsProcess(float delta)
		{
			//calling methods with float delta
			RunSwing(delta);

			if(swingAnim.IsPlaying())
			{
				hitbox.Disabled = false;
			}
			else
			{
				hitbox.Disabled = true;
			}

			if(hitboxEnabled)
			{
				hitbox.Disabled = false;
			}
			else
			{
				hitbox.Disabled = true;
			}
		}

		//on timer timeout complete
		void OnSwingTimeoutComplete()
		{
			canSwing = true;
		}

		void OnHitboxEnableComplete()
		{
			hitboxEnabled = false;
		}

		//used to see if swing index is even or odd, then swing in the appropriate direction
		void RunSwing(float delta)
		{			
			if(Input.IsActionJustPressed("Swing Weapon") && canSwing && !hitboxEnabled && swingIndex % 2 == 0)
			{
				swingAnim.Stop(true);
				swingAnim.Play("SwingDown");
				swingIndex += 1;
				canSwing = false;	
				hitboxEnabled = true;
				animBoxEnable.Start();
				swingTimer.Start();				
			}
			else if(Input.IsActionJustPressed("Swing Weapon") && canSwing && !hitboxEnabled && swingIndex % 2 != 0)
			{
				swingAnim.Stop(true);
				swingAnim.Play("SwingUp");
				swingIndex += 1;
				canSwing = false;	
				hitboxEnabled = true;
				swingTimer.Start();
				animBoxEnable.Start();
			}
			if(swingIndex > 6)
			{
				swingIndex = 1;
			}
		}
	}
}








