using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public void GameStartButton()
    {
        // 방금 만든 GameManager 싱글톤을 원격 호출해 게임을 시작합니다.
        GameManager.Instance.StartGame();
    }
}