using UnityEngine;
using UnityEngine.UI;

namespace GridPulse
{
    public class DirectionUIManager : MonoBehaviour
    {
        [SerializeField] private SwipeInputController _inputController;

        [SerializeField] private Button _upbtn;
        [SerializeField] private Button _downbtn;
        [SerializeField] private Button _leftbtn;
        [SerializeField] private Button _rightbtn;

        private void Start()
        {
            _upbtn.onClick.RemoveAllListeners();
            _downbtn.onClick.RemoveAllListeners();
            _leftbtn.onClick.RemoveAllListeners();
            _rightbtn.onClick.RemoveAllListeners();

            _upbtn.onClick.AddListener(UpButtonClick);
            _downbtn.onClick.AddListener(DownButtonClick);
            _leftbtn.onClick.AddListener(LeftButtonClick);
            _rightbtn.onClick.AddListener(RightButtonClick);
        }

        private void UpButtonClick()
        {
            _inputController.RequestDirection(Direction.Up);
        }

        private void DownButtonClick()
        {
            _inputController.RequestDirection(Direction.Down);
        }

        private void LeftButtonClick()
        {
            _inputController.RequestDirection(Direction.Left);
        }

        private void RightButtonClick()
        {
            _inputController.RequestDirection(Direction.Right);
        }
    }
}