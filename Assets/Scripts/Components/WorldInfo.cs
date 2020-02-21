using Unity.Collections;
using Unity.Entities;

namespace Components
{
    [GenerateAuthoringComponent]
    public class WorldInfo : IComponentData
    {
        public NativeHashMap<ChunkCoord, Chunk> Chunks = new NativeHashMap<ChunkCoord, Chunk>();
    }
}
