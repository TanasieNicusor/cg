using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject _speedBoostPrefab;
    public int _speedBoostCount = 10;

    [SerializeField]
    private GameObject _birdsEyeBoostPrefab;
    public int _birdsEyeBoostCount = 10;

    [SerializeField]
    private GameObject _stunAIBoostPrefab;
    public int _stunAIBoostCount = 10;

    [SerializeField]
    private GameObject _breakWallsPrefab;
    public int _breakWallsCount = 10;

    [SerializeField]
    private MazeCell _mazeCellPrefab;

    [SerializeField]
    private int _mazeWidth;

    [SerializeField]
    private int _mazeDepth;                                            

    private MazeCell[,] _mazeGrid;

    void Start()
    {
        _mazeGrid = new MazeCell[_mazeWidth, _mazeDepth];
        for(int i = 0; i < _mazeWidth; i++)
            for (int j = 0; j < _mazeDepth; j++)
                    _mazeGrid[i, j] = Instantiate(_mazeCellPrefab, new Vector3(i, 0, j), Quaternion.identity);

        int entryY = _mazeDepth / 2;
        int exitY = _mazeDepth / 2;

        MazeCell entryCell = _mazeGrid[0, entryY];
        MazeCell exitCell = _mazeGrid[_mazeWidth - 1, exitY];

        entryCell.ClearLeftWall();  
        exitCell.ClearRightWall();  


        GenerateMaze(null, _mazeGrid[0, 0]);
        PlaceSpeedBoosts();
        PlaceBirdsEye();
        PlaceStunAI();
        PlaceBreakWalls();
    }

    private void GenerateMaze(MazeCell previousCell, MazeCell currentCell)
    {
        currentCell.Visit();
        ClearWalls(previousCell, currentCell);
        MazeCell nextCell;
        do
        {
            nextCell = GetNextUnvisitedCell(currentCell);
            if (nextCell != null)
                GenerateMaze(currentCell, nextCell);
        } while(nextCell != null );
    }

    private MazeCell GetNextUnvisitedCell(MazeCell currentCell)
    {
        var unvisitedCells = GetUnvisitedCells(currentCell);
        return unvisitedCells.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();
    }

    private IEnumerable<MazeCell> GetUnvisitedCells(MazeCell currentCell)
    {
        int x = (int)currentCell.transform.position.x;
        int z = (int)currentCell.transform.position.z;
        if (x + 1 < _mazeWidth)
        {
            var cellToRight = _mazeGrid[x + 1, z];
            if (cellToRight.IsVisited == false)
                yield return cellToRight;
        }

        if (x - 1 >= 0)
        {
            var cellToLeft = _mazeGrid[x - 1, z];
            if (cellToLeft.IsVisited == false)
                yield return cellToLeft;
        }

        if (z + 1 < _mazeDepth)
        {
            var cellToFront = _mazeGrid[x, z + 1];
            if (cellToFront.IsVisited == false)
                yield return cellToFront;
        }

        if (z - 1 >= 0)
        {
            var cellToBack = _mazeGrid[x, z - 1];
            if (cellToBack.IsVisited == false)
                yield return cellToBack;
        }
    }


    private void ClearWalls(MazeCell previousCell, MazeCell currentCell)
    {
        if (previousCell == null)
            return;
        if(previousCell.transform.position.x < currentCell.transform.position.x)
        {
            previousCell.ClearRightWall();
            currentCell.ClearLeftWall();
            return;
        }
        if (previousCell.transform.position.x > currentCell.transform.position.x)
        {
            previousCell.ClearLeftWall();
            currentCell.ClearRightWall();
            return;
        }
        if (previousCell.transform.position.z < currentCell.transform.position.z)
        {
            previousCell.ClearFrontWall();
            currentCell.ClearBackWall();
            return;
        }
        if (previousCell.transform.position.z > currentCell.transform.position.z)
        {
            previousCell.ClearBackWall();
            currentCell.ClearFrontWall();
            return;
        }
    }

    private void PlaceSpeedBoosts()
    {
        List<MazeCell> allCells = new List<MazeCell>();


        foreach (var cell in _mazeGrid)
        {
            allCells.Add(cell);
        }


        allCells = allCells.OrderBy(_ => Random.value).ToList();

        int placed = 0;

        foreach (var cell in allCells)
        {
            if (placed >= _speedBoostCount)
                break;


            Vector3 pos = cell.transform.position;
            if ((int)pos.x == 0 || (int)pos.x == _mazeWidth - 1)
                continue;


            Vector3 spawnPos = pos + Vector3.up * 0.4f;

            Instantiate(_speedBoostPrefab, spawnPos, Quaternion.identity);
            placed++;
        }
    }

    private void PlaceBirdsEye()
    {
        List<MazeCell> allCells = new List<MazeCell>();


        foreach (var cell in _mazeGrid)
        {
            allCells.Add(cell);
        }


        allCells = allCells.OrderBy(_ => Random.value).ToList();

        int placed = 0;

        foreach (var cell in allCells)
        {
            if (placed >= _birdsEyeBoostCount)
                break;


            Vector3 pos = cell.transform.position;
            if ((int)pos.x == 0 || (int)pos.x == _mazeWidth - 1)
                continue;


            Vector3 spawnPos = pos + Vector3.up * 0.6f;

            Instantiate(_birdsEyeBoostPrefab, spawnPos, Quaternion.identity);
            placed++;
        }
    }

    private void PlaceStunAI()
    {
        List<MazeCell> allCells = new List<MazeCell>();


        foreach (var cell in _mazeGrid)
        {
            allCells.Add(cell);
        }


        allCells = allCells.OrderBy(_ => Random.value).ToList();

        int placed = 0;

        foreach (var cell in allCells)
        {
            if (placed >= _stunAIBoostCount)
                break;


            Vector3 pos = cell.transform.position;
            if ((int)pos.x == 0 || (int)pos.x == _mazeWidth - 1)
                continue;


            Vector3 spawnPos = pos + Vector3.up * 0.2f;

            Instantiate(_stunAIBoostPrefab, spawnPos, Quaternion.identity);
            placed++;
        }
    }

    private void PlaceBreakWalls()
    {
        List<MazeCell> allCells = new List<MazeCell>();


        foreach (var cell in _mazeGrid)
        {
            allCells.Add(cell);
        }


        allCells = allCells.OrderBy(_ => Random.value).ToList();

        int placed = 0;

        foreach (var cell in allCells)
        {
            if (placed >= _breakWallsCount)
                break;


            Vector3 pos = cell.transform.position;
            if ((int)pos.x == 0 || (int)pos.x == _mazeWidth - 1)
                continue;


            Vector3 spawnPos = pos + Vector3.up * 0.8f;

            Instantiate(_breakWallsPrefab, spawnPos, Quaternion.identity);
            placed++;
        }
    }


}
