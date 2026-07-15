using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Level")]
    [SerializeField] private LevelData levelData;
    [SerializeField] private CellVisualDatabase cellVisualDatabase;

    [Header("References")]
    [SerializeField] private GridCell cellPrefab;
    [SerializeField] private RectTransform boardRoot;

    private GridData gridData;
    private GridCell[,] cellViews;

    public GridData Grid => gridData;

    private void Awake()
    {
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        if (levelData == null)
        {
            Debug.LogError("LevelData is not assigned!");
            return;
        }

        gridData = new GridData(levelData.Width, levelData.Height);
        cellViews = new GridCell[levelData.Width, levelData.Height];

        SpawnGrid();
        LoadLevel();
    }

    private void SpawnGrid()
    {
        float cellSize = 100f;

        for (int y = 0; y < levelData.Height; y++)
        {
            for (int x = 0; x < levelData.Width; x++)
            {
                GridCell cell = Instantiate(cellPrefab, boardRoot);

                cell.name = $"Cell {x},{y}";
                cell.Initialize(new Vector2Int(x, y), cellVisualDatabase);

                RectTransform rt = cell.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(x * cellSize, -y * cellSize);

                cellViews[x, y] = cell;
            }
        }
    }

    private void LoadLevel()
    {
        for (int x = 0; x < levelData.Width; x++)
        {
            for (int y = 0; y < levelData.Height; y++)
            {
                gridData.Set(x, y, CellType.Empty);
            }
        }

        foreach (CellData cell in levelData.Cells)
        {
            if (!IsInside(cell.Position.x, cell.Position.y))
                continue;

            gridData.Set(cell.Position.x, cell.Position.y, cell.Type);
        }

        RefreshGrid();
    }

    public void RefreshGrid()
    {
        for (int x = 0; x < levelData.Width; x++)
        {
            for (int y = 0; y < levelData.Height; y++)
            {
                cellViews[x, y].SetType(gridData.Get(x, y));
            }
        }
    }

    public GridCell GetView(int x, int y)
    {
        return cellViews[x, y];
    }

    private bool IsInside(int x, int y)
    {
        return x >= 0 &&
               x < levelData.Width &&
               y >= 0 &&
               y < levelData.Height;
    }
}