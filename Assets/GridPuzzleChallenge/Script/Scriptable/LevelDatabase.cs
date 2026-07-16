using System.Collections.Generic;
using UnityEngine;

namespace GridPulse
{
    [CreateAssetMenu(fileName = "LevelDatabase", menuName = "Grid Puzzle/Level Database")]
    public class LevelDatabase : ScriptableObject
    {
        public List<LevelData> Levels = new();
    }
}