using System;

public struct Vector2Int
{
    public float X {get;}
    public float Y {get;}

    public Vector2Int(int x, int y){
        X = x;
        Y = y;
    }

    public static Vector2Int operator + (Vector2Int v1, Vector2Int v2){
        return new Vector2Int(v1.X + v2.X, v1.Y + v2.Y);
    }

    public static double CalculateDistance(Vector2Int v1, Vector2Int v2){
        return Math.Sqrt(
                        Math.Pow(v2.X - v1.X, 2) + 
                        Math.Pow(v2.Y - v1.Y, 2)
                        );
    }
}