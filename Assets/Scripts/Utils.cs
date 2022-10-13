using UnityEngine;

public static class Utils
{
    public static Vector3 Multiply( Vector3 a, Vector3 b )
    {
        return new( a.x * b.x, a.y * b.y, a.z * b.z );
    }
}