using Unity.Entities;

namespace Components
{
    public struct LoadChunk : IComponentData
    {
        public ChunkCoord Coord;
    }
}
