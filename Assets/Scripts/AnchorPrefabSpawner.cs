using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Meta.XR.MRUtilityKit;

public class AnchorPrefabSpawner : MonoBehaviour
{
    [SerializeField] private MRUKAnchor.SceneLabels _label;
    [SerializeField] private GameObject _prefab;

    private void Start()
    {
        if (MRUK.Instance is null) return;

        MRUK.Instance.RegisterSceneLoadedCallback(() =>
        {
            SpawnPrefabs(MRUK.Instance.GetCurrentRoom());
        });
    }

    public void SpawnPrefabs(MRUKRoom room)
    {
        foreach (var anchor in room.Anchors)
        {
            if (anchor.Label == _label)
            {
                SpawnPrefab(anchor);
            }
        }
    }

    private void SpawnPrefab(MRUKAnchor anchorInfo)
    {
        if (anchorInfo.PlaneRect.HasValue)
        {
            var prefab = Instantiate(_prefab, anchorInfo.transform);
            prefab.transform.parent = anchorInfo.transform;

            Vector3 prefabSize = Vector3.one;

            Vector2 planeSize = anchorInfo.PlaneRect.Value.size;
            Vector2 scale = new Vector2(planeSize.x / prefabSize.x, planeSize.y / prefabSize.y);

            Vector2 prefabPivot = Vector3.zero;
            Vector2 planePivot = anchorInfo.PlaneRect.Value.center;

            prefabPivot.Scale(scale);
            prefab.transform.localPosition = new Vector3(planePivot.x - prefabPivot.x, planePivot.y - prefabPivot.y, 0);
            prefab.transform.localRotation = Quaternion.identity;
            prefab.transform.localScale = new Vector3(scale.x, scale.y, 0.2f);
        }
    }
}