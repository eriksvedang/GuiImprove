using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public struct IntPoint3
{
    public IntPoint3(int pX, int pY, int pZ)
    {
        x = pX;
        y = pY;
        z = pZ;
    }
    public int x;
    public int y;
    public int z;
    public int sum { get { return x + y + z; } }
    public int lengthManhattan { get { return Mathf.Abs(x) + Mathf.Abs(y) + Mathf.Abs(z); } }
    public Vector3 ToVector3() { return new Vector3((float)x, (float)y, (float)z); }
    public static readonly IntPoint3 one = new IntPoint3(1, 1, 1);
    public static readonly IntPoint3 zero = new IntPoint3(0, 0, 0);
    public static readonly IntPoint3 up = new IntPoint3(0, 1, 0);
    public static readonly IntPoint3 right = new IntPoint3(1, 0, 0);
    public static readonly IntPoint3 forward = new IntPoint3(0, 0, 1);
    public static IntPoint3 operator -(IntPoint3 pFirst)
    {
        return new IntPoint3(-pFirst.x, -pFirst.y,
            -pFirst.z);
    }
    public static IntPoint3 operator -(IntPoint3 pFirst, IntPoint3 pSecond)
    {
        return new IntPoint3(pFirst.x - pSecond.x, pFirst.y - pSecond.y, pFirst.z
            - pSecond.z);
    }
    public static IntPoint3 operator +(IntPoint3 pFirst, IntPoint3 pSecond)
    {
        return new IntPoint3(pFirst.x + pSecond.x, pFirst.y + pSecond.y, pFirst.z + pSecond.z);
    }
    public static IntPoint3 operator *(IntPoint3 pFirst, int pSecond)
    {
        return new IntPoint3(pFirst.x * pSecond, pFirst.y * pSecond, pFirst.z * pSecond);
    }
    public static IntPoint3 operator /(IntPoint3 pFirst, int pSecond)
    {
        return new IntPoint3(pFirst.x / pSecond, pFirst.y / pSecond, pFirst.z / pSecond);
    }
    public static bool operator ==(IntPoint3 pFirst, IntPoint3 pSecond)
    {
        return (pFirst.x == pSecond.x && pFirst.y == pSecond.y && pFirst.z == pSecond.z);
    }
    public static bool operator !=(IntPoint3 pFirst, IntPoint3 pSecond)
    {
        return !(pFirst == pSecond);
    }
    public override bool Equals(object obj)
    {
        if (!(obj is IntPoint3))
            return false;
        return (IntPoint3)obj == this;
    }
    public override int GetHashCode()
    {
        return -y ^ z ^ x;
    }
    public override string ToString()
    {
        return "(" + x + "," + y + ", " + z + ")";
    }
    public static IntPoint3 Round(Vector3 pPosition, float StepSize)
    {
        return new IntPoint3(
            (int)(Mathf.Round(pPosition.x / StepSize) * StepSize),
            (int)(Mathf.Round(pPosition.y / StepSize) * StepSize),
            (int)(Mathf.Round(pPosition.z / StepSize) * StepSize)
            );
    }
    public static IntPoint3 Normalize(IntPoint3 p)
    {
        int absX = Mathf.Abs(p.x);
        int absY = Mathf.Abs(p.y);
        int absZ = Mathf.Abs(p.z);
        if (absX > absY && absX > absZ)
        {
            if (p.x > 0)
                return IntPoint3.right;
            else
                return -IntPoint3.right;
        }
        if (absY > absX && absY > absZ)
        {
            if (p.y > 0)
                return IntPoint3.up;
            else
                return -IntPoint3.up;
        }
        if (absZ > absX && absZ > absY)
        {
            if (p.z > 0)
                return IntPoint3.forward;
            else
                return -IntPoint3.forward;
        }
        return IntPoint3.zero;
    }
    public static IntPoint3 Normalize(Vector3 pVector)
    {
        float absX = Mathf.Abs(pVector.x);
        float absY = Mathf.Abs(pVector.y);
        float absZ = Mathf.Abs(pVector.z);
        if ( absX > absY && absX > absZ)
        {
            if (pVector.x > 0)
                return IntPoint3.right;
            else
                return -IntPoint3.right;
        }
        if (absY > absX && absY > absZ)
        {
            if (pVector.y > 0)
                return IntPoint3.up;
            else
                return -IntPoint3.up;
        }
        if (absZ > absX && absZ > absY)
        {
            if (pVector.z > 0)
                return IntPoint3.forward;
            else
                return -IntPoint3.forward;
        }
        return IntPoint3.zero;
    }
    
}