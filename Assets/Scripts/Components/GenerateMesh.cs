using Unity.Entities;

namespace Components
{
    public struct GenerateMesh : IComponentData
    {
        public ChunkCoord Coord;
    }
}
