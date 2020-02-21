using Components;
using Unity.Entities;
using Unity.Jobs;

namespace Systems
{
    [UpdateAfter(typeof(CameraLoadRadiusSystem))]
    public class LoadChunkSystem : JobComponentSystem
    {
        EntityQuery loadChunkQuery;
        EndSimulationEntityCommandBufferSystem commandBufferSystem;
        WorldInfo worldInfo;

        protected override void OnCreate()
        {
            loadChunkQuery = GetEntityQuery(ComponentType.ReadOnly<LoadChunk>());
            commandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
            
            var worldInfoEntity = EntityManager.CreateEntityQuery(ComponentType.ReadWrite<WorldInfo>()).GetSingletonEntity();
            worldInfo = EntityManager.GetComponentObject<WorldInfo>(worldInfoEntity);
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var commandBuffer = commandBufferSystem.CreateCommandBuffer().ToConcurrent();
            var worldInfo = this.worldInfo;
            var jobHandle = Entities.ForEach((Entity entity, int entityInQueryIndex, ref LoadChunk loadChunk) =>
            {
                var coord = loadChunk.Coord;
                var chunk = new Chunk(coord);

                commandBuffer.RemoveComponent<LoadChunk>(0, entity);
                commandBuffer.AddComponent(0, entity, chunk);
                worldInfo.Chunks.Add(coord, chunk);
            }).Schedule(inputDeps);

            commandBufferSystem.AddJobHandleForProducer(jobHandle);

            return default;
        }
    }
}
