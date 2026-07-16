using UnityEngine;
using UnityEngine.UI;

namespace GridPulse
{
    public class GameActionsUI : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;

        [Header("Action Buttons")]
        [SerializeField] private Button _undoBtn;
        [SerializeField] private Button _restartBtn;

        private void Start()
        {
            _undoBtn.onClick.RemoveAllListeners();
            _restartBtn.onClick.RemoveAllListeners();

            _undoBtn.onClick.AddListener(() => _gameManager.Undo());
            _restartBtn.onClick.AddListener(() => _gameManager.Restart());
        }
    }
}