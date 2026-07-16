using System;

namespace GridPulse
{
    [Serializable]
    public class GameState
    {
        public CellType[,] Grid;
        public int MovesRemaining;
        public int Pulse;
        public bool IsGameOver;
    }
}