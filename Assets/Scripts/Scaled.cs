using System;
using UnityEngine;

public class Scaled
{
	public const float SCREEN_WIDTH = 1024f;
    public const float SCREEN_HEIGHT = 768f;
	
	public static Vector2 MousePos {
		get {
            Vector3 mousePos3d = Input.mousePosition;
            return FromScreenSpaceToActiveSpace(new Vector2(mousePos3d.x, mousePos3d.y));
		}
	}
    
    public static float scaleFactor {
        get {
            //return Mathf.Min(Screen.height / Scaled.SCREEN_HEIGHT, Screen.width / Scaled.SCREEN_WIDTH);
            //return Screen.width / Scaled.SCREEN_WIDTH;
            //return Screen.height / Scaled.SCREEN_HEIGHT;
            return 1f;
        }
    }

    static int distanceFromLeft {
        get 
        {
            if(scaleFactor < 1f) {
                return 0;
            }
            else {
                return (Screen.width - (int)SCREEN_WIDTH) / 2;
            }
        }
    }

    static int distanceFromTop { 
        get 
        { 
            if(scaleFactor < 1f) {
                return 0;
            }
            else {
                return (Screen.height - (int)SCREEN_HEIGHT) / 2; 
            }
        }
    }
    
	public static Vector2 FromScreenSpaceToActiveSpace(Vector2 pPoint) {
        return new Vector2(pPoint.x - (float)distanceFromLeft, Screen.height - pPoint.y - distanceFromTop);
	}

    public static Rect ActiveArea { 
        get {
            float sf = Mathf.Min(scaleFactor, 1.0f);
            Vector2 activeAreaXY = new Vector2(
                ((Screen.width - Scaled.SCREEN_WIDTH * sf) / 2), 
                ((Screen.height - Scaled.SCREEN_HEIGHT * sf) / 2));
            return new Rect(activeAreaXY.x, activeAreaXY.y, Scaled.SCREEN_WIDTH * sf, Scaled.SCREEN_HEIGHT * sf); 
        } 
    }

   
}
