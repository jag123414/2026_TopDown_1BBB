using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string titleSceneName = "TitleScene";
    public string gameSceneName = "GameScene";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void GameOver()
    {
        // 플레이어가 게임오버 되었을 때, 방금 만든 GameDataManager를 호출해 데이터를 저장합니다.
        GameDataManager.Instance.SaveGameResult();
        GoTitle();
    }

    public void GoTitle()
    {
        SceneManager.LoadScene(titleSceneName);
    }
}
