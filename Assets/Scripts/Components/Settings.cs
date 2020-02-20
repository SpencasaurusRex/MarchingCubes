using Unity.Entities;

namespace Components
{
    [GenerateAuthoringComponent]
    public struct Settings : IComponentData
    {
        public int ChunkLoadRadius;
        public int MeshGenerateRadius;
    }
}
