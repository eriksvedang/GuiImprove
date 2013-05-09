using System;
using UnityEngine;
using System.Collections.Generic;

public class InstructionPanel : Container
{	
	Label _instruction;
	Queue<string> _messages = new Queue<string>();
	bool _isShowing = false;
	Container _bg;
	
	public InstructionPanel (Vector2 pPosition) : base(pPosition)
	{
		_bg = new Container("help_Frame", new Vector2(-375/2, 0));
		this.AddChild(_bg);
		_instruction = new Label("transparent_textfield", "", new Vector2(25, 13));
		_instruction.customHitbox = new Rect(0, 0, 375-50, 50);
		_instruction.textAlignment = TextAnchor.MiddleCenter;
		_instruction.reactsToMouseClick = false;
		_bg.AddChild(_instruction);
	}
	
	public void ShowText(string pInstructionText) {
		if(_isShowing) {
			//D.Log("trying to show a message but already showing one so let's hide first");
			_messages.Enqueue(pInstructionText);
			Hide();
		}
		else {
			//D.Log("showing a message: " + pInstructionText);
			_instruction.text = pInstructionText;
			EasyAnimate<Vector2>.instance.Register(_bg, "a", new EasyAnimate<Vector2>.EasyAnimState(
	                Time.time, Time.time + 0.5f,
	                _bg.ScreenPosition,
	                _bg.ScreenPosition + new Vector2(0, -85),
	                iTween.vector2easeInOutSine.Sample,
	                _bg.SetScreenPosition)
	            );
			_isShowing = true;
		}
	}
	
	public void Hide() {
		if(_isShowing) {
			//D.Log("hide...");
			EasyAnimate<Vector2>.instance.Register(this, "a", new EasyAnimate<Vector2>.EasyAnimState(
	                Time.time, Time.time + 0.5f,
	                _bg.ScreenPosition,
	                _bg.ScreenPosition + new Vector2(0, 85),
	                iTween.vector2easeInOutSine.Sample,
	                _bg.SetScreenPosition, DoneHiding)
	            );
		}
	}
	
	private void DoneHiding() {
		_isShowing = false;
		if(_messages.Count > 0) {
			//D.Log("Done hiding and found a message: " + _messages.Peek());
			ShowText(_messages.Dequeue());
		} else {
			//D.Log("Done hiding and no more messages...");
		}
	}
}

