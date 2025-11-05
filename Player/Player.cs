using Godot;
using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

public partial class Player : Entity
{
	private Timer AttackTimer;
	private AnimationPlayer Animation;

	private bool IsAttacking = false;

	private string DirectionToAttack = "Right";

	[Export]
	private float Speed = 300.0f;

	[Export]
	private float JumpVelocity = -400.0f;

	public override void _Ready()
	{
		base._Ready();
		AttackTimer = GetNode<Timer>("AttackTimer");
		Animation = GetNode<AnimationPlayer>("AnimationPlayer");
		Animation.AnimationFinished += AnimationFinished;
	}

	private Vector2 GetKnockbackFromDirection(string direction)
	{
		switch (direction)
		{
			case "Up":
				return new Vector2(0, -1);
			case "Bottom":
				return new Vector2(0, 1);
			case "Left":
				return new Vector2(-1, 0);
			case "Right":
				return new Vector2(1, 0);
			default:
				return Vector2.Zero;
		}
	}

	private void AnimationFinished(StringName AnimationName)
	{
		IsAttacking = false;
	}

    public override void _Process(double delta)
    {
        base._Process(delta);
    }

	
	public override void _PhysicsProcess(double delta)
	{
        base._PhysicsProcess(delta);
		if (Input.IsActionJustPressed("action_attack"))
		{
			if (!IsAttacking)
			{
				IsAttacking = true;
				Animation.Play($"Attack{DirectionToAttack}");
				GD.Print($"Attacking{DirectionToAttack}");
				Velocity += GetKnockbackFromDirection(DirectionToAttack) * 200;
			}
		}
		
		Vector2 velocity = Velocity;

		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		if (Input.IsActionJustPressed("action_jump") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		float direction = Input.GetAxis("move_left", "move_right");
		if (direction != 0)
		{
			velocity.X = direction * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		if (Input.IsActionPressed("attack_up"))
		{
			DirectionToAttack = "Up";
		}
		else if (Input.IsActionPressed("attack_down"))
		{
			DirectionToAttack = "Bottom";
		}
		else if (Input.IsActionPressed("attack_left"))
		{
			DirectionToAttack = "Left";
		}
		else if (Input.IsActionPressed("attack_right"))
		{
			DirectionToAttack = "Right";
		}
		Velocity = velocity;
		MoveAndSlide();
	}
}
