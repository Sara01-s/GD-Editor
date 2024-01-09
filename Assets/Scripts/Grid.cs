using static Unity.Mathematics.math;
using UnityEngine;
using System;

public sealed class Grid<T> {

	public event Action<Grid<T>, int, int> OnGridObjectChanged;

	public int Width { get; private set; }
	public int Height { get; private set; }
	public float CellSize { get; private set; }
	public Vector3 OriginPosition { get; private set; }

	private readonly T[,] _grid;
	private readonly TextMesh[,] _debugTextGrid;
    
	public Grid(int width, int height, float cellSize, Vector3 originPosition, Func<Grid<T>, int, int, T> initGridObject) {
		Width = width;
		Height = height;
		CellSize = cellSize;
		OriginPosition = originPosition;

		_grid = new T[width, height];
		_debugTextGrid = new TextMesh[width, height];

		for (int x = 0; x < _grid.GetLength(0); x++) {
			for (int y = 0; y < _grid.GetLength(1); y++) {
				_grid[x, y] = initGridObject(this, x, y);
			}
		}

		for (int x = 0; x < _grid.GetLength(0); x++) {

			for (int y = 0; y < _grid.GetLength(1); y++) {

				_debugTextGrid[x, y] = Utils.CreateWorldText(new(_grid[x, y].ToString(), GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f, null, 10));

				Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100.0f);
				Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100.0f);
			}
			
			Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100.0f);
			Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100.0f);

			OnGridObjectChanged += (Grid<T> grid, int x, int y) => {
				_debugTextGrid[x, y].text = _grid[x, y]?.ToString();
			};
		}
	}

	public void RaiseGridObjectChanged(int x, int y) {
		OnGridObjectChanged?.Invoke(this, x, y);
	}

	public void SetCellGridObject(int x, int y, T value) {
		if (x <= 0 || y <= 0 || x > Width || y > Height) return; // Ignore invalid values

		_grid[x, y] = value;
		_debugTextGrid[x, y].text = _grid[x, y].ToString();
	}

	public void SetCellGridObject(Vector3 worldPosition, T value) {
		var cellPosition = WorldToCellPosition(worldPosition);
		SetCellGridObject(cellPosition.x, cellPosition.y, value);
	}

	public T GetCellGridObject(int x, int y) {
		if (x < 0 || y < 0 || x > Width || y > Height) return default; // Ignore invalid values
		return _grid[x, y];
	}

	public T GetCellGridObject(Vector3 worldPosition) {
		var cellPosition = WorldToCellPosition(worldPosition);
		return _grid[cellPosition.x, cellPosition.y];
	}

	private Vector2Int WorldToCellPosition(Vector3 worldPosition) {
		int x = Convert.ToInt32(floor((worldPosition - OriginPosition).x / CellSize));
		int y = Convert.ToInt32(floor((worldPosition - OriginPosition).y / CellSize));

		return new Vector2Int(x, y);
	}

	private Vector3 GetWorldPosition(int x, int y) {
		return new Vector3(x, y) * CellSize + OriginPosition;
	}

}
