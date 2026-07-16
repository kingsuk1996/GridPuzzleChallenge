using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GridPulse
{
    public class GridCell : MonoBehaviour
    {
        [SerializeField] private Image _background;
        [SerializeField] private TMP_Text _label;

        private CellVisualDatabase _database;

        public void Initialize(Vector2Int position, CellVisualDatabase database)
        {
            _database = database;
        }

        public void SetType(CellType type)
        {
            CellVisualData data = _database.GetVisual(type);

            if (data == null)
                return;

            _background.color = data.BackgroundColor;
            _label.text = data.Label;
            _label.color = data.LabelColor;
        }
    }
}