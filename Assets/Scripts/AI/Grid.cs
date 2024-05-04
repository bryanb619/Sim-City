using System;
using System.Collections.Generic;
public class Grid 
{
    private CellType[,] _grid;

    public int Width { get; private set; }

    public int Height { get; private set; }

    private List<Point> _roadList = new List<Point>();
    private List<Point> _specialStructure = new List<Point>();

    public Grid (int width , int height)
    {
        this.Width = width;
        this.Height = height;

        _grid = new CellType[width, height];
    }


    public static bool IsCellWakable(CellType cellType, bool aiAgent = false)
    {
        if (aiAgent)
        {
            return cellType == CellType.Road;
        }
        return cellType == CellType.Empty || cellType == CellType.Road;
    }

     public Point GetRandomRoadPoint()
    {
        Random rand = new Random();
        return _roadList[rand.Next(0, _roadList.Count - 1)];
    }

       public Point GetRandomSpecialStructurePoint()
    {
        Random rand = new Random();
        return _roadList[rand.Next(0, _roadList.Count - 1)];
    }

    public List<Point> GetAdjacentCells(Point cell, bool isAgent)
    {
        return GetWakableAdjacentCells((int)cell.X, (int)cell.Y, isAgent);
    }

    public float GetCostOfEnteringCell(Point cell)
    {
        return 1;
    }

    public List<Point> GetAllAdjacentCells(int x, int y)
    {
        List<Point> adjacentCells = new List<Point>();
        if (x > 0)
        {
            adjacentCells.Add(new Point(x - 1, y));
        }
        if (x < Width - 1)
        {
            adjacentCells.Add(new Point(x + 1, y));
        }
        if (y > 0)
        {
            adjacentCells.Add(new Point(x, y - 1));
        }
        if (y < Height - 1)
        {
            adjacentCells.Add(new Point(x, y + 1));
        }
        return adjacentCells;
    }

    public List<Point> GetWakableAdjacentCells(int x, int y, bool isAgent)
    {
        List<Point> adjacentCells = GetAllAdjacentCells(x, y);
        for (int i = adjacentCells.Count - 1; i >= 0; i--)
        {
            if(IsCellWakable(_grid[adjacentCells[i].X, adjacentCells[i].Y], isAgent)==false)
            {
                adjacentCells.RemoveAt(i);
            }
        }
        return adjacentCells;
    }

    public List<Point> GetAdjacentCellsOfType(int x, int y, CellType type)
    {
        List<Point> adjacentCells = GetAllAdjacentCells(x, y);
        for (int i = adjacentCells.Count - 1; i >= 0; i--)
        {
            if (_grid[adjacentCells[i].X, adjacentCells[i].Y] != type)
            {
                adjacentCells.RemoveAt(i);
            }
        }
        return adjacentCells;
    }

    /// <summary>
    /// Returns array [Left neighbour, Top neighbour, Right neighbour, Down neighbour]
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public CellType[] GetAllAdjacentCellTypes(int x, int y)
    {
        CellType[] neighbours = { CellType.None, CellType.None, CellType.None, 
        CellType.None };
        if (x > 0)
        {
            neighbours[0] = _grid[x - 1, y];
        }
        if (x < Width - 1)
        {
            neighbours[2] = _grid[x + 1, y];
        }
        if (y > 0)
        {
            neighbours[3] = _grid[x, y - 1];
        }
        if (y < Height - 1)
        {
            neighbours[1] = _grid[x, y + 1];
        }
        return neighbours;
    }
}
