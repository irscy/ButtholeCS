using Godot;
using System;
using Butthole;

namespace Butthole.Nodes
{
	class MainScene : Jinx
	{
		Node2D jinx = new Node2D();
		public override void _Ready()
		{
			jinx = GetNode<Node2D>("/root/Main/JINX");
		}
	}
}
