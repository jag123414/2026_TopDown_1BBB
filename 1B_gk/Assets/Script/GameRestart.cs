using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRestart : MonoBehaviour
{
    // 1. [이어하기 버튼용] 저장된 생존시간을 불러와서 시작
    public void RestartGame()
    {
        if (PlayerPrefs.HasKey("HasSavedData"))
        {
            float savedTime = PlayerPrefs.GetFloat("SavedTime", 0f);
            Debug.Log("저장된 생존시간을 불러옵니다: " + savedTime + "초");
            SceneManager.LoadScene("SampleScene");
        }
        else
        {
            Debug.Log("저장된 데이터가 없어 처음부터 시작합니다.");
            SceneManager.LoadScene("SampleScene");
        }
    }

    // 2. [게임 시작 버튼용] ★이전 기록을 완전히 지우고 0초부터 시작★
    public void GameStartButtonAction()
    {
        Debug.Log("새 게임을 시작합니다. 이전 데이터를 삭제합니다.");

        // 중요: 컴퓨터에 남아있는 이전 생존 시간 기록을 완전히 지웁니다.
        PlayerPrefs.DeleteKey("HasSavedData");
        PlayerPrefs.DeleteKey("SavedTime");
        PlayerPrefs.Save(); // 지운 상태를 디스크에 확실히 저장합니다.

        // 0초부터 새 판 시작!
        SceneManager.LoadScene("SampleScene");
    }

    // 3. 플레이어가 죽었을 때 시간을 컴퓨터에 기록하는 기능
    public static void SaveSurvivalTime(float currentTime)
    {
        PlayerPrefs.SetFloat("SavedTime", currentTime);
        PlayerPrefs.SetInt("HasSavedData", 1);
        PlayerPrefs.Save();
        Debug.Log("생존시간 저장 완료: " + currentTime + "초");
    }
}
