using Butthole;
using Godot;

namespace Butthole
{
	partial class LabelUnblur : RichTextLabel
	{
		Camera2D camera; 
		Node2D Main;

		public override void _Ready()
		{
			Main = GetTree().Root.GetNode<Node2D>("Main");
			camera = Main.GetNode<Camera2D>("Camera2D");		
		}
	}
}
