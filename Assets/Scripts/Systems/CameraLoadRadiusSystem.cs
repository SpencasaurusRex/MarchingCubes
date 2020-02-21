using Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

namespace Systems
{
    public class CameraLoadRadiusSystem : ComponentSystem
    {
        EntityQuery settingsQuery;
        EntityQuery worldInfoQuery;
        
        EntityArchetype loadChunkArchetype;
        EntityArchetype generateMeshArchetype;

        protected override void OnCreate()
        {
            settingsQuery = EntityManager.CreateEntityQuery(typeof(Settings));
            worldInfoQuery = EntityManager.CreateEntityQuery(typeof(WorldInfo));

            loadChunkArchetype = EntityManager.CreateArchetype(typeof(LoadChunk));
            generateMeshArchetype = EntityManager.CreateArchetype(typeof(GenerateMesh));
        }

        protected override void OnUpdate()
        {
            var settingsArray = settingsQuery.ToComponentDataArray<Settings>(Allocator.TempJob);
            var settings = settingsArray[0];

            var worldInfoEntity = worldInfoQuery.GetSingletonEntity();
            var worldInfo = EntityManager.GetComponentObject<WorldInfo>(worldInfoEntity, ComponentType.ReadOnly(typeof(WorldInfo)));

            Entities.WithAllReadOnly<CameraTag>().ForEach((Entity _, ref LocalToWorld r) =>
            {
                var currentChunk = ChunkCoord.FromPosition(r.Position);

                // Load surrounding chunks
                ChunkCoord coord;
                for (int dx = -settings.ChunkLoadRadius; dx <= settings.ChunkLoadRadius; dx++)
                {
                    for (int dz = -settings.ChunkLoadRadius; dz <= settings.ChunkLoadRadius; dz++)
                    {
                        coord = currentChunk.Delta(dx, 0, dz);
                        if (worldInfo.Chunks.ContainsKey(coord))
                        {
                            continue;
                        }

                        Entity e = EntityManager.CreateEntity(loadChunkArchetype);
                        EntityManager.SetComponentData(e, new LoadChunk { Coord = coord } );
                    }
                }

                // Generate mesh of surrounding chunks
                for (int dx = -settings.MeshGenerateRadius; dx <= settings.MeshGenerateRadius; dx++)
                {
                    for (int dz = -settings.MeshGenerateRadius; dz <= settings.MeshGenerateRadius; dz++)
                    {
                        coord = currentChunk.Delta(dx, 0, dz);
                        if (worldInfo.Chunks.ContainsKey(coord))
                        {
                            continue;
                        }

                        Entity e = EntityManager.CreateEntity(generateMeshArchetype);
                        EntityManager.SetComponentData(e, new GenerateMesh { Coord = coord } );
                    }
                }
            });

            settingsArray.Dispose();
        }
    }
}
