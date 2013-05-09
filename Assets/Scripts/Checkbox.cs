using System;
using UnityEngine;

public class Checkbox : Container
{
	public delegate void OnChecked(Checkbox pCheckbox);
	
	Texture _checkedTexture, _uncheckedTexture;
	bool _checked;
	OnChecked _onChange;
	
	public Checkbox (Vector2 pPosition) : base(pPosition)
	{
		_checkedTexture = (Texture)Resources.Load("checkbox_checked");
		_uncheckedTexture = (Texture)Resources.Load("checkbox_unchecked");
		isChecked = false;
	}
    
    public Checkbox (Vector2 pPosition, OnChecked pOnChecked) : base(pPosition)
    {
        _checkedTexture = (Texture)Resources.Load("checkbox_checked");
        _uncheckedTexture = (Texture)Resources.Load("checkbox_unchecked");
        isChecked = false;
        _onChange = pOnChecked;
    }
	
	public Checkbox (Vector2 pPosition, string pCheckedTexture, string pUncheckedTexture, OnChecked pOnChecked) : base(pPosition)
	{
		_checkedTexture = (Texture)Resources.Load(pCheckedTexture);
		_uncheckedTexture = (Texture)Resources.Load(pUncheckedTexture);
		isChecked = false;
		_onChange = pOnChecked;
	}
	
	public bool isChecked {
		get { return _checked; }
		set {
			_checked = value;
			if(_checked) {
				_texture = _checkedTexture;
			}
			else {
				_texture = _uncheckedTexture;
			}
		}
	}
	
	public override void Press()
	{
		isChecked = !isChecked;
		if(_onChange != null) {
			_onChange(this);
		}
	}
}
