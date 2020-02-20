
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public static class Util
{
    public static Vector3 Position(this LocalToWorld localToWorld)
    {
        float4 pos = localToWorld.Value.c3;
        return new Vector3(pos.x, pos.y, pos.z);
    }

    public static ChunkCoord Delta(this ChunkCoord coord, int dx, int dy, int dz) =>
        new ChunkCoord(coord.X + dx, coord.Y + dy, coord.Z + dz);
}