using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Label : Container
{
    string _label = null;
	private bool _highlighted = false;
	private GUISkin _skin;
	public Color textColor = Color.white;
	public float textMargin = 5.0f;
	Container _overlayWhenHighlighted;
	public TextAnchor textAlignment = TextAnchor.UpperLeft;
	
	public bool highlighted {
		set {
			_highlighted = value;
			_overlayWhenHighlighted.Visible = value;
		}
		get {
			 return _highlighted;
		}
	}
	
    public Label(string pTextureName,Vector2 pPosition)
        : base(pTextureName, pPosition)
    {
        _label = null;
    }
	
    public Label(string pTextureName, string pLabel, Vector2 pPosition)
		: base(pTextureName, pPosition) 
	{
        _label = pLabel;
		_skin = (GUISkin)Resources.Load("BasicSkin");
		if(_skin == null) { throw new Exception("Can't find BasicSkin"); }
	}
	
	public Label(string pTextureName, string pLabel, Vector2 pPosition, string pSkinName)
		: base(pTextureName, pPosition) 
	{
        _label = pLabel;
		_skin = (GUISkin)Resources.Load(pSkinName);
		if(_skin == null) { throw new Exception("Can't find BasicSkin"); }
	}
	
	public void SetOverlayForWhenHighlighted(string pTextureName) {
		_overlayWhenHighlighted = new Container(pTextureName, new Vector2(0, 0));
		this.AddChild(_overlayWhenHighlighted);
		_overlayWhenHighlighted.Visible = false;
	}
	
    public override void Draw()
    {
        if (!_visible)
            return;
        PreDraw();
        if (_texture != null)
        {           
			if (_disabled) { 
				GUI.color = new Color(0.1f, 1.0f, 1.0f, 0.5f); 
			}			
			else {
				GUI.color = tint;
			}
			
			Rect r = new Rect(CalculatedScreenPosition.x,
                              CalculatedScreenPosition.y, 
			                  _texture.width, 
			                  _texture.height);
			
            GUI.DrawTexture(r, _texture);
			
			// Text:
            if (_label != null) {
				Rect r2 = new Rect(CalculatedScreenPosition.x + textMargin,
				                           CalculatedScreenPosition.y + textMargin,
				                           width,
				                           height);
				
				_skin.label.alignment = textAlignment;
				GUI.skin = _skin;
				GUI.color = new Color(textColor.r, textColor.g, textColor.b, tint.a);
				
                GUI.Label(r2, _label);
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
        set { _label = value; }
        get { return _label; }
    }
}
