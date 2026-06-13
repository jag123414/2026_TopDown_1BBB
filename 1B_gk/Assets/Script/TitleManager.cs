using UnityEngine;
using UnityEngine.UI; // 💡 버튼 텍스트를 바꾸기 위해 필요합니다.


public class TitleManager : MonoBehaviour
{
    public Text hardModeText; // 화면에 "하드모드: OFF" 등을 표시할 텍스트 칸
    private int isHardMode = 0; // 0이면 일반, 1이면 하드모드

    void Start()
    {
        // 💾 게임이 켜질 때 이전에 유저가 설정했던 하드모드 기억을 불러옵니다.
        isHardMode = PlayerPrefs.GetInt("HARD_MODE", 0);
        UpdateHardModeUI();
    }

    public void GameStartButton()
    {
        GameManager.Instance.StartGame();
    }

    // ⭐⭐⭐ [추가] 하드모드 버튼을 누르면 실행될 함수 ⭐⭐⭐
    public void ToggleHardMode()
    {
        if (isHardMode == 0) isHardMode = 1;  // 일반 -> 하드
        else isHardMode = 0;                 // 하드 -> 일반

        // 💾 변경된 하드모드 설정을 기기에 즉시 저장! (과제 조건 만족)
        PlayerPrefs.SetInt("HARD_MODE", isHardMode);
        PlayerPrefs.Save();

        UpdateHardModeUI();
    }

    void UpdateHardModeUI()
    {
        if (hardModeText == null) return;

        if (isHardMode == 1) hardModeText.text = "HARD MODE: ON";
        else hardModeText.text = "HARD MODE: OFF";
    }

    // ⭐⭐⭐ [최종 완료 추가] 게임을 완전히 종료하고 나가는 함수 ⭐⭐⭐
    public void QuitGame()
    {
        Debug.Log("게임 종료 버튼 클릭됨!");

        #if UNITY_EDITOR
        // 유니티 에디터 창에서 테스트 중일 때 작동하는 종료 코드
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // 실제 게임을 빌드(.exe 설치 파일 등)해서 켰을 때 컴퓨터에서 작동하는 종료 코드
        Application.Quit();
        #endif
    }

}
