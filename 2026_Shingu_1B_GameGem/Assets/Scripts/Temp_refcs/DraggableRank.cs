using UnityEngine;

public class DraggableRank : MonoBehaviour
{
    public int rankLevel = 1; // 계급장레벨 (0은 빈칸)
    public float dragSpeed = 30.0f;     // 드래그 시 오브젝트 이동 속도
    public float snapBackSpeed = 30f;   // 원위치로 돌아가는 속도 설정

    public bool isDragging = false;     // 현재 드래그 중인지 확인하는 변수
    public Vector3 originalPosition;
    public GridCell currentCell;

    public Camera mainCamera;           // 메인 카메라
    public Vector3 dragOffset;          // 드래그 시 오프셋 (보정값)
    public SpriteRenderer spriteRenderer;   // 계급 이미지 렌더러

    public RankGameManager GameManager; // 게임 매니져
                                        // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        mainCamera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
        GameManager = FindAnyObjectByType<RankGameManager>();
        
    }

    void Start()
    {
        originalPosition = transform.position;
    }

    void StartDragging()
    {
        isDragging = true;
        dragOffset = transform.position - GetMouseWorldPosition();
        spriteRenderer.sortingOrder = 0;
    }

    void StopDragging()
    {
        isDragging = false;
        spriteRenderer.sortingOrder = 1;
        GridCell targetCell = GameManager.FindClosestCell(transform.position);
        
        if(targetCell != null )
        {
            if(targetCell.currentRank == null) // 빈칸인 경우 -> 이동
            {
                MoveToCell(targetCell);
            }
            else if(targetCell.currentRank != this && targetCell.currentRank.rankLevel == rankLevel)
            {
                MergeWithCell(targetCell);
            }
            else
            {
                ReturnToOriginalPosition();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isDragging)
        {
            Vector3 targetPosition = GetMouseWorldPosition() + dragOffset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, dragSpeed * Time.deltaTime);
        }
        else if( transform.position != originalPosition && currentCell != null )
        {
            transform.position = Vector3.Lerp(originalPosition, originalPosition,snapBackSpeed * Time.deltaTime);
        }
    }

    private void OnMouseDown()
    {
        StartDragging();
    }

    private void OnMouseUp()
    {
        if (!isDragging) return;
        StopDragging();
    }

    public void MoveToCell(GridCell targetCell)
    {
        if(currentCell != null)
        {
            currentCell.currentRank = null;
        }

        currentCell = targetCell; // 새로운 칸으로 이동
        targetCell.currentRank = this;

        originalPosition = new Vector3(targetCell.transform.position.x, targetCell.transform.position.y, 0);
        transform.position = originalPosition;
    }

    public void ReturnToOriginalPosition()
    {
        transform.position = originalPosition;
    }

    public void MergeWithCell(GridCell targetCell)
    {
        if(targetCell.currentRank == null || targetCell.currentRank.rankLevel != rankLevel)
        {
            ReturnToOriginalPosition();
            return;
        }

        if(currentCell != null)
        {
            currentCell.currentRank = null;     // 기존 칸에서 제거
        }

        // 합치기 실행 MergeRank 함수를 통해서 실행
        GameManager.MergeRanks(this, targetCell.currentRank);
    }

    public Vector3 GetMouseWorldPosition()          // 월드 좌표 구하기
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -mainCamera.transform.position.z;
        return mainCamera.ScreenToWorldPoint(mousePos);
    }

    public void SetRankLevel(int level)
    {
        rankLevel = level;
        Debug.Log("위쪽 들어왔어요");

        if(GameManager != null && GameManager.rankSprites.Length > level -1)
        {
            Debug.Log("들어왔어요");
            spriteRenderer.sprite = GameManager.rankSprites[level - 1];         // 레벨에 맞는 스프라이트로 변경
        }
    }
}
