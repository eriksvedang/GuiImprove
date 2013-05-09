using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
[System.Flags]
public enum Direction : int
{
    ZERO            = 0x000,
    RIGHT           = 0x001,
    UP_RIGHT        = 0x002,
    UP              = 0x004,
    UP_LEFT         = 0x008,
    LEFT            = 0x010,
    DOWN_LEFT       = 0x020,
    DOWN            = 0x040,
    DOWN_RIGHT      = 0x080
}
[Serializable]
public struct IntPoint
{
    public int x;
    public int y;
    public IntPoint( int pX, int pY )
    {
        x = pX;
        y = pY;
    }
    public float EuclidianDistanceTo( IntPoint pOtherPoint )
    {
        IntPoint deltaPoint = pOtherPoint - this;
        return (float)Math.Sqrt(deltaPoint.x * deltaPoint.x + deltaPoint.y * deltaPoint.y);
    }
    public int ManhattanDistanceTo( IntPoint pOtherPoint )
    {
        IntPoint deltaPoint = pOtherPoint - this;
        deltaPoint.x = deltaPoint.x < 0 ? -deltaPoint.x : deltaPoint.x;
        deltaPoint.y = deltaPoint.y < 0 ? -deltaPoint.y : deltaPoint.y;
        return deltaPoint.x + deltaPoint.y;
    }
    // UNITY unit circle is reversed
    public static IntPoint Zero = new IntPoint(0, 0);
    public static IntPoint Up = new IntPoint(0, 1);
    public static IntPoint Right = new IntPoint(1, 0);
    public static IntPoint Left = new IntPoint(-1, 0);
    public static IntPoint Down = new IntPoint(0, -1);
    public static IntPoint UpRight = new IntPoint(1, 1);
    public static IntPoint UpLeft = new IntPoint(-1, 1);
    public static IntPoint DownRight = new IntPoint(1, -1);
    public static IntPoint DownLeft = new IntPoint(-1, -1);
    public static IntPoint DirectionToIntPoint(Direction direction)
    {
        switch (direction)
        {
            case Direction.UP:
                return Up;
            case Direction.DOWN:
                return Down;
            case Direction.RIGHT:
                return Right;
            case Direction.LEFT:
                return Left;
            case Direction.UP_RIGHT:
                return UpRight;
            case Direction.UP_LEFT:
                return UpLeft;
            case Direction.DOWN_RIGHT:
                return DownRight;
            case Direction.DOWN_LEFT:
                return DownLeft;
            default:
                return Zero;
        }
    }
    public Direction ToDirection()
    {
        if (this == IntPoint.Up ) { return Direction.UP; }
        else if (this == IntPoint.UpLeft ) { return Direction.UP_LEFT; }
        else if (this == IntPoint.UpRight) { return Direction.UP_RIGHT; }
        else if (this == IntPoint.Right) { return Direction.RIGHT; }
        else if (this == IntPoint.Left) { return Direction.LEFT; }
        else if (this == IntPoint.Down) { return Direction.DOWN; }
        else if (this == IntPoint.DownLeft) { return Direction.DOWN_LEFT; }
        else if (this == IntPoint.DownRight) { return Direction.DOWN_RIGHT; }
        else { return Direction.ZERO; }
    }
    public static IntPoint operator-( IntPoint pFirst, IntPoint pSecond )
    {
        return new IntPoint(pFirst.x - pSecond.x, pFirst.y - pSecond.y);
    }
    public static IntPoint operator+(IntPoint pFirst, IntPoint pSecond)
    {
        return new IntPoint(pFirst.x + pSecond.x, pFirst.y + pSecond.y);
    }
    public static IntPoint operator *(int pFirst, IntPoint pSecond)
    {
        return pSecond * pFirst;
    }
    public static IntPoint operator *(IntPoint pFirst, int pSecond)
    {
        return new IntPoint(pFirst.x * pSecond, pFirst.y * pSecond);
    }
    public static IntPoint operator /(IntPoint pFirst, int pSecond)
    {
        return new IntPoint(pFirst.x / pSecond, pFirst.y / pSecond);
    }
    public static bool operator ==(IntPoint pFirst, IntPoint pSecond)
    {
        return (pFirst.x == pSecond.x && pFirst.y == pSecond.y);
    }
    public static bool operator !=(IntPoint pFirst, IntPoint pSecond)
    {
        return !(pFirst == pSecond);
    }
    public override bool Equals(object obj)
    {
        if (!(obj is IntPoint))
            return false;
        return (IntPoint)obj == this;
    }
    public override int GetHashCode()
    {
        return (x % 100 + y * 100) + 42;
    }
    public override string ToString()
    {
        return "(" + x + "," + y + ")";
    }
}