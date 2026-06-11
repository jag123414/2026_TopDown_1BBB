using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// 💡 게임을 다시 시작하기 위해 필요한 기능입니다!

public class LifeManager : MonoBehaviour
{
    // 인스펙터 창에서 연결한 하트 이미지 배열입니다.
    public Image[] hearts;

    // 현재 남은 체력수
    private int currentHealth;

    void Start()
    {
        // 게임이 시작될 때 연결된 하트의 총 개수(3개)로 체력을 자동 설정합니다.
        if (hearts != null && hearts.Length > 0)
        {
            currentHealth = hearts.Length;
        }
        else
        {
            currentHealth = 3; // 기본값
        }
    }

    // 플레이어가 대미지를 입을 때 실행되는 함수입니다.
    public void TakeDamage()
    {
        // 체력이 이미 0 이하라면 더 이상 아무것도 하지 않습니다.
        if (currentHealth <= 0) return;

        // 체력을 1 감소 (예: 3 -> 2)
        currentHealth--;

        // 하트 이미지를 화면에서 끕니다.
        if (hearts != null && currentHealth >= 0 && currentHealth < hearts.Length)
        {
            if (hearts[currentHealth] != null)
            {
                hearts[currentHealth].gameObject.SetActive(false);
            }
        }

        // 💡 체력이 0이 되면 게임을 다시 시작합니다.
        if (currentHealth <= 0)
        {
            // ─────────────────────────────────────────────────────────
            // [새로 추가된 부분] 
            // SurvivalTimeDisplay 스크립트를 찾아서 흘러간 생존시간 데이터를 안전하게 저장합니다.
            SurvivalTimeDisplay timeDisplay = FindObjectOfType<SurvivalTimeDisplay>();
            if (timeDisplay != null)
            {
                // 질문자님의 실제 생존 시간(survivalTime)을 가져와 안전하게 저장합니다.
                GameRestart.SaveSurvivalTime(timeDisplay.GetSurvivalTime());
            }
            // ─────────────────────────────────────────────────────────

            Invoke("RestartGame", 0.5f); // 0.5초 뒤에 RestartGame 함수를 실행합니다.
        }
    }

    // 게임을 처음부터 다시 시작하는 함수입니다.
    void RestartGame()
    {
        // [수정된 부분] 현재 씬 대신 "Start screen" 씬을 불러오도록 변경했습니다!
        SceneManager.LoadScene("Start screen");
    }
}
