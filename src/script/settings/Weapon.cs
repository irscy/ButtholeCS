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

		CollisionShape2D hitbox;

		float swingDelay = 0.4f;
		int swingIndex;
		bool canSwing = true;

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
			swingTimer.Connect("timeout", this, "OnTimeoutComplete");
			AddChild(swingTimer);

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
		}

		//on timer timeout complete
		void OnTimeoutComplete()
		{
			canSwing = true;
		}

		//used to see if swing index is even or odd, then swing in the appropriate direction
		void RunSwing(float delta)
		{			
			if(Input.IsActionJustPressed("Swing Weapon") && canSwing && swingIndex % 2 == 0)
			{
				swingAnim.Stop(true);
				swingAnim.Play("SwingDown");
				swingIndex += 1;
				canSwing = false;	
				swingTimer.Start();				
			}
			else if(Input.IsActionJustPressed("Swing Weapon") && canSwing && swingIndex % 2 != 0)
			{
				swingAnim.Stop(true);
				swingAnim.Play("SwingUp");
				swingIndex += 1;
				swingTimer.Start();
				canSwing = false;				
			}
			if(swingIndex > 6)
			{
				swingIndex = 1;
			}
		}
	}
}








