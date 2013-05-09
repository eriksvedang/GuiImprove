using System;
using System.Collections.Generic;
using UnityEngine;

public class Container :  IDisposable
{
	public Batcher batcher;

	public delegate void Pressed(Container pContainer);
	
    public static Vector2 groupClippingOffset = Vector2.zero;
	protected List<Container> _children = new List<Container>();
	protected Texture _texture;
	protected Vector2 _relativePosition;
	protected Vector2 _screenPosition;
		
	protected string _name;
	protected bool _disabled = false;
	protected bool _visible = true;
	protected bool _hasCustomHitbox = false;
	protected Rect _customHitbox;
	protected bool _reactsToMouseClick = true;
	public string warningText = "";
	public Color colorWhenDisabled = new Color(0.1f, 1.0f, 1.0f, 0.5f);
	public Color tint = Color.white;
	protected Texture _hoverOverlay = null;
	protected Vector2 _hoverOverlayOffset;
	protected bool _hoverOverlayVisible = false;
	
	private List<string> _tags;
    public string tooltipText = "";
	
	protected Pressed _onContainerPressed;
	
    protected Vector2 _textureScale = Vector2.one;
	
    public override string ToString ()
    {
        return GetType().ToString() + ", texture " + _texture.name;
    }
	
	/// <summary>
	/// Returns true if it was set to hovered
	/// </summary>
	public virtual bool SetHovered() {
		if(_hoverOverlay != null && !disabled && Visible) {
			_hoverOverlayVisible = true;
			return true;
		}
		return false;
	}
	
	public void SetNotHovered() {
		foreach(Container c in _children) {
			c.SetNotHovered();
		}
		if(_hoverOverlay != null) {
			_hoverOverlayVisible = false;
		}
	}
	
	public void setHoverOverlay(string pTextureName, Vector2 pOffset) {
		_hoverOverlay = (Texture)Resources.Load(pTextureName);
		_hoverOverlayOffset = pOffset;
	}
	
	public float alpha {
		get {
			return tint.a;
		}
		set {
			tint = new Color(tint.r, tint.g, tint.b, value);
			foreach(Container c in _children) {
				c.alpha = value;
			}
		}
	}
	
	public void SetAlpha(float pAlpha) {
		this.alpha = pAlpha;
	}
	
	public void SetOnContainerPressedDelegate(Pressed pDelegateFunction) {
		_onContainerPressed = pDelegateFunction;
	}
	
	public bool HasTag(string pTagName) {
		if(_tags == null) { return false; }
		foreach(string s in _tags) {
			if(s.ToLower() == pTagName.ToLower()) {
				return true; 
			}
		}
		return false;
	}
	
	public void AddTag(string pTagName) {
		if(_tags == null) {
			_tags = new List<string>();
		}
		_tags.Add(pTagName);
	}
	
	public Rect customHitbox {
		set {
			_hasCustomHitbox = true;
			_customHitbox = value;
		}
		get {
			return _customHitbox;
		}
	}
	
	public bool reactsToMouseClick {
		get { return _reactsToMouseClick; }
		set { _reactsToMouseClick = value; }
	}
	
    #region Constructors
    public Container (Vector2 pPosition)
	{
		init("", false, pPosition);
	}
    public Container(string pTextureName, Vector2 pPosition)
    {
        init(pTextureName, true, pPosition);
    }

	public Container (string pTextureName, Vector2 pPosition, Vector2 pTextureScale)
	{	
		init(pTextureName, true, pPosition);
        TextureScale = pTextureScale;
	}
    public Container(string pTextureName, Vector2 pPosition, Vector2 pTextureScale, Pressed pOnContainerPressed)
    {
        init(pTextureName, true, pPosition);
        _onContainerPressed = pOnContainerPressed;
        TextureScale = pTextureScale;
    }
	public Container (string pTextureName, Vector2 pPosition, Pressed pOnContainerPressed)
	{	
		init(pTextureName, true, pPosition);
		_onContainerPressed = pOnContainerPressed;
    }
	public Container (Texture pTexture, Vector2 pPosition)
	{
		_texture = pTexture;
		_relativePosition = pPosition;
		RefreshScreenPosition(new Vector2());
	}
    #endregion
	
    public Vector2 RelativePosition {
		get {
			return this._relativePosition;
		}
		set {
			_relativePosition = value;
		}
	}
	
    public void SetRelativePosition(Vector2 pPosition)
    {
        RelativePosition = pPosition;
    }
	
    public void SetScreenPosition(Vector2 pPosition)
    {
        ScreenPosition = pPosition;
    }
	
	public List<Container> Children {
		get {
			return this._children;
		}
	}

