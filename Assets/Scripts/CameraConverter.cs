using Components;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

[RequiresEntityConversion]
[RequireComponent(typeof(Camera))]
public class CameraConverter : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new CopyTransformFromGameObject());
        dstManager.AddComponentData(entity, new CameraTag());
    }
}