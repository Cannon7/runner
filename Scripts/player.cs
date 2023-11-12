using Godot;
using System;
public partial class player : CharacterBody2D
{
	UI_data UI = new UI_data();
	private AnimatedSprite2D Char_anims;
	private Sprite2D alert;
	private Timer timer;
	public const float Speed = 150.0f;
	public bool istimeover = true;
	public bool canfish = false;
	private RandomNumberGenerator rng = new RandomNumberGenerator();
	private bool fishresult = false;
	
	public override void _Ready()
	{
		Char_anims = GetNode<AnimatedSprite2D>("Fisherman_sprites");
		timer = GetNode<Timer>("Timer");
		alert = GetNode<Sprite2D>("Alert");
		alert.Visible = false;
	}
	public override void _PhysicsProcess(double delta)
	{
		if ((Input.IsActionJustReleased("ui_select")) && (canfish))
		{
			fishattempt();
		}
		Vector2 velocity = Velocity;
		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Y = direction.Y * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
		}
		_UpdateSprite(velocity.X, velocity.Y);

		Velocity = velocity;
		MoveAndSlide();
	}
	private void fishattempt()
	{
		GD.Print("fish attempt");
		istimeover = false;
		timer.Start(rng.RandiRange(1, 5));
		while (istimeover != true)
		{
			Char_anims.Play("fishing");
		}
		_minigame();
	}
	private void _minigame()
	{
		istimeover = false;
		alert.Visible = true;
		timer.Start(1);
		while (istimeover != true)
		{
			if (Input.IsActionJustReleased("ui_select"))
			{
				UI.addfish();
				GD.Print("fish landed");
				Char_anims.Play("hook");
			}
		}
		alert.Visible = false;
		istimeover = false;
		timer.Start(1);
		while (istimeover != true)
		{
		}
	}
	private void _UpdateSprite(float velx, float vely)
	{
		bool walking = (velx != 0) || (vely != 0);
		if (walking)
		{
			if(vely >= 0)
			{
				Char_anims.Play("walk");
			}
			else
			{
				Char_anims.Play("walk_back");
			}
			Char_anims.FlipH = velx < 0;
		}
		else
		{
			Char_anims.Play("idle");
		}
	}
	private void _on_timer_timeout()
	{
		istimeover = true;
	}
	private void _on_dock_body_entered(Node2D body)
	{
		canfish = true;
		GD.Print("in");
	}
	private void _on_dock_body_exited(Node2D body)
	{
		canfish = false;
		GD.Print("out");
	}
}

