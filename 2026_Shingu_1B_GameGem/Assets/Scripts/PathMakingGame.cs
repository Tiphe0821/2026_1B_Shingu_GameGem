using UnityEngine;

public class PathMakingGame : MonoBehaviour         // 아에 새로 만들까?
{
    public int gridWidth = 4;
    public int gridHeight = 4;          //  기본 게임 크기 : 4 x 4
    public float CellSize = 0.5f;       //  셀 크기
    public GameObject PathPrefabs;      //  셀 (지나갈 수 있는 길)
    public GameObject WallCellPrefabs;  //  셀 (지나갈 수 없는 길)

    public GridCell[,] grid;            // 셀들을 저장하는 2차원 배열

    // 계급장을 바꿔가면서 표시해야 했기에 필요했던 코드는 필요 없기 때문에 필요한 부분만 사용한다

    [Header("블럭 생성 좌표")]
    [Header("통상 : 0~3, 0~3")]
    [Header("왼쪽, 아래부터 시작")]

    public bool isMiniGame = false;     // 켜주기 전까지는 거짓. 입력을 못받게 해야한다
    
    private void InitializedGrid()
    {
        grid = new GridCell[gridWidth, gridHeight];
        for(int x = 0; x < gridWidth; x++)
        {
            for(int y = 0; y < gridHeight; y++)
            {                                               
                Vector3 position = new Vector3( 
                    x * CellSize - (gridWidth * CellSize / 2) + CellSize / 2 + this.transform.position.x,
                    y * CellSize - (gridWidth * CellSize / 2) + CellSize / 2 + this.transform.position.y,
                    1f
                );

                GameObject cellObj = Instantiate(WallCellPrefabs, position, Quaternion.identity, this.transform);
                GridCell cell = cellObj.GetComponent<GridCell>();
                cell.Initialize(x, y);
            }
        }
    }
    
    void Start()
    {
        InitializedGrid();
    }

    public GridCell FindClosestCell(Vector3 position)
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (grid[x, y].ContainsPosition(position))
                {
                    return grid[x, y];
                }
            }
        }

        GridCell closestCell = null;            // 없다면 가장 가까운 칸 찾기
        float closestDistance = float.MaxValue;

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                float distance = Vector3.Distance(position, grid[x, y].transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestCell = grid[x, y];
                }
            }
        }

        if (closestDistance > CellSize * 2)
        {
            return null;
        }
        return closestCell;
    }
}
