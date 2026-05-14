using UnityEngine;

[CreateAssetMenu(fileName = "NewIceGame", menuName = "Game/IceGameSetting")]
public class IceGameSO : ScriptableObject
{
    [Header("얼음 미니게임 스테이지 설정 : 별 개수 설정")]

    public int stars; // 먹어야하는 별의 수


    // 굳이 써야할까 싶은 느낌이 든다. 하지만 쓰는게 좋을 것 같다. 스테이지 분류도 겸하게 하면 어떨까? 잘 모르겠다

}
