using Godot;
using System;

public partial class FloatingDamage : Node2D
{
	private Label DamageLabel;
	private Timer LifeTimer;

	public int Damage = 0;

	public override void _Ready()
    {
		DamageLabel = GetNode<Label>("Label");
		LifeTimer = GetNode<Timer>("Timer");
        LifeTimer.Timeout += () => QueueFree();
		GlobalPosition += new Vector2(new RandomNumberGenerator().RandfRange(-10.0f, 10.0f), 0);
		DamageLabel.Text = Damage.ToString();
	}

	public override void _Process(double delta)
    {
		GlobalPosition -= new Vector2(0, (float)delta*10);
    }
}
