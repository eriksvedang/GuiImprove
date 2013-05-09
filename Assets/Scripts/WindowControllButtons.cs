using System;
using UnityEngine;

public class WindowControllButtons : Container
{
    Container _windowed;
    Container _fullscreen;
    Container _quit;
    
    public WindowControllButtons()
        : base(Vector2.zero) {
#if !UNITY_WEBPLAYER
        this.AddChild(_quit = new Container("quit", Vector2.one * 10f));
        this.AddChild(_fullscreen = new Container("fullscreen", Vector2.one * 10f + Vector2.right * 23f));
        this.AddChild(_windowed = new Container("windowed", Vector2.one * 10f + Vector2.right * 23f));
        _quit.SetOnContainerPressedDelegate(o => { Application.Quit(); });
        _fullscreen.SetOnContainerPressedDelegate(o => { 
            Resolution r = Screen.resolutions[Screen.resolutions.Length - 1];
            Screen.SetResolution(r.width, r.height, true); 
        });
        _windowed.SetOnContainerPressedDelegate(o => { Screen.SetResolution(1024, 768, false); });
#endif
    }
    public override void PreDraw() {
#if !UNITY_WEBPLAYER
        base.PreDraw();
        if (Screen.fullScreen) {
            _fullscreen.Visible = false;
            _windowed.Visible = true;
        }
        else {
            _fullscreen.Visible = true;
            _windowed.Visible = false;
        }
#endif
    }
}

