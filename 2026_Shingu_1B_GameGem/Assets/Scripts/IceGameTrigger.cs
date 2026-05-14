using UnityEngine;

public class IceGameTrigger : MonoBehaviour
{
    [Header("시작하기를 원하는 아이스 게임을 넣기")]
    public IceGame game;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(game.gameObject.activeSelf)      // 게임이 활성화 되어있는 경우
            {
                game.StartIceGame();
            }
        }
    }
}
