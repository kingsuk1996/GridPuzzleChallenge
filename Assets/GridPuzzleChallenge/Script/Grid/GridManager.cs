using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Level")]
    [SerializeField] private LevelData _levelData;
    [SerializeField] private CellVisualDatabase _cellVisualDatabase;

    [Header("References")]
    [SerializeField] private GridCell _gridCellPrefab;
    [SerializeField] private RectTransform _boardRoot;

    private GridData gridData;
    private GridCell[,] cellViews;

    public GridData Grid => gridData;

    private void Awake()
    {
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        if (_levelData == null)
        {
            Debug.LogError("LevelData is not assigned!");
            return;
        }

        gridData = new GridData(_levelData.Width, _levelData.Height);
        cellViews = new GridCell[_levelData.Width, _levelData.Height];

        SpawnGrid();
        LoadLevel();
    }

    private void SpawnGrid()
    {
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

    private void LoadLevel()
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

    public GridCell GetView(int x, int y)
    {
        return cellViews[x, y];
    }

    public bool IsInside(int x, int y)
    {
        return x >= 0 &&
               x < _levelData.Width &&
               y >= 0 &&
               y < _levelData.Height;
    }
}