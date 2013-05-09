using UnityEngine;
using System.Collections.Generic;

public class SingleChoiceButton : Container {
	
	//public delegate void OnSingleChoiceButtonWasPressed(SingleChoiceButton pButton);
	
	private List<SingleChoiceButton> _otherChoices;
	
	public Color colorWhenSelected = Color.white;
	public Color colorWhenNotSelected = Color.gray;
	public bool oneChoiceHasToBeSelected = false;
	
	private Texture _overlayWhenSelected;
	private Vector2 _overlayWhenSelectedOffset;
    
    protected bool _selected = false;
    public bool selected {
        get { return _selected; }
        set { 
            _selected = value; 
        }
    }
    
    public override bool SetHovered() {
        if(_hoverOverlay != null && !disabled && Visible && !_selected) {
            _hoverOverlayVisible = true;
             return true;
        }
        return false;
    }
	
	public void SetOverlayWhenSelected(string pTextureName, Vector2 pOffset) {
		_overlayWhenSelected = (Texture)Resources.Load(pTextureName);
		_overlayWhenSelectedOffset = pOffset;
	}

	public SingleChoiceButton(string pTextureName, Vector2 pPosition, List<SingleChoiceButton> pOtherChoices, Container.Pressed pOnPressed)
		: base(pTextureName, pPosition) 
	{
		_otherChoices = pOtherChoices;
		_otherChoices.Add(this);
		_onContainerPressed = pOnPressed;
	}
	
	public override void Press ()
	{
		if(_disabled || !_visible) { return; }
        
        base.Press();        
		
		if(this.selected) { 
			if(oneChoiceHasToBeSelected) {
				// do nothing
			}
			else {
				foreach(SingleChoiceButton s in _otherChoices) {
					s.selected = false;
				}
			}
		}
		else {
			foreach(SingleChoiceButton s in _otherChoices) {
				s.selected = false;
			}
			this.selected = true;
		}
	}
	
	/*
	 * 			if(_disabled) {
				GUI.color = new Color(0.5f, 0.5f, 0.5f, 0.2f);
				size = 0.8f;
				compensate = (_texture.width * (1.0f - size)) / 2;
			}
			*/
		        
	public override void Draw()
    {
		PreDraw();
		
		if(!Visible) { return; }
		
		if(_texture != null) {
			
			float size = 1.0f;
			float compensate = 0;
			
			if(disabled) {
				GUI.color = colorWhenDisabled;
			} 
			else {
				if(selected) {
					GUI.color = colorWhenSelected;
				}
				else {
					GUI.color = colorWhenNotSelected;
				}
			}
			
			float w = _texture.width * size;
			float h = _texture.height * size;
            Rect r = new Rect((CalculatedScreenPosition.x + compensate),
                                      (CalculatedScreenPosition.y + compensate), w, h);
			GUI.DrawTexture(r, _texture);
			
			if(selected && _overlayWhenSelected != null) {
				GUI.color = Color.white;
				Rect r2 = new Rect((CalculatedScreenPosition.x + _overlayWhenSelectedOffset.x),
                                           (CalculatedScreenPosition.y + _overlayWhenSelectedOffset.y), 
				                           _overlayWhenSelected.width,
				                           _overlayWhenSelected.height);										
				GUI.DrawTexture(r2, _overlayWhenSelected);
			}
		}
		foreach(Container c in _children) {
			c.Draw();
		}
		
		PostDraw();
	}

}

