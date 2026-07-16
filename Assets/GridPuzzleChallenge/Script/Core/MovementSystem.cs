using UnityEngine;

namespace GridPulse
{
    public class MovementSystem
    {
        private readonly GridData grid;
        private readonly GridManager gridManager;

        public MovementSystem(GridData grid, GridManager gridManager)
        {
            this.grid = grid;
            this.gridManager = gridManager;
        }

        public MoveResult Move(Direction direction)
        {
            Vector2Int player = grid.Find(CellType.Player);
            Vector2Int next = player + DirectionToVector(direction);

            if (!gridManager.IsInside(next.x, next.y))
                return MoveResult.OutOfBounds;

            CellType target = grid.Get(next.x, next.y);

            switch (target)
            {
                case CellType.Wall:
                    return MoveResult.Blocked;

                case CellType.Goal:

                    grid.Move(player, next);
                    gridManager.RefreshGrid();

                    return MoveResult.GoalReached;

                case CellType.Energy:

                    grid.Move(player, next);
                    gridManager.RefreshGrid();

                    return MoveResult.CollectedEnergy;

                case CellType.Crack:

                    grid.Move(player, next);
                    gridManager.RefreshGrid();

                    return MoveResult.BrokeCrack;

                default:

                    grid.Move(player, next);
                    gridManager.RefreshGrid();

                    return MoveResult.Moved;
            }
        }

        private Vector2Int DirectionToVector(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up: return Vector2Int.down;
                case Direction.Down: return Vector2Int.up;
                case Direction.Left: return Vector2Int.left;
                case Direction.Right: return Vector2Int.right;
            }

            return Vector2Int.zero;
        }
    }
}