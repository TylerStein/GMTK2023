using UnityEngine;

public static class Distance
{
    public static float ManhattanDistance(float x1, float x2, float y1, float y2)
    {
        return Mathf.Abs(x1 - x2) + Mathf.Abs(y1 - y2);
    }
    public static int ManhattanDistance(int x1, int x2, int y1, int y2)
    {
        return Mathf.RoundToInt(Mathf.Abs(x1 - x2) + Mathf.Abs(y1 - y2));
    }
}
