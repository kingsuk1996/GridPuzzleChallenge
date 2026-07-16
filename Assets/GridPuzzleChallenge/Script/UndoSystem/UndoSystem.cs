using System.Collections.Generic;

public class UndoSystem
{
    private readonly Stack<GameState> _history = new();

    public int Count => _history.Count;

    public void Save(GameState state)
    {
        _history.Push(state);
    }

    public bool TryUndo(out GameState state)
    {
        if (_history.Count == 0)
        {
            state = null;
            return false;
        }

        state = _history.Pop();
        return true;
    }

    public void Clear()
    {
        _history.Clear();
    }
}