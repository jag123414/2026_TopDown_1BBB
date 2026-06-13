using UnityEngine;
using UnityEngine.UI; // 💡 UI 텍스트를 제어하기 위해 반드시 필요한 네임스페이스입니다.

public class Gold : MonoBehaviour
{
    // 유니티 인스펙터 창에서 연결할 텍스트 컴포넌트 변수
    public Text goldText;

    void Update()
    {
        if (goldText != null && GameDataManager.Instance != null)
        {
            // 데이터 매니저에 저장된 totalGold 장부 수치를 가져와 화면에 실시간으로 그려줍니다.
            goldText.text = "GOLD: " + GameDataManager.Instance.saveData.totalGold.ToString();
        }
    }
}
