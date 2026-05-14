using UnityEngine;

public class StarTrigger : MonoBehaviour
{
    public IceGame icegame;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            icegame.AddStar();
            Destroy(this.gameObject);
        }
    }
}
