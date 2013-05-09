using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TextEntry : Container
{
	public delegate void OnEnterPressed(TextEntry pTextEntry);
	
	private string _text = "";
	private GUISkin _skin;
	private bool _secret;
	private OnEnterPressed _onEnterPressed;
	public Color textColor = new Color(255, 255, 255, 255);
	private string _defaultText;
	private bool _makeActive = false;
    static int counter = 0;
    public int maxlength = 120;
	
    public TextEntry(string pTextureName, string pDefaultText, Vector2 pPosition, bool pSecret, OnEnterPressed pOnEnterPressed)
		: base(pTextureName, pPosition) 
	{
		_onEnterPressed = pOnEnterPressed;
		_secret = pSecret;
        _defaultText = pDefaultText;
		_skin = (GUISkin)Resources.Load("BasicSkin");
		if(_skin == null) { throw new Exception("Can't find BasicSkin"); }
        _name = "input_field_" + counter++;
	}
	
    public override void Draw()
    {
        
        //GUI.GetNameOfFocusedControl() == _name &&  FUNKAR INTE HELT!!!
		if(
            Event.current.type == EventType.KeyDown && 
            Event.current.keyCode == KeyCode.Return && 
            _onEnterPressed != null
            ) { 
			_onEnterPressed(this);
		}
		
        if (!_visible)
            return;
        PreDraw();
        if (_texture != null)
        {
            GUI.color = tint;
			
            if (_disabled) { GUI.color = new Color(0.1f, 1.0f, 1.0f, 0.5f); }
            Rect r = new Rect(CalculatedScreenPosition.x,
                              CalculatedScreenPosition.y, 
			                  _texture.width, 
			                  _texture.height);
            GUI.DrawTexture(r, _texture);
			
            if (_text != null) {
				Rect r2 = new Rect(CalculatedScreenPosition.x + 5.0f,
				                           CalculatedScreenPosition.y + 5.0f,
				                           this.width,
				                           this.height);
				
				GUI.skin = _skin;
				_skin.label.alignment = TextAnchor.UpperLeft;
				GUI.color = new Color(textColor.r, textColor.g, textColor.b, alpha);
				
				if(_disabled) {
					if(_secret) {
						string s = "";
						for(int i = 0; i < _text.Length; i++) {
							s += "*";
						}
						GUI.Label(r2, s);
					}
					else {
	                	GUI.Label(r2, _text);
					}
				}
				else {
					if(_secret) {
						
						if(_text.Length == 0) {
							GUI.color = new Color(textColor.r, textColor.g, textColor.b, alpha * 0.5f);
							Rect r3 = new Rect(CalculatedScreenPosition.x + 10.0f,
				                           CalculatedScreenPosition.y + 5.0f,
				                           this.width,
				                           this.height);
							GUI.Label(r3, _defaultText);
							GUI.SetNextControlName(_name);
							_text = GUI.TextField(r2, _text, maxlength);
							if(_makeActive) {
								GUI.FocusControl(_name);
								_makeActive = false;
							}
						}
						
						_text = GUI.PasswordField(r2, _text, '*', maxlength);
					}
					else {
						
						if(_text.Length == 0) {
							GUI.color = new Color(textColor.r, textColor.g, textColor.b, alpha * 0.5f);
							Rect r3 = new Rect(CalculatedScreenPosition.x + 10.0f,
				                           CalculatedScreenPosition.y + 5.0f,
				                           this.width,
				                           this.height);
							GUI.Label(r3, _defaultText);
							GUI.SetNextControlName(_name);
							_text = GUI.TextField(r2, _text);
							if(_makeActive) {
								GUI.FocusControl(_name);
								_makeActive = false;
							}
						}
						
						_text = GUI.TextField(r2, _text, maxlength);
					}
				}
			}
        }
        foreach (Container c in _children)
        {
            c.Draw();
        }
        PostDraw();

    }
	
    public string text
    {
        set { _text = value; }
        get { return _text; }
    }
	
	public void ActivateTextfield()
	{
		_makeActive = true;
	}
}
