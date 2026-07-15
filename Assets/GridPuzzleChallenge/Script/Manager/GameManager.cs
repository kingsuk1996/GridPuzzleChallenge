using GridPulse.Presentation;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SwipeInputController _swipeInputController;
    [SerializeField] private GridManager _gridManager;
    [SerializeField] private GameUIController _gameUIController;

    private int _moveCount;
    private const int MaxMoves = 14;

    private MovementSystem _movementSystem;

    private void Start()
    {
        _movementSystem = new MovementSystem(_gridManager.Grid, _gridManager);

        _swipeInputController.DirectionRequested += HandleDirection;

        _gameUIController.SetMoves(0, MaxMoves);
        _gameUIController.SetPulse(0, 1);
        _gameUIController.SetUndoCount(0);
        _gameUIController.ClearStatus();
    }

    private void HandleDirection(Direction direction)
    {
        MoveResult result = _movementSystem.Move(direction);

        switch (result)
        {
            case MoveResult.Moved:

                _moveCount++;
                _gameUIController.SetMoves(_moveCount, MaxMoves);
                _gameUIController.ClearStatus();
                break;

            case MoveResult.OutOfBounds:

                _gameUIController.SetStatus(GameMessages.OutOfBounds);
                break;

            case MoveResult.Blocked:

                _gameUIController.SetStatus(GameMessages.Blocked);
                break;

            case MoveResult.GoalReached:

                _gameUIController.SetStatus(GameMessages.GoalReached);
                break;
        }
    }

    private void OnDestroy()
    {
        _swipeInputController.DirectionRequested -= HandleDirection;
    }
}
