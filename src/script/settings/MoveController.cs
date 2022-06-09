using Godot;
using Butthole;

namespace Butthole.Settings
{
	class MoveController : Node2D
	{
		/* 
		NOTES (IN ORDER):
		--------visual--------
		- add MoveController script under a node and set MoveController object's value to that node
		- set definedNode 's value to a node
		- add sprite under definedNode as child index 0
		- instance FlipDirSquish scene under sprite as child index 0
		- instance FlipDirUp scene under sprite as child index 1
		-------with object-----
		- set defined node (definedNode)
		- set defined sprite (definedNode_sprite)
		- set animation values (FlipDirSquish, FLipDirUp)
		- call SetObjectValues under _Ready()
		*/

		//objects
		public Node2D definedNode { get; set; }
		public Sprite definedNode_Sprite { get; set; }

		[Export] public Texture upSpr { get; set; }
		[Export] public Texture downSpr { get; set; }
		[Export] public Texture horizSpr { get; set; }

		public AnimationPlayer FlipDirSquish { get; set; } 
		public AnimationPlayer FlipDirUp { get; set; }

		Vector2 xSpeed;
		Vector2 ySpeed;

		//variables
		bool left;
		bool right;
		bool canPlayAnim;

		public void SetObjectValues()
		{	
			canPlayAnim = true;
			xSpeed = new Vector2(200, 0);
			ySpeed = new Vector2(0, 200);
		}
		
		public override void _Ready()
		{
			
		}

		public void EnableMoveControls(float delta)
		{
			switch(this)
			{
				//LEFT MOVEMENT
				//on press
				case {} when Input.IsActionPressed("Move Left") && !right:
					definedNode.GlobalPosition -= xSpeed * delta;
					definedNode_Sprite.Texture = horizSpr;
					definedNode_Sprite.FlipH = false;
					left = true;
					if(canPlayAnim)
					{
						FlipDirSquish.Stop(true);
						FlipDirSquish.Play("FlipDirSquish");
					}
					canPlayAnim = false;
					break;

				//on release
				case {} when Input.IsActionJustReleased("Move Left"):
					left = false;
					canPlayAnim = true;
					break;
				
				//RIGHT MOVEMENT
				//on press
				case {} when Input.IsActionPressed("Move Right") && !left:
					definedNode.GlobalPosition += xSpeed * delta;
					definedNode_Sprite.Texture = horizSpr;
					definedNode_Sprite.FlipH = true;

					right = true;
					if(canPlayAnim)
					{
						FlipDirSquish.Stop(true);
						FlipDirSquish.Play("FlipDirSquish");
					}
					canPlayAnim = false;
					break;
				
				//on release
				case {} when Input.IsActionJustReleased("Move Right"):
					right = false;
					canPlayAnim = true;
					break;

				case {} when Input.IsActionPressed("Move Up"):
					definedNode.GlobalPosition -= ySpeed * delta;		
					definedNode_Sprite.Texture = upSpr;
					break;		
			}
		} 
	}
}
