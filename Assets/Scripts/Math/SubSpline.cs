using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

public struct Curve
{
    internal SplineNode[] points;
    public float length
    {
        get { return points[points.Length - 1].timeToHere; }
    }
    public int pointCount
    {
        get { return points.Length; }
    }
    public Vector3 GetInterpolatedPositionAtLength(float pLength)
    {
        for (int i = 0; i < points.Length; i++)
        {
            if (points[i].timeToHere > pLength)
            {
                float partLength = pLength - points[i - 1].timeToHere;

                Vector3 deltaPoint = points[i].point - points[i - 1].point;

                float deltaLength = points[i].timeToHere - points[i - 1].timeToHere;

                return points[i - 1].point + deltaPoint * (partLength / deltaLength);
            }  
        }

        return points[points.Length -1].point;
    }
    public override string ToString()
    {
        StringBuilder s = new StringBuilder();
        foreach( SplineNode n in points )
        {
            s.Append( "point " );
            s.Append( n.ToString() );
            s.Append( "\n");
        }
        return s.ToString();
    }
}

public class SubSpline
{
    private float tension;
    private SplineNode[] nodes;
    public SubSpline(Vector3[] pControlPoints, float pTension)
    {
        if (pControlPoints.Length < 2)
        {
            D.LogError("Path must have at least two points!");
        }
        //Debug.Log("points : " + pControlPoints.Length);
        tension = pTension;
        nodes = new SplineNode[pControlPoints.Length + 2];
        nodes[0] = new SplineNode(GetExtrapolatedStartPoint(pControlPoints[0], pControlPoints[1]), 0f);
        nodes[nodes.Length - 1] = new SplineNode(GetExtraolatedEndPoint(pControlPoints[pControlPoints.Length - 2], pControlPoints[pControlPoints.Length - 1]), 0f);
        
        for (int cpIndex = 0; cpIndex < pControlPoints.Length; cpIndex++)
        {
            int nodeIndex = cpIndex + 1;
            float len = 0;
            if (cpIndex > 0)
            {
                float timeUpToPreviousNode = nodes[nodeIndex - 1].timeToHere;
                float timeFromPrevNodeToPresentNode = (pControlPoints[cpIndex] - nodes[nodeIndex - 1].point).magnitude;
                len = timeUpToPreviousNode + timeFromPrevNodeToPresentNode;
            }                
            
            SplineNode node = new SplineNode(pControlPoints[cpIndex], len);
            nodes[nodeIndex] = node;
        }
    }
    private Vector3 GetExtrapolatedStartPoint(Vector3 pFirst, Vector3 pSecond)
    {
        return pFirst + pFirst - pSecond;
    }
    private Vector3 GetExtraolatedEndPoint(Vector3 pNextToLast, Vector3 pLast)
    {
        return pLast + pLast - pNextToLast;
    }
    private Vector3 GetHermiteInternal(int idxFirstPoint, float t)
    {
        float t2 = t * t;
        float t3 = t2 * t;
        Vector3 P0 = nodes[idxFirstPoint - 1].point;
        Vector3 P1 = nodes[idxFirstPoint].point;
        Vector3 P2 = nodes[idxFirstPoint + 1].point;
        Vector3 P3 = nodes[idxFirstPoint + 2].point;
        //float tension = 0.5f;	// 0.5 equivale a catmull-rom
        Vector3 T1 = tension * (P2 - P0);
        Vector3 T2 = tension * (P3 - P1);
        float Blend1 = 2 * t3 - 3 * t2 + 1;
        float Blend2 = -2 * t3 + 3 * t2;
        float Blend3 = t3 - 2 * t2 + t;
        float Blend4 = t3 - t2;
        return Blend1 * P1 + Blend2 * P2 + Blend3 * T1 + Blend4 * T2;
    }
    public Vector3 GetHermiteAtTime(float timeParam)
    {
        int c;
        for (c = 1; c < nodes.Length - 2; c++)
        {
            if (nodes[c].timeToHere > timeParam)
                break;
        }
        int idx = c - 1;
        float param = (timeParam - nodes[idx].timeToHere) / (nodes[idx + 1].timeToHere - nodes[idx].timeToHere);
        return GetHermiteInternal(idx, param);
    }
    public float TotalTime()
    {
        return nodes[nodes.Length - 2].timeToHere;
    }
    public Curve GetCurve( int pSteps )
    {
        Curve p =  new Curve();
        p.points = new SplineNode[pSteps];
        float partStep = TotalTime() / (float)pSteps;
        for (int i = 0; i < pSteps; i++)
        { 
           float timeHere = partStep * i;
           Vector3 point = GetHermiteAtTime(timeHere);
           float newTime = 0f;
           if (i > 0)
           { 
               Vector3 deltaPoisition = point - p.points[i -1].point;
               newTime = p.points[i - 1].timeToHere + deltaPoisition.magnitude;
           }
           p.points[i] = new SplineNode(point, newTime);
        }
        return p;
    }
}
internal struct SplineNode
{
    internal SplineNode(Vector3 pPoint, float pTime)
    {
        point = pPoint;
        timeToHere = pTime;
    }
    internal Vector3 point;
    internal float timeToHere;
    public override string ToString()
    {
        return "[pos: " + point + ", timeHere: " + timeToHere + "]";
    }
}


