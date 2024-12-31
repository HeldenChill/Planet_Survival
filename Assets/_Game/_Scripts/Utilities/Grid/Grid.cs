using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Utilities.Grid
{
    public interface IGrid { }
    public class Grid<T, TD> : IGrid where T : GridCell<TD>
    {
        public event Action<int, int, T> _OnGridChange;
        protected readonly TextMeshPro[,] debugTextArray;
        protected readonly T[,] gridArray;
        private readonly Vector3 originPosition;
        //Grid is change or not when compare to last save state
        private bool isChange = false;
        public bool IsChange => isChange;

        public Grid(int width, int height, float cellSize, Vector3 originPosition = default,
            Func<GridCell<TD>> constructorCell = null, GridPlane gridPlaneType = GridPlane.XY)
        {
            Width = width;
            Height = height;
            CellSize = cellSize;
            this.originPosition = originPosition;
            GridPlaneType = gridPlaneType;

            gridArray = new T[width, height];
            debugTextArray = new TextMeshPro[width, height];

            for (int x = 0; x < gridArray.GetLength(0); x++)
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    if (constructorCell != null) gridArray[x, y] = (T)constructorCell();
                    gridArray[x, y].SetCellPosition(x, y);
                    gridArray[x, y].Size = cellSize;
                    gridArray[x, y].GridPlaneType = gridPlaneType;
                    switch (gridPlaneType)
                    {
                        case GridPlane.XY:
                            gridArray[x, y].UpdateWorldPosition(originPosition.x, originPosition.y);
                            break;
                        case GridPlane.XZ:
                            gridArray[x, y].UpdateWorldPosition(originPosition.x, originPosition.z);
                            break;
                        case GridPlane.YZ:
                            gridArray[x, y].UpdateWorldPosition(originPosition.y, originPosition.z);
                            break;
                    }

                    gridArray[x, y].OnValueChange += OnGridCellValueChange;
                }
        }

        public float CellSize { get; }

        public int Width { get; }

        public int Height { get; }

        public GridPlane GridPlaneType { get; }

        public Vector3 GetWorldPosition(int x, int y)
        {
            return GetUnitVector3(x, y) * CellSize + originPosition;
        }

        public Vector3 GetWorldPosition(Vector3 worldPosition)
        {
            (int x, int y) = GetGridPosition(worldPosition);

            return GetUnitVector3(x, y) * CellSize + originPosition;
        }

        public (int, int) GetGridPosition(Vector3 worldPosition)
        {
            Vector3 realPos = worldPosition - originPosition;
            switch (GridPlaneType)
            {
                case GridPlane.XY:
                    return (Mathf.FloorToInt(realPos.x / CellSize), Mathf.FloorToInt(realPos.y / CellSize));
                case GridPlane.XZ:
                    return (Mathf.FloorToInt(realPos.x / CellSize), Mathf.FloorToInt(realPos.z / CellSize));
                case GridPlane.YZ:
                    return (Mathf.FloorToInt(realPos.y / CellSize), Mathf.FloorToInt(realPos.z / CellSize));
            }

            return default;
        }

        public void SetGridCell(int x, int y, T value)
        {
            if (value is null) return;
            if (x >= 0 && y >= 0 && x < Width && y < Height)
            {
                gridArray[x, y].OnValueChange -= OnGridCellValueChange;
                gridArray[x, y] = value;
                gridArray[x, y].OnValueChange += OnGridCellValueChange;
            }
        }

        public void SetGridCell(Vector3 position, T value)
        {
            int x, y;
            (x, y) = GetGridPosition(position);
            SetGridCell(x, y, value);
        }

        public T GetGridCell(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < Width && y < Height) return gridArray[x, y];
            return default;
        }

        public T GetGridCell(Vector3 worldPosition)
        {
            int x, y;
            (x, y) = GetGridPosition(worldPosition);
            if (x >= 0 && y >= 0 && x < Width && y < Height) return gridArray[x, y];
            return default;
        }

        protected virtual void OnGridCellValueChange(int x, int y, bool isRevert)
        {
            #region DEBUG
            if (debugTextArray[x, y] != null)
                debugTextArray[x, y].text = gridArray[x, y].ToString();
            _OnGridChange?.Invoke(x, y, gridArray[x, y]);
            #endregion
            if (isRevert) return;
            isChange = true;
        }

        private Vector3 GetUnitVector3(float val1, float val2)
        {
            switch (GridPlaneType)
            {
                case GridPlane.XY:
                    return new Vector3(val1, val2, 0);
                case GridPlane.XZ:
                    return new Vector3(val1, 0, val2);
                case GridPlane.YZ:
                    return new Vector3(0, val1, val2);
            }

            return default;
        }


        #region VISIT CLASS

        public abstract class PathfindingAlgorithm
        {
            protected Grid<T, TD> grid;
            public abstract List<T> FindPath(int startX, int startY, int endX, int endY, Grid<T, TD> grid);
        }

        public sealed class DebugGrid
        {
            private readonly Vector2 unwalkableUV = Vector2.zero;
            private readonly Vector2 walkableUV = new(9f / 334, 0);

            public void DrawGrid(Grid<T, TD> grid, bool detail = false)
            {
                for (int x = 0; x < grid.gridArray.GetLength(0); x++)
                    for (int y = 0; y < grid.gridArray.GetLength(1); y++)
                    {
                        if (detail)
                        {
                            string content = grid.gridArray[x, y].GetCellPosition().ToString();
                            Vector3 localPosition = grid.GetWorldPosition(x, y) + new Vector3(grid.CellSize / 5, grid.CellSize / 2 + 0.3f, grid.CellSize * 0.75f);
                            grid.debugTextArray[x, y] = GridUtilities.CreateWorldText(content, null, localPosition, 10, Color.white, TextAnchor.MiddleCenter);
                            // Rotate text base on the gridPlane
                            grid.debugTextArray[x, y].transform.rotation = grid.GridPlaneType switch
                            {
                                GridPlane.XY => Quaternion.Euler(0, 90, 0),
                                GridPlane.XZ => Quaternion.Euler(90, 0, 0),
                                GridPlane.YZ => Quaternion.Euler(0, 0, 90),
                                _ => grid.debugTextArray[x, y].transform.rotation
                            };

                        }

                        Debug.DrawLine(grid.GetWorldPosition(x, y), grid.GetWorldPosition(x, y + 1), Color.white, 100f,
                            true);
                        Debug.DrawLine(grid.GetWorldPosition(x, y), grid.GetWorldPosition(x + 1, y), Color.white, 100f,
                            true);
                    }

                Debug.DrawLine(grid.GetWorldPosition(0, grid.Height), grid.GetWorldPosition(grid.Width, grid.Height),
                    Color.white, 100f);
                Debug.DrawLine(grid.GetWorldPosition(grid.Width, 0), grid.GetWorldPosition(grid.Width, grid.Height),
                    Color.white, 100f);
            }
        }
        #endregion
    }
}
