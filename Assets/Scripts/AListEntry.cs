using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class AListEntry : Container
{
	public AListEntry(Vector2 pPosition) : base(pPosition) 
	{
	}

    public abstract bool MousePressed(Vector2 pPosition);
    public abstract bool MouseReleased(Vector2 pPosition);

}
