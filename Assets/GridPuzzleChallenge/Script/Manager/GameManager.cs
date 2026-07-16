using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Level Database")]
    [SerializeField] private LevelDatabase _levelDatabase;

    [Header("Script Reference")]
    [SerializeField] private SwipeInputController _swipeInputController;
    [SerializeField] private GridManager _gridManager;
    [SerializeField] private GameUIController _gameUIController;

    private int _maxMoves;
    private int _maxPulse;

    private int _currentMoves;
    private int _currentPulse;
    private int _currentLevelIndex = 0;

    private bool _isGameOver;

    private MovementSystem _movementSystem;
    private UndoSystem _undoSystem;

    private void Awake()
    {
        _gridManager.LoadLevel(_levelDatabase.Levels[_currentLevelIndex]);
    }

    private void Start()
    {
        _swipeInputController.DirectionRequested += HandleDirection;
        InitializeGame();
    }

    private void InitializeGame()
    {
        _movementSystem = new MovementSystem(_gridManager.Grid, _gridManager);
        _undoSystem = new UndoSystem();

        _currentMoves = 0;
        _currentPulse = 0;

        _maxMoves = _gridManager.LevelData.MaxMoves;
        _maxPulse = _gridManager.LevelData.MaxPulse;
        _currentMoves = _maxMoves;

        _isGameOver = false;

        _gameUIController.SetMoves(_currentMoves, _maxMoves);
        _gameUIController.SetPulse(_currentPulse, _maxPulse);
        _gameUIController.SetUndoCount(0);

        _gameUIController.ClearStatus();
        _gameUIController.SetStatus($"Grid cleared in {_maxMoves} moves!");

        _gameUIController.HideGameOver();
    }

    private void HandleDirection(Direction direction)
    {
        if (_isGameOver)
            return;

        GameState previousState = CreateGameState();

        MoveResult result = _movementSystem.Move(direction);

        switch (result)
        {
            case MoveResult.Moved:
                _undoSystem.Save(previousState);
                RegisterMove();
                _gameUIController.ClearStatus();
                break;

            case MoveResult.CollectedEnergy:
                _undoSystem.Save(previousState);
                _currentPulse++;
                RegisterMove();
                _gameUIController.SetStatus(GameMessages.EnergyCollected);
                _gameUIController.SetPulse(_currentPulse, _maxPulse);
                break;

            case MoveResult.BrokeCrack:
                _undoSystem.Save(previousState);
                _currentPulse--;
                RegisterMove();
                _gameUIController.SetStatus(GameMessages.CrackBroken);
                _gameUIController.SetPulse(_currentPulse, _maxPulse);
                break;

            case MoveResult.GoalReached:
                _undoSystem.Save(previousState);
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

        _gameUIController.SetUndoCount(_undoSystem.Count);
    }

    private GameState CreateGameState()
    {
        return new GameState
        {
            Grid = _gridManager.Grid.Clone(),
            MovesRemaining = _currentMoves,
            Pulse = _currentPulse
        };
    }

    private void RegisterMove()
    {
        _currentMoves--;
        _gameUIController.SetMoves(_currentMoves, _maxMoves);

        if (_currentMoves == 0)
        {
            _isGameOver = true;
            _gameUIController.ShowGameOver(GameMessages.OutOfMoves);
        }
    }

    #region Game Actions

    public void Undo()
    {
        if (!_undoSystem.TryUndo(out GameState state))
            return;

        _gridManager.Grid.Restore(state.Grid);

        _currentMoves = state.MovesRemaining;
        _currentPulse = state.Pulse;

        _gridManager.RefreshGrid();

        _gameUIController.SetMoves(_currentMoves, _maxMoves);
        _gameUIController.SetPulse(_currentPulse, _maxPulse);
        _gameUIController.SetUndoCount(_undoSystem.Count);

        _gameUIController.ClearStatus();

        _isGameOver = false;
        _gameUIController.HideGameOver();
    }

    public void Restart()
    {
        _gridManager.LoadLevel(_levelDatabase.Levels[_currentLevelIndex]);
        InitializeGame();
    }

    #endregion Game Actions

    private void OnDestroy()
    {
        _swipeInputController.DirectionRequested -= HandleDirection;
    }
}
