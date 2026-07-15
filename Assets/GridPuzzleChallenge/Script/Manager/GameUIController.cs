using TMPro;
using UnityEngine;

namespace GridPulse.Presentation
{
    public class GameUIController : MonoBehaviour
    {
        [Header("Top Bar")]
        [SerializeField] private TMP_Text _movesText;
        [SerializeField] private TMP_Text _pulseText;
        [SerializeField] private TMP_Text _undoText;

        [Header("Status")]
        [SerializeField] private TMP_Text _statusText;

        public void SetMoves(int current, int max)
        {
            _movesText.text = $"MOVES {current}/{max}";
        }

        public void SetPulse(int current, int max)
        {
            _pulseText.text = $"PULSE {current}/{max}";
        }

        public void SetUndoCount(int count)
        {
            _undoText.text = $"UNDO {count}";
        }

        public void SetStatus(string message)
        {
            _statusText.text = message;
        }

        public void ClearStatus()
        {
            _statusText.text = string.Empty;
        }
    }
}