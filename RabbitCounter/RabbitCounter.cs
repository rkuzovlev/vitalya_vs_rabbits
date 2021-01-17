using Godot;
using System;

public class RabbitCounter : Node2D
{
	private RichTextLabel _label;
	private int _count = 0;
	
	public override void _Ready()
	{
		_label = GetNode<RichTextLabel>("count");
		
		SetCount(_count);
	}

	public void SetCount(int count)
	{
		_count = count;
		
		if (_label != null)
		{
			_label.Text = count.ToString();
		}
	}
}
