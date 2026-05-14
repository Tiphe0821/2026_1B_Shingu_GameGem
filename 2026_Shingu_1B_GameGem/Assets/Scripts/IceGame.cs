using UnityEngine;

public class IceGame : MonoBehaviour
{

    public PlayerController player;
    public Transform StartPosition;
    public Transform originPosition = null;

    private int maxStars;
    private int starCount = 0;
    public bool finished = false;

    [Header("끝나면 나올 지점 추가")]
    public Transform finishPosition = null;

    [Header("스테이지 별 개수 데이터 끌어다 놓기")]
    [SerializeField] IceGameSO data;

    private void Awake()
    {
        maxStars = data.stars;
    }

    public void StartIceGame()
    {
        finished = false;
        player.MiniCanMove = false;
        player.IsMiniGame = true;
        originPosition = player.transform;
        player.gameObject.transform.position = StartPosition.position;
        player.MiniCanMove = true;
    }

    public void AddStar()
    {
        starCount++;

        CheckStars();
    }

    private void CheckStars()
    {
        if(starCount == maxStars)
            finished = true;
    }

    private void Update()
    {

        if (finished)
        {
            player.IsMiniGame = false;
            if (finishPosition == null)     // 만약 끝 지점이 설정되어 있지 않다면
                player.transform.position = originPosition.position;
            else
                player.transform.position = finishPosition.position;
            this.gameObject.SetActive(false);
        }
    }
}
