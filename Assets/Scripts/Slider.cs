using System;
using UnityEngine;

public class Slider : Container
{
	public delegate void OnSliderChanged(Slider pSlider);
	
	float _value;
	Container _adjustor;
	float maxWidth = 190f;
	bool _held = false;
	OnSliderChanged _onSliderChanged;
	public bool triggerChangeOnlyOnMouseUp = true;
	
	public float Value {
		get { return _value; }
		set { 
			_value = Mathf.Clamp(value, 0f, 1f);
			_adjustor.RelativePosition = new Vector2(maxWidth * _value, 0);
			RefreshChildScreenPositions();
		}
	}
	
	public Slider (Vector2 pPosition, OnSliderChanged pOnSliderChanged) : base("transparent_textfield", pPosition)
	{
		_onSliderChanged = pOnSliderChanged;
		_adjustor = new Container("lobby_SettingsAdjustor", new Vector2(0, 0));
		_adjustor.reactsToMouseClick = false;
		AddChild(_adjustor);
		this.customHitbox = new Rect(0, 0, maxWidth + 25, 20);
		this.Value = 0.75f;
	}
	
	public override void Press ()
	{
		_held = true;
		Update();
	}
	
	public override void Update ()
	{
		base.Update ();
		
		if(Input.GetMouseButtonUp(0) && _held) {
			_held = false;
			if(triggerChangeOnlyOnMouseUp) {
				_onSliderChanged(this);
			}
		}
		
		float prevValue = this.Value;
		
		if(_held) {
			float dx = Scaled.MousePos.x - this.ScreenPosition.x - (_adjustor.width / 2);
			float fraction = dx / maxWidth;
			this.Value = fraction;
		}
		
		if(prevValue != this.Value && !triggerChangeOnlyOnMouseUp) {
			_onSliderChanged(this);
		}
	}
}

