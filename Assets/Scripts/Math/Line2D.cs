using System;
using System.Collections.Generic;
using UnityEngine;
public class Line2D
{
	private  Vector2 _biNormal;
	private  Vector2 _normal;
	private  Vector2 _position;
    private  float _distance;
	public Line2D( Vector2 pPointOnLine, Vector2 pNormal ) 
	{
        _position = pPointOnLine;
		_biNormal.x = pNormal.y;
        _biNormal.y = -pNormal.x;
		_normal = pNormal;
		_distance = DotProduct(_normal, pPointOnLine);

	}
	public float DistanceTo(Vector2 pPosition )
	{
        return DotProduct(pPosition, _normal) - _distance;
	}
	public Vector2 ClosestPointOnLine( Vector2 pPosition )
	{		
		float dist = DotProduct(_normal,pPosition - _position);
        return new Vector3(pPosition.x - dist * _normal.x, pPosition.y - dist * _normal.y);
	}
	public float DotProduct(Vector2 a, Vector2 b )
	{
		return a.x * b.x + a.y * b.y;
	}
    public Vector2 GetIntersectionPoint(Vector2 startPos, Vector2 rayForward)
    {
        float t = -DistanceTo(startPos) / DotProduct(rayForward, _normal);
        //float closingUpSpeed = Mathf.Abs(DotProduct(rayForward, _biNormal));
        return startPos + rayForward * t;
    }

    public Vector2 BiNormal
    {
        get { return _biNormal; }
    }
    public Vector2 Normal
    {
        get { return _normal; }
    }
    public Vector2 Point
    {
        get { return _position; }
    }    
}
