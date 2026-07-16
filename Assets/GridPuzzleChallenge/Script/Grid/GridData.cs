
using UnityEngine;

[System.Serializable]
public class GridData
{
    private CellType[,] cells;

    public int Width { get; }
    public int Height { get; }

    public GridData(int width, int height)
    {
        Width = width;
        Height = height;
        cells = new CellType[width, height];
    }

    public CellType Get(int x, int y)
    {
        return cells[x, y];
    }

    public void Set(int x, int y, CellType value)
    {
        cells[x, y] = value;
    }

    public Vector2Int Find(CellType type)
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                if (cells[x, y] == type)
                    return new Vector2Int(x, y);
            }
        }

        return new Vector2Int(-1, -1);
    }

    public void Move(Vector2Int from, Vector2Int to)
    {
        CellType type = Get(from.x, from.y);

        Set(from.x, from.y, CellType.Empty);
        Set(to.x, to.y, type);
    }

    public CellType[,] Clone()
    {
        CellType[,] copy = new CellType[Width, Height];

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                copy[x, y] = cells[x, y];
            }
        }
        return copy;
    }

    public void Restore(CellType[,] state)
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                cells[x, y] = state[x, y];
            }
        }
    }
}