	public Vector2 ScreenPosition {
		get {
			return this._screenPosition;
		}
		set {
			_screenPosition = value;
            foreach (Container c in _children)
                c.RefreshScreenPosition(_screenPosition);
		}
	}

    public Vector2 TextureScale
    {
        get { return _textureScale; }
        set 
        {
            _textureScale = value;
        }
    }
	
    public void SetTexture(string pTextureName) 
    {
        _texture = (Texture)Resources.Load(pTextureName);
         if(_texture == null) {
             throw new Exception("Can't load texture '" + pTextureName + "'");
         }
    }
    
	private void init(string pTextureName, bool loadTexture, Vector2 pPosition) {
		_name = pTextureName;
		if(loadTexture) {
			SetTexture(pTextureName);
		}
		_relativePosition = pPosition;
		RefreshScreenPosition(new Vector2());
	}
	
	public void RefreshScreenPosition(Vector2 pParentScreenPosition) {
		_screenPosition = pParentScreenPosition + _relativePosition;
		foreach(Container c in _children) {
			c.RefreshScreenPosition(_screenPosition);
		}
	}
	
	public void RefreshChildScreenPositions() {
		foreach(Container c in _children) {
			c.RefreshScreenPosition(_screenPosition);
		}
	}
	
    public virtual void PreDraw()
    { 
    
    }

    //this is really something that I wished we could skip, but no. 
    //the gui renderer somtimes creates new coordinate systems, and this will compensate for that
    //also this is only in effect while drawing
    public Vector2 CalculatedScreenPosition 
    {
        get {
            return Scaled.ActiveArea.Position() + _screenPosition + groupClippingOffset; 
        }
    }
	
	public virtual void Draw() {

		if(!_visible) { return; }
		
        PreDraw();
		
		if(_texture != null) {
			
			if(_disabled) { 
				//GUI.color = new Color(colorWhenDisabled.r, colorWhenDisabled.g, colorWhenDisabled.b, colorWhenDisabled.a);
                GUI.color = colorWhenDisabled;
			}
			else {
                GUI.color = new Color(tint.r, tint.g, tint.b, tint.a);
			}
            //if(Event.current.type == EventType.Repaint)
            {
                Rect r = new Rect(CalculatedScreenPosition.x, //          _screenPosition.x,
				                  CalculatedScreenPosition.y, //		_screenPosition.y,
                                  _texture.width * _textureScale.x,
                                  _texture.height * _textureScale.y);

				//GUI.color = new Color(0f, 1f, 1f, 0.2f);
                //GUI.DrawTexture(r,  _texture);

				batcher.Draw(_texture.name, new float[] {
					r.x,
					r.yMax,
					1f,

					r.xMax,
					r.yMax,
					1f,

					r.xMax,
					r.y,
					1f,

					r.x,
					r.y,
					1f,
				}.ToVector3Array());


                //r.y += r.height;
                //r.height = -r.height;
                //GameController.guiMaterial.mainTexture = _texture;
                //GLDraw.DrawTexture(r, GameController.guiMaterial);
            }
            
		}
		
		foreach(Container c in _children) {
			c.Draw();
		}
		
        if (disabled)
        {
            DrawDisabled();
        }
        
		PostDraw();
	}
	
	public virtual void Update() {
		foreach(Container c in _children) {
			c.Update();
		}
	}
	
    public virtual void PostDraw() 
    {
		if(_hoverOverlay != null && _hoverOverlayVisible) {
			GUI.color = Color.white;
			//D.Log("Drawing hover thingy for " + name);
			Rect r = new Rect(CalculatedScreenPosition.x + _hoverOverlayOffset.x, 
			                          CalculatedScreenPosition.y + _hoverOverlayOffset.y, 
			                          _hoverOverlay.width, _hoverOverlay.height);
	    	GUI.DrawTexture(r, _hoverOverlay);
		}
        
    }
	
    public void DrawDisabled()
	{ 
      /*  
		if (this.disabled)
        {
            //when making GL draws we dont compensate with CalculatedScreenPosition, since the GLMatrix is not changed :)
            // even though the GUI matrix is diffrent
            GLDraw.DrawBox(new Color(1f, 0f, 0f, 0.2f), new Rect(_screenPosition.x, _screenPosition.y, width, height));
        }
      */
        
        
    }
	
	public virtual void Press() {
		if(_onContainerPressed != null && Visible && !disabled) {
			_onContainerPressed(this);
		}
	}
	
	//public virtual void Click(Vector2 mousePosition) { throw new NotImplementedException(); }
	
	public virtual Container GetContainerAtPosition(Vector2 pPosition) {
        var result = GetContainersAtPosition(pPosition);
        if (result.Count >= 1) {
            return result[result.Count - 1];
        }
        else {
            return null;
        }
	}
	
