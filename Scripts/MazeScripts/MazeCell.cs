using UnityEngine;
using UnityEngine.AI;

public class MazeCell : MonoBehaviour
{
    [SerializeField] private GameObject _leftWall;
    [SerializeField] private GameObject _rightWall;
    [SerializeField] private GameObject _frontWall;
    [SerializeField] private GameObject _backWall;
    [SerializeField] private GameObject _unvisitedBlock;

    public bool IsVisited { get; private set; }

    public void Visit()
    {
        IsVisited = true;
        _unvisitedBlock.SetActive(false);
    }

    public void ClearLeftWall()
    {
        DisableWall(_leftWall);
    }

    public void ClearRightWall()
    {
        DisableWall(_rightWall);
    }

    public void ClearFrontWall()
    {
        DisableWall(_frontWall);
    }

    public void ClearBackWall()
    {
        DisableWall(_backWall);
    }

    private void DisableWall(GameObject wall)
    {
        if (wall == null) return;

        // Disable the wall visually
        wall.SetActive(false);

        // Disable the NavMeshObstacle if it exists
        NavMeshObstacle obstacle = wall.GetComponent<NavMeshObstacle>();
        if (obstacle != null)
        {
            obstacle.enabled = false;
        }
    }

    public void ClearAllWalls()
    {
        if (_backWall != null) _backWall.SetActive(false);
        if (_frontWall != null) _frontWall.SetActive(false);
        if (_leftWall != null) _leftWall.SetActive(false);
        if (_rightWall != null) _rightWall.SetActive(false);
    }

}
