using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    public Animator myAnim;
    private PlayerController myController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        myAnim = GetComponent<Animator>();
        myController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        myAnim.SetInteger("Side", myController.GetSide());
        myAnim.SetFloat("Speed", myController.GetSpeed());
    }
}
