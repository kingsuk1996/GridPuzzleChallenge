using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level_", menuName = "Grid Puzzle/Level")]
public class LevelData : ScriptableObject
{
    [Header("Grid")]
    public int Width = 6;
    public int Height = 6;

    [Header("Cells")]
    public List<CellData> Cells = new();
}

[Serializable]
public class CellData
{
    public Vector2Int Position;
    public CellType Type;
}