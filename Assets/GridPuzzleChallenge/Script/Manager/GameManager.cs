using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SwipeInputController _swipeInputController;
    [SerializeField] private GridManager _gridManager;
    [SerializeField] private GameUIController _gameUIController;

    private int _moveCount;
    private int _maxMoves;
    private int _maxPulse;

    private int _currentMoves;
    private int _currentPulse;

    private MovementSystem _movementSystem;
    private bool _isGameOver;

    private void Start()
    {
        _swipeInputController.DirectionRequested += HandleDirection;
        InitializeGame();
    }

    private void InitializeGame()
    {
        _movementSystem = new MovementSystem(_gridManager.Grid, _gridManager);

        _currentMoves = 0;
        _currentPulse = 0;
        _moveCount = 0;

        _maxMoves = _gridManager.LevelData.MaxMoves;
        _maxPulse = _gridManager.LevelData.MaxPulse;
        _currentMoves = _maxMoves;

        _isGameOver = false;

        _gameUIController.SetMoves(_currentMoves, _maxMoves);
        _gameUIController.SetPulse(_currentPulse, _maxPulse);
        _gameUIController.SetUndoCount(0);
        _gameUIController.ClearStatus();
        _gameUIController.HideGameOver();
    }

    private void HandleDirection(Direction direction)
    {
        if (_isGameOver)
            return;

        MoveResult result = _movementSystem.Move(direction);

        switch (result)
        {
            case MoveResult.Moved:
                RegisterMove();
                _gameUIController.ClearStatus();
                break;

            case MoveResult.CollectedEnergy:
                _currentPulse++;
                RegisterMove();
                _gameUIController.SetStatus(GameMessages.EnergyCollected);
                _gameUIController.SetPulse(_currentPulse, _maxPulse);
                break;

            case MoveResult.BrokeCrack:
                _currentPulse--;
                RegisterMove();
                _gameUIController.SetStatus(GameMessages.CrackBroken);
                _gameUIController.SetPulse(_currentPulse, _maxPulse);
                break;

            case MoveResult.GoalReached:
                _isGameOver = true;
                RegisterMove();
                _gameUIController.SetStatus(GameMessages.GoalReached);
                _gameUIController.ShowGameOver(GameMessages.GridCleared);
                break;

            case MoveResult.OutOfBounds:
                _gameUIController.SetStatus(GameMessages.OutOfBounds);
                break;

            case MoveResult.Blocked:
                _gameUIController.SetStatus(GameMessages.Blocked);
                break;
        }
    }

    private void RegisterMove()
    {
        _moveCount++;
        _currentMoves--;

        _gameUIController.SetMoves(_currentMoves, _maxMoves);
        _gameUIController.SetUndoCount(_moveCount);

        if (_currentMoves == 0)
        {
            _isGameOver = true;
            _gameUIController.ShowGameOver(GameMessages.OutOfMoves);
        }
    }

    public void Restart()
    {
        _gridManager.Restart();

        InitializeGame();
    }

    private void OnDestroy()
    {
        _swipeInputController.DirectionRequested -= HandleDirection;
    }
}
