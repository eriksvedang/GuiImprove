using System;
using UnityEngine;
public static class RectExtensions
{
    public static void Position( this Rect pSelf, Vector2 pPosition ){
        pSelf.x = pPosition.x;
        pSelf.y = pPosition.y;
    }
    public static Vector2 Position( this Rect pSelf){
        return new Vector2( pSelf.x, pSelf.y );
    }
}

