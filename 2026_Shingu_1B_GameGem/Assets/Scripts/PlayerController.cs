using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;     

    private Vector2 moveInput;
    public float moveSpeed;     // 나중에 SO 사용으로 조절 가능하도록 할 예정

    public bool isCanMove;      // 움직일 수 있는가?
    public bool isMiniGame;     // 미니게임 진행 중인지 확인

    [Header ("애니메이션 관련 데이터")]
    public int side = 4;            // 캐릭터가 보는 방향 (4가 기본인 이유 : 캐릭터의 얼굴이 온전히 보이는 앞면이기 때문)

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();

        if(moveInput.x >0)
        {
            side = 1;
        }
        else if(moveInput.x < 0)
        {
            side = 3;
        }

        if (moveInput.y < 0)
        {
            side = 4;
        }
        else if (moveInput.y > 0)
        {
            side = 2;
        }
    }

    public int GetSide()
    {
        return side;
    }

    public float GetSpeed()
    {
        float currentSpeed = rb.linearVelocity.magnitude;

        return currentSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(moveInput.normalized.x * moveSpeed * Time.deltaTime, moveInput.normalized.y * moveSpeed * Time.deltaTime, 1.0f);
    }
}
