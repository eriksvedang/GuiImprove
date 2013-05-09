using System;
using UnityEngine;

public class ColorPicker : Container
{
	private Color _color = Color.white;
	private Container _colorSelector;
	
	public Color color {
		set {
			_color = value;
		}
		get {
			return _color;
		}
	}
	
	public ColorPicker (Vector2 pPosition) : base("ColorPickerBackground", pPosition)
	{
		_colorSelector = new Container("ColorSelector", new Vector2());
		_colorSelector.Visible = false;
		AddChild(_colorSelector);
	}
	
	public bool SelectColor()
	{
		float x = Scaled.MousePos.x - ScreenPosition.x;
		float y = Scaled.MousePos.y - ScreenPosition.y;
		
		float hue = x / width;
		float lightness = (y / height);
		
		if(hue >= 0.0f && hue <= 1.0f && lightness >= 0.0f && lightness <= 1.0f) {
			color = CalculateRGB(hue, lightness);
			_colorSelector.RelativePosition = new Vector2(x - _colorSelector.width / 2, y - _colorSelector.height / 2);
			_colorSelector.Visible = true;
			RefreshChildScreenPositions();
			return true;
		} else {
			_colorSelector.Visible = false;
		}
		return false;
	}
	
	public Color CalculateRGB(float pHue, float pLightness) {
		float r = 0;
		float g = 0;
		float b = 0;
		
		float H = pHue * 360;
		
		if(0 < H && H <= 60)   { g = H / 60.0f; }
		if(60 < H && H <= 180) { g = 1.0f; }
		if(180 < H && H <= 240) { g = 1.0f - (H - 180) / 60.0f; }
		    
		if(0 < H && H <= 60)   { r = 1.0f; }
		if(60 < H && H <= 120) { r = 1.0f - (H - 60) / 60.0f; }
		if(240 < H && H <= 300) { r = (H - 240) / 60.0f; }
		if(300 < H && H <= 360) { r = 1.0f; }
		
		if(120 < H && H <= 180) { b = (H - 120) / 60.0f; }
		if(180 < H && H <= 300) { b = 1.0f; }
		if(300 < H && H <= 360) { b = 1.0f - (H - 300) / 60.0f; }
		
		float modifier = (pLightness * 2) - 1;
		
		return new Color(modifier + r, modifier + g, modifier + b, 0.0f);
	}
}
