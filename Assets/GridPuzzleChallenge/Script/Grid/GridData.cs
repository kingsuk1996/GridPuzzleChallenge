
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
}