using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;    // 소환할 몬스터 프리팹
    public Transform[] spawnPoints;   // 몬스터가 태어날 위치들 (여러 군데 지정 가능)

    private float spawnTimer = 0f;

    void Update()
    {
        // 💡 [핵심 연동] 우리가 만든 GameDataManager 내부의 ScriptableObject 데이터를 실시간으로 가져옵니다!
        if (GameDataManager.Instance == null || GameDataManager.Instance.gameSettingData == null) return;

        float interval = GameDataManager.Instance.gameSettingData.spawnInterval;
        int maxCount = GameDataManager.Instance.gameSettingData.maxEnemyCount;

        // ⭐⭐⭐ [하드 모드 PlayerPrefs 연동 추가] ⭐⭐⭐
        // 기기에 저장된 하드 모드 정보(0 또는 1)를 읽어옵니다.
        int isHardMode = PlayerPrefs.GetInt("HARD_MODE", 0);
        if (isHardMode == 1)
        {
            // 하드 모드가 켜져 있다면 스폰 간격을 절반(0.5)으로 줄여서 몬스터가 2배 빠르게 쏟아지게 합니다.
            interval *= 0.5f;
        }

        // 현재 맵에 존재하는 적들의 숫자를 셉니다. (태그가 Enemy인 오브젝트 개수)
        int currentEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        // 최대 마리 수보다 적을 때만 시간 타이머를 돌려 소환합니다.
        if (currentEnemyCount < maxCount)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= interval)
            {
                spawnTimer = 0f;
                SpawnEnemy();
            }
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null || spawnPoints == null || spawnPoints.Length == 0) return;

        // 여러 개의 스폰 포인트 중 무작위(랜덤)로 한 곳을 골라 몬스터를 소환합니다.
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        Debug.Log("ScriptableObject 설정에 의해 몬스터 소환 완료!");
    }
}
