using System;
using System.Collections.Generic;
using UnityEngine;

namespace GridPulse
{
    [CreateAssetMenu(fileName = "CellVisualDatabase", menuName = "Grid Puzzle/Cell Visual Database")]
    public class CellVisualDatabase : ScriptableObject
    {
        [SerializeField]
        private List<CellVisualData> visuals = new();

        private Dictionary<CellType, CellVisualData> _lookup;

        private void OnEnable()
        {
            BuildLookup();
        }

        private void BuildLookup()
        {
            _lookup = new Dictionary<CellType, CellVisualData>();

            foreach (var visual in visuals)
            {
                _lookup[visual.Type] = visual;
            }
        }

        public CellVisualData GetVisual(CellType type)
        {
            if (_lookup == null || _lookup.Count == 0)
                BuildLookup();

            _lookup.TryGetValue(type, out var visual);
            return visual;
        }
    }

    [Serializable]
    public class CellVisualData
    {
        public CellType Type;
        public Color BackgroundColor;
        public string Label;
        public Color LabelColor = Color.white;
    }
}