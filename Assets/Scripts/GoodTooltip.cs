using System;
using System.Collections.Generic;
using UnityEngine;

public class GoodTooltip : Container
{
    GUISkin _skin;
    string _headerText;
    string _warningText;
    string _bodyText;
    Texture _helpSignTop, _helpSignBottom;
    Texture _helpSignMiddle;
    Texture _helpSignSelect, _helpSignSelect2;
    float _nrOfSegments = 5;
    public bool armAtBottom = true;
    public float armXextension = -200;
    public static GoodTooltip instance = null;
    
    private Texture2D _tooltipNormal, _tooltipFlipped;
    
    public GoodTooltip (Vector2 pPosition, string pHeaderText, string pWarningText, string pBodyText) : base(pPosition)
    {
        instance = this;
        _headerText = pHeaderText;
        _warningText = pWarningText;
        _bodyText = pBodyText;
     
        /*
        _helpSignTop = (Texture)Resources.Load ("HelpSignTop");
        _helpSignMiddle = (Texture)Resources.Load ("HelpSignMiddle");
        _helpSignSelect = (Texture)Resources.Load ("HelpSignSelect");
        _helpSignSelect2 = (Texture)Resources.Load ("HelpSignSelect2");
        _helpSignBottom = (Texture)Resources.Load ("HelpSignBottom");
        */
        
        SetTexture("ToolTip");
        
        _tooltipNormal = (Texture2D)Resources.Load ("ToolTip");
        _tooltipFlipped = (Texture2D)Resources.Load ("ToolTipFlipped");

        _skin = (GUISkin)Resources.Load ("BasicSkin");
        Visible = false;

        RefreshNrOfSegments ();
    }
 
    private void RefreshNrOfSegments ()
    {
        _nrOfSegments = ((_bodyText.Length + _warningText.Length) / 34);
    }
 
    public float variableHeight {
        get {
            return 32.0f * _nrOfSegments;
        }
    }
    public void SetHoveredItem(Container pContainer) {
        if(pContainer != null) {
            if (pContainer.tooltipText != "" && !_visible && !pContainer.disabled) {
                _bodyText = pContainer.tooltipText;
                Visible = true;
            }
        }
    }
 
    public override void Draw ()
    {
        if (!Visible) {
            return;
        }
        
        Rect r = new Rect(CalculatedScreenPosition.x,
                          CalculatedScreenPosition.y,
                          _texture.width * _textureScale.x,
                          _texture.height * _textureScale.y);
        
        
        bool flipped = _screenPosition.x > 600;
        
        if(flipped) {
            r.Set(r.x - 315, r.y, r.width, r.height);
            GUI.DrawTexture(r, _tooltipFlipped);
        }
        else {
            GUI.DrawTexture(r, _tooltipNormal);
        }      
     
        GUI.color = new Color (1, 1, 1, alpha);

        GUI.skin = _skin;
        
        if(flipped) {
            _skin.label.alignment = TextAnchor.UpperRight;
        } else {
            _skin.label.alignment = TextAnchor.UpperLeft;
        }
     
        float x = CalculatedScreenPosition.x + 10;
        float y = CalculatedScreenPosition.y + 22;
        
        GUI.color = new Color (1, 1, 1, alpha);
        GUI.Label (new Rect (x + 25 + (flipped ? -315 : 0), y - 16, 230, 500), _bodyText);
        if(Event.current.type == EventType.Repaint)
            Visible = false;
    }
 
    public string bodyText {
        set {
            _bodyText = value;
            RefreshNrOfSegments ();
        }
    }
 
    public string headerText {
        set {
            _headerText = value;
            RefreshNrOfSegments ();
        }
    }
 
    public new string warningText {
        set {
            _warningText = value;
            RefreshNrOfSegments ();
        }
    }
}

