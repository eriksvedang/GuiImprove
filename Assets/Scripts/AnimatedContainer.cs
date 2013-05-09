using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedContainer : Container
{
	List<Texture> _frames = new List<Texture>();
	float _time = 0.0f;
	public float speed = 1.0f;
	public bool loop = false;
	public bool isStopped = false;
	
	public AnimatedContainer (string[] pFrameNames, Vector2 pPosition) : base(pPosition)
	{
		foreach(string frameName in pFrameNames) {
			Texture t = (Texture)Resources.Load(frameName);
            if(t == null) {
                throw new Exception("Can't find texture with name " + frameName);
            }
			_frames.Add(t);
		}
		_texture = _frames[0];
	}
	
	public float totalLength {
		get {
			return _frames.Count * speed;
		}
	}
	
	public override void Update ()
	{
		if(!isStopped) {
			_time += speed * Time.deltaTime;
			if((int)_time >= _frames.Count) {
				if(loop) {
					_time -= (float)(_frames.Count);
				} else {
					_time = (float)(_frames.Count) - 1f;
					isStopped = true;
				}
			}
		}	
        _texture = _frames[(int)_time];
	}		   
}
