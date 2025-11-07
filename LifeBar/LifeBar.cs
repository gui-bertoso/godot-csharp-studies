using Godot;
using System;

public partial class LifeBar : Control
{
	private Sprite2D LifeSprite;

	public override void _Ready()
	{
		LifeSprite = GetNode<Sprite2D>("Sprite2D");

		UpdateLifeIcon();
	}

	private void UpdateLifeIcon()
	{
		LifeSprite.Frame = 5 - Globals.Life;
	}

	public override void _Process(double delta)
	{
	}
}
