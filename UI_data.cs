using Godot;
using System;

public partial class UI_data : Label
{
	private Label fishtext;
	public int fishcount;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		fishtext = GetNode<Label>("UI_fish");
		fishcount = 0;
		fishtext.Text = fishcount.ToString();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
	public void addfish()
	{
		fishcount += 1;
		fishtext.Text = fishcount.ToString();
	}
}
