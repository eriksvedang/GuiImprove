using System;
using UnityEngine;

public class PlaybackMultifunctionalButton : Container
{
	Container _blinker1;
    public Container _blinker2;
	float _blinkTimer = 0f;
	
	Texture 
		_gotoPlanning, 
		_startPlayback, 
		_pausePlayback, 
		_gotoWinState, 
		_gotoLoseState, 
		_waitingForEnemy,
		_placePawns,
		_submitMoves,
        _tie            
			;
	
	public PlaybackMultifunctionalButton (Vector2 pPosition) : base(pPosition)
	{
		_gotoPlanning = (Texture)Resources.Load("playback_gotoPlanning");
		D.isNull(_gotoPlanning, "Couldn't load gotoPlanning");
		_startPlayback = (Texture)Resources.Load("playback_startPlayback");
		D.isNull(_startPlayback, "Couldn't load startPlayback");
		_pausePlayback = (Texture)Resources.Load("playback_pausePlayback");
		D.isNull(_pausePlayback, "Couldn't load pausePlayback");
        _waitingForEnemy = (Texture)Resources.Load("playback_waitingForEnemy");
        D.isNull(_waitingForEnemy, "Couldn't load playback_waitingForEnemy");
        _gotoWinState = (Texture)Resources.Load("playback_gotoWinState");
        D.isNull(_gotoWinState, "Couldn't load playback_gotoWinState");
        _gotoLoseState = (Texture)Resources.Load("playback_gotoLoseState");
        D.isNull(_gotoLoseState, "Couldn't load playback_gotoLoseState");
		_placePawns = (Texture)Resources.Load("playback_PlacePawns");
        D.isNull(_placePawns, "Couldn't load playback_PlacePawns");
		_submitMoves = (Texture)Resources.Load("playback_SubmitMoves");
        D.isNull(_submitMoves, "Couldn't load playback_SubmitMoves");
        _tie = (Texture)Resources.Load("playback_gotoTieState");
        D.isNull(_tie, "Couldn't load playback_gotoTieState");
		
		_texture = _gotoPlanning;
		
		_blinker1 = new Container("playback_startPlaybackExtra1", new Vector2(87, -16));
        _blinker2 = new Container("playback_startPlaybackExtra2", new Vector2(87, -16));
		
		_blinker2.SetOnContainerPressedDelegate(OnBlinkerPressed);
		
		AddChild(_blinker1);
		AddChild(_blinker2);
		
		colorWhenDisabled = new Color(0.1f, 1.0f, 0.1f, 0.1f);
        customHitbox = new Rect(0, 50, 170, 100);
	}
	
	private void OnBlinkerPressed(Container pContainer)
	{
		if (_onContainerPressed != null) {
			_onContainerPressed(this);
		}
	}
	
	public void ShowGotoPlanning() {
		_texture = _gotoPlanning;
		setHoverOverlay("planNextTurn_selected", new Vector2(882-855, 64));
	}
	
	public void ShowStartPlayback() {
		_texture = _startPlayback;
		setHoverOverlay("startPlayback_selected", new Vector2(880-855, 64));
	}
	
	public void ShowPausePlayback() {
		_texture = _pausePlayback;
		setHoverOverlay("stopPlayback_selected", new Vector2(885-855, 64));
	}

    public void ShowGotoWinState()
    {
        _texture = _gotoWinState;
		setHoverOverlay("youWon_selected", new Vector2(904-855, 64));
    }

    public void ShowGotoLoseState()
    {
        _texture = _gotoLoseState;
		setHoverOverlay("youLost_selected", new Vector2(899-855, 64));
    }

    public void ShowWaitingForEnemy()
    {
        _texture = _waitingForEnemy;
		setHoverOverlay("", new Vector2(0, 0));
		//setHoverOverlay("waiting_selected", new Vector2(903-855, 64));
    }
	
	public void ShowPlacePawns()
    {
        _texture = _placePawns;
		setHoverOverlay("placePawns_selected", new Vector2(889-855, 64));
    }
	
	public void ShowSubmitMoves()
    {
        _texture = _submitMoves;
		setHoverOverlay("submitMoves_selected", new Vector2(886-855, 64));
    }
	
    public void ShowTie()
    {
        _texture = _tie;
        setHoverOverlay("Tie_selected", new Vector2(886-855+39, 64));
    }
 
	public void UpdateBlink()
	{
		_blinker2.alpha = 0.6f + Mathf.Sin(_blinkTimer * 6.0f) * 0.4f;
		_blinker1.alpha = _blinker2.alpha * 0.5f;
		_blinkTimer += Time.deltaTime;
	}
	
	public void SetBlinkOn(bool pOn) {
		//_blinker1.Visible = _blinker2.Visible = pOn;
        if(!pOn) {
            _blinker1.alpha = _blinker2.alpha = 0.0f;
        }
	}
}
