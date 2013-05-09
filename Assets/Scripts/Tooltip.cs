using System;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip : Container {
	
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
	
	public Tooltip(Vector2 pPosition, string pHeaderText, string pWarningText, string pBodyText) : base(pPosition) 
	{
		_headerText = pHeaderText;
		_warningText = pWarningText;
		_bodyText = pBodyText;
		
		_helpSignTop = (Texture)Resources.Load("HelpSignTop");
		_helpSignMiddle = (Texture)Resources.Load("HelpSignMiddle");
		_helpSignSelect = (Texture)Resources.Load("HelpSignSelect");
		_helpSignSelect2 = (Texture)Resources.Load("HelpSignSelect2");
		_helpSignBottom = (Texture)Resources.Load("HelpSignBottom");
		
		_skin = (GUISkin)Resources.Load("BasicSkin");
		
		RefreshNrOfSegments();
	}
	
	private void RefreshNrOfSegments() {
		_nrOfSegments = 3 + ((_bodyText.Length + _warningText.Length) / 34);
	}
	
	public float variableHeight {
		get {
			return 32.0f * _nrOfSegments;
		}
	}
	
	public override void Draw ()
	{
		if(!Visible) { return; }
		
		GUI.color = new Color(1, 1, 1, alpha);
		
		float armY = CalculatedScreenPosition.y;
		if(armAtBottom) {
			armY = CalculatedScreenPosition.y + variableHeight;
		}
		
		// Arm thingy
		GUI.DrawTexture(new Rect(CalculatedScreenPosition.x + armXextension,
                                         armY,
                                         _helpSignSelect.width,
                                         _helpSignSelect.height), _helpSignSelect);
		
		if(armXextension < -70) {
			GUI.DrawTexture(new Rect(CalculatedScreenPosition.x - 123,
	                                         armY,
	                                         _helpSignSelect2.width,
	                                         _helpSignSelect2.height), _helpSignSelect2);
		}
		
		// Top part
		GUI.DrawTexture(new Rect(CalculatedScreenPosition.x,
                                         CalculatedScreenPosition.y,
                                         _helpSignTop.width,
                                         _helpSignTop.height), _helpSignTop);
		
		// Middle parts
		for(int i = 1; i < _nrOfSegments; i++) {
			GUI.DrawTexture(new Rect(CalculatedScreenPosition.x,
                                         CalculatedScreenPosition.y + i * 32,
                                         _helpSignTop.width,
                                         _helpSignTop.height), _helpSignMiddle);
		}
		
		// Bottom part
		GUI.DrawTexture(new Rect(CalculatedScreenPosition.x,
                                         CalculatedScreenPosition.y + variableHeight,
                                         _helpSignBottom.width,
                                         _helpSignBottom.height), _helpSignBottom);
		
		GUI.skin = _skin;
		_skin.label.alignment = TextAnchor.UpperLeft;
		
		GUI.color = new Color(1, 1, 0, alpha);
		float x = CalculatedScreenPosition.x + 10;
		float y = CalculatedScreenPosition.y + 10;
		GUI.Label(new Rect(x, y, 220, 20), _headerText);
		y += 20 + 20;
		
		if(_warningText.Length > 0) {
			GUI.color = new Color(1, 0, 0, alpha);
			GUI.Label(new Rect(x, y, 220, 70), _warningText);
			y += 60 + 20;
		}
		
		GUI.color = new Color(1, 1, 1, alpha);
		GUI.Label(new Rect(x, y, 220, 500), _bodyText);
	}
	
	public string bodyText {
		set {
			_bodyText = value;
			RefreshNrOfSegments();
		}
	}
	
	public string headerText {
		set {
			_headerText = value;
			RefreshNrOfSegments();
		}
	}
	
	public new string warningText {
		set {
			_warningText = value;
			RefreshNrOfSegments();
		}
	}
}

