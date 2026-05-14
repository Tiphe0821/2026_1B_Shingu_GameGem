using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator myAnim;

    private Vector2 moveInput;
    public float moveSpeed = 2;     // 나중에 SO 사용으로 조절 가능하도록 할 예정

    public bool isMiniGame = false;     // 미니게임 진행 중인지 확인 
    public bool miniCanMove = true;      // 미니게임 진행 중 움직일 수 있는가?
    private Vector2 miniMoveInput;

    [Header ("애니메이션 관련 데이터")]
    public int side = 4;            // 캐릭터가 보는 방향 (4가 기본인 이유 : 캐릭터의 얼굴이 온전히 보이는 앞면이기 때문)
    public bool isMoving = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
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

        myAnim.SetInteger("Side", side);
    }

    public bool MiniCanMove
    {
        get { return miniCanMove; }
        set { miniCanMove = value; }
    }

    public bool IsMiniGame
    {
        get { return isMiniGame; }
        set { isMiniGame = value; }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("IceWall"))
        {
            Vector2 tempVector = collision.gameObject.transform.position;
            // 벡터 계산
            Vector2 oppDir = new Vector2(transform.position.x - tempVector.x, transform.position.y - tempVector.y);
            transform.Translate(oppDir.normalized.x * 0.07f, oppDir.normalized.y * 0.07f, 0f);

            miniCanMove = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMiniGame)
        {
            transform.Translate(moveInput.normalized.x * moveSpeed * Time.deltaTime, moveInput.normalized.y * moveSpeed * Time.deltaTime, 0f);
        }
        else
        {
            if(MiniCanMove)
            {
                if(moveInput.x != 0)
                {
                    miniMoveInput.x = moveInput.x;
                    miniMoveInput.y = 0f;
                    miniCanMove = false;
                }
                else if(moveInput.y != 0)
                {
                    miniMoveInput.y = moveInput.y;
                    miniMoveInput.x = 0f; 
                    miniCanMove = false;
                }
            }
            else
            {
                transform.Translate(miniMoveInput.normalized.x * moveSpeed * 1.2f * Time.deltaTime, miniMoveInput.normalized.y * moveSpeed * 1.2f * Time.deltaTime, 0f);
            }
        }
        // rb.linearVelocity = new Vector2(moveInput.normalized.x * moveSpeed, moveInput.normalized.y * moveSpeed);

        if (moveInput.magnitude > 0.1)
            myAnim.SetBool("IsMoving", true);
        else
            myAnim.SetBool("IsMoving", false);
    }
}
