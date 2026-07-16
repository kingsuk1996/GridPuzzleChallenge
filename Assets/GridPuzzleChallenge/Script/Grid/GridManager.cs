using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Level")]
    private LevelData _levelData;
    [SerializeField] private CellVisualDatabase _cellVisualDatabase;

    [Header("References")]
    [SerializeField] private GridCell _gridCellPrefab;
    [SerializeField] private RectTransform _boardRoot;

    private GridData gridData;
    private GridCell[,] cellViews;

    public GridData Grid => gridData;
    public LevelData LevelData => _levelData;

    public void LoadLevel(LevelData levelData)
    {
        _levelData = levelData;

        gridData = new GridData(levelData.Width, levelData.Height);
        cellViews = new GridCell[levelData.Width, levelData.Height];

        SpawnGrid();
        PopulateLevel();
    }

    private void SpawnGrid()
    {
        foreach (Transform child in _boardRoot)
        {
            Destroy(child.gameObject);
        }

        float cellSize = 100f;

        for (int y = 0; y < _levelData.Height; y++)
        {
            for (int x = 0; x < _levelData.Width; x++)
            {
                GridCell cell = Instantiate(_gridCellPrefab, _boardRoot);

                cell.name = $"Cell {x},{y}";
                cell.Initialize(new Vector2Int(x, y), _cellVisualDatabase);

                RectTransform rt = cell.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(x * cellSize, -y * cellSize);

                cellViews[x, y] = cell;
            }
        }
    }

    private void PopulateLevel()
    {
        for (int x = 0; x < _levelData.Width; x++)
        {
            for (int y = 0; y < _levelData.Height; y++)
            {
                gridData.Set(x, y, CellType.Empty);
            }
        }

        foreach (CellData cell in _levelData.Cells)
        {
            if (!IsInside(cell.Position.x, cell.Position.y))
                continue;

            gridData.Set(cell.Position.x, cell.Position.y, cell.Type);
        }

        RefreshGrid();
    }

    public void RefreshGrid()
    {
        for (int x = 0; x < _levelData.Width; x++)
        {
            for (int y = 0; y < _levelData.Height; y++)
            {
                cellViews[x, y].SetType(gridData.Get(x, y));
            }
        }
    }

    public bool IsInside(int x, int y)
    {
        return x >= 0 &&
               x < _levelData.Width &&
               y >= 0 &&
               y < _levelData.Height;
    }
}