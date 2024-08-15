using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTargets", menuName = "SOs/Targets")]
public class TargetsSO : ScriptableObject
{
    public enum TargetType
    {
        Mole,
        Ghost,
        Empty
    }

    public Dictionary<TargetType, GameObject> targetDict = new Dictionary<TargetType, GameObject>();
}
