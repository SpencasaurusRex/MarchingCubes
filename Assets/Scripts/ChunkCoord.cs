using System;
using Components;
using UnityEngine;

public struct  ChunkCoord : IEquatable<ChunkCoord>
{
    public int X;
    public int Y;
    public int Z;

    public ChunkCoord(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static ChunkCoord FromBlockCoords(int gx, int gy, int gz) =>
        new ChunkCoord(gx >> Chunk.Bits, gy >> Chunk.Bits, gz >> Chunk.Bits);

    public static ChunkCoord FromPosition(Vector3 position) =>
        FromBlockCoords((int) position.x, (int) position.y, (int) position.z);


    public bool Equals(ChunkCoord other)
    {
        return X == other.X && Y == other.Y && Z == other.Z;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = X;
            hashCode = (hashCode * 397) ^ Y;
            hashCode = (hashCode * 397) ^ Z;
            return hashCode;
        }
    }
}