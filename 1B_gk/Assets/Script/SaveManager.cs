using UnityEngine;

public class SaveManager : MonoBehaviour
{
    // 데이터를 저장하는 함수입니다.
    public static void SaveGame(int stage, float bestTime, int gold)
    {
        PlayerPrefs.SetInt("SavedStage", stage);       // 스테이지 번호 저장
        PlayerPrefs.SetFloat("BestTime", bestTime);    // 최고 생존 시간 저장
        PlayerPrefs.SetInt("Gold", gold);              // 골드 저장

        PlayerPrefs.Save(); // 데이터를 하드디스크에 실제로 씁니다.
        Debug.Log("게임 데이터가 성공적으로 저장되었습니다!");
    }

    // 저장된 스테이지 번호를 가져오는 함수입니다.
    public static int GetSavedStage()
    {
        // 저장된 게 없다면 기본값으로 1스테이지를 반환합니다.
        return PlayerPrefs.GetInt("SavedStage", 1);
    }

    // 저장된 최고 생존 시간을 가져오는 함수입니다.
    public static float GetBestTime()
    {
        return PlayerPrefs.GetFloat("BestTime", 0f);
    }

    // 모든 저장 데이터를 초기화하는 함수입니다. (처음부터 하기 버튼 등에 사용)
    public static void ResetData()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("모든 저장 데이터가 삭제되었습니다.");
    }
}
