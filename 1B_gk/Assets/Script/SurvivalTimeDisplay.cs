using UnityEngine;
using UnityEngine.UI; // 일반 유니티 텍스트(UI)를 제어하기 위해 반드시 필요합니다.

public class SurvivalTimeDisplay : MonoBehaviour
{
    // 화면에 보여줄 일반 UI Text를 연결할 변수입니다.
    public Text timeText;

    // 생존 시간을 누적해서 저장할 변수입니다. (초 단위)
    private float survivalTime = 0f;

    // 게임 씬이 시작될 때 무조건 시간을 0으로 초기화합니다.
    void Start()
    {
        survivalTime = 0f;

        // (선택 사항) 만약 이전에 저장된 데이터가 남아있어서 방해된다면 완전히 삭제합니다.
        if (PlayerPrefs.HasKey("HasSavedData"))
        {
            PlayerPrefs.DeleteKey("HasSavedData");
            PlayerPrefs.DeleteKey("SavedTime");
            PlayerPrefs.Save();
        }
    }

    void Update()
    {
        // 1. 게임이 실행되는 동안 매 프레임마다 흐른 시간(초)을 더해줍니다.
        survivalTime += Time.deltaTime;

        // 2. 누적된 전체 초를 '분'과 '초'로 쪼갭니다.
        int minutes = (int)survivalTime / 60; // 전체 시간을 60으로 나눈 몫 = 분
        int seconds = (int)survivalTime % 60; // 60으로 나누고 남은 나머지 = 초

        // 3. 텍스트 UI에 "00:00" 형태로 글자를 만들어 넣어줍니다.
        timeText.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);
    }

    // LifeManager에서 이 값을 안전하게 꺼내 갈 수 있도록 해주는 기능입니다.
    public float GetSurvivalTime()
    {
        return survivalTime;
    }
}
