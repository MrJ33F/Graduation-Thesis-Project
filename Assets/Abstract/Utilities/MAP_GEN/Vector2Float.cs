using System;

public struct Vector2Float
{
    public float X {get;}
    public float Y {get;}

    public Vector2Float(float x, float y){
        X = x;
        Y = y;
    }

    public static Vector2Float operator + (Vector2Float v1, Vector2Float v2){
        return new Vector2Float(v1.X + v2.X, v1.Y + v2.Y);
    }

    public static double CalculateDistance(Vector2Float v1, Vector2Float v2){
        return Math.Sqrt(
                        Math.Pow(v2.X - v1.X, 2) + 
                        Math.Pow(v2.Y - v1.Y, 2)
                        );
    }
}