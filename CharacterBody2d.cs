using Godot;
using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace MyGame
{
	public partial class CharacterBody2d : CharacterBody2D
	{
		private Timer AttackTimer;
		private AnimationPlayer Animation;

		private bool IsAttacking = false;

		public string DirectionToAttack = "Right";
		public const float Speed = 300.0f;
		public const float JumpVelocity = -400.0f;

		public override void _Ready()
		{
			AttackTimer = GetNode<Timer>("AttackTimer");
			Animation = GetNode<AnimationPlayer>("AnimationPlayer");
			Animation.AnimationFinished += AnimationFinished;
		}

		private void AnimationFinished(string AnimationName = "")
		{
			IsAttacking = false;
		}

		public override void _Process(double delta)
		{
			if (Input.IsActionJustPressed("action_attack"))
			{
				if (!IsAttacking)
				{
					IsAttacking = true;
					Animation.Play($"Attack{DirectionToAttack}");
				}
			}
		}


		public override void _PhysicsProcess(double delta)
		{
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
				DirectionToAttack = "Down";
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
}