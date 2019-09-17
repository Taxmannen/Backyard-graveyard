using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <author>Simon</author>
/// </summary>
[CreateAssetMenu(fileName = "Level", menuName = "Level manager/Levels/Level")]
public class LevelSO : ScriptableObject
{
    [Header("Waves")]
    public EnemyWaveSO[] gameWaves;
}