    protected bool IsInside(Rect r, Vector2 pPosition)
    {
        if (
           pPosition.x > r.x &&
           pPosition.y > r.y &&
           pPosition.x < r.x + r.width &&
           pPosition.y < r.y + r.height
               )
        {
            return true;
        }
        return false;
    
    }
	
    public virtual List<Container> GetContainersAtPosition(Vector2 pPosition)
    {
		List<Container> foundContainers = new List<Container>();
		foreach(Container c in _children) {
			List<Container> list = c.GetContainersAtPosition(pPosition);
			foundContainers.AddRange(list);
		}
        
		if( _reactsToMouseClick && 
            _texture != null && 
            Visible &&
            IsInside(new Rect(customHitbox.x + _screenPosition.x, customHitbox.y + _screenPosition.y, this.width, this.height), pPosition)
        )
		{
			foundContainers.Add(this);
		}		
		
		return foundContainers;
	}
	
	public Container GetChildWithName(string pName) {
		foreach(Container c in _children) {
			if(c.name == pName) {
				return c;
			}
		}
		throw new Exception("Couldn't find child " + pName + " in container " + _name);
	}
	
	public virtual void AddChild(Container pContainer)
    {
		_children.Add(pContainer);
        if (this.disabled)
            pContainer.disabled = true;
        if(!this.Visible)
            pContainer.Visible = false;

        pContainer.RefreshScreenPosition(this.ScreenPosition);
	}
	
    public void MoveToBack(Container pChild)
    {
        int i = _children.IndexOf(pChild);
        if(i != -1)
        {
            _children.RemoveAt(i);
            _children.Insert(0,pChild);
        }
    }
	
    public void MoveToFront(Container pChild)
    {
        int i = _children.IndexOf(pChild);
        if (i != -1)
        {
            _children.RemoveAt(i);
            _children.Add(pChild);
        }
    }
    
    public virtual void RemoveChild(Container pContainer)
    {
		pContainer.RemoveChildren();
		_children.Remove(pContainer);
	}

    public virtual void RemoveChildren()
    {
		foreach(Container c in _children) {
			c.RemoveChildren();
		}
		_children.Clear();
	}
	
	public bool disabled {
		get { return _disabled; }
		set { 
			_disabled = value; 
			if(_disabled && _hoverOverlay != null) {
				_hoverOverlayVisible = false;
			}
			foreach(Container c in _children) {
				c.disabled = value;
			}
		}
	}

    public Vector2 RelativePositionUnder(float pPaddingX, float pPaddingY )
    {
        return this._relativePosition + new Vector2(pPaddingX, height + pPaddingY);
    }
    public Vector2 RelativePositionUnder(float pPaddingY)
    {
        return RelativePositionUnder(0f, pPaddingY);
    }
    public Vector2 RelativePositionUnder()
    {
        return RelativePositionUnder(0f, 0f);
    }
    public Vector2 RelativePositionRightOf(float pPaddingX, float pPaddingY)
    {
        return this._relativePosition + new Vector2(width + pPaddingX, pPaddingY);
    }
    public Vector2 RelativePositionRightOf(float pPaddingX)
    {
        return RelativePositionRightOf(pPaddingX, 0f);
    }
    public Vector2 RelativePositionRightOf()
    {
        return RelativePositionRightOf(0f, 0f);
    }
	
	public bool Visible {
		get {
			return this._visible;
		}
		set {
			_visible = value;
            foreach (Container c in _children)
            {
                c.Visible = value;
            }
		}
	}	
	
	public float width
    { 
        get
        {
			if(_hasCustomHitbox) {
				return _customHitbox.width;
			}
            float maxWidth = 0.0f;
            if (_texture != null)
                maxWidth = _texture.width * _textureScale.x;
            foreach (Container c in _children)
            {
                float h = c.RelativePosition.x + c.width;
                if (h > maxWidth){ maxWidth = h;  }
            }
            return maxWidth;
        }

    }
    public float height
    {
        get
        {
			if(_hasCustomHitbox) {
				return _customHitbox.height;
			}
			float maxHeight = 0.0f;
            if (_texture != null)
                maxHeight = _texture.height * _textureScale.y;
			foreach(Container c in _children) {
				float h = c.RelativePosition.y + c.height;
				if(h > maxHeight) {
					maxHeight = h;
				}
			}
            return maxHeight;
        }
    }
	
	public string name {
		get { return _name; }
	}
    ~Container()
    {        
    }

    #region IDisposable Members

    public void Dispose()
    {
        _texture = null;
        _hoverOverlay = null;
        this._onContainerPressed = null;
        _texture = null;
        foreach (Container c in _children)
            c.Dispose();
        _children.Clear();
    }

    #endregion
}



