using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDatabase", menuName = "Grid Puzzle/Level Database")]
public class LevelDatabase : ScriptableObject
{
    public List<LevelData> Levels = new();
}