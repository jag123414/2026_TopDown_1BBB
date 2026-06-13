using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("--- 몬스터 프리팹 설정 ---")]
    public GameObject enemyPrefab;         // 기존 일반 몬스터 프리팹
    public GameObject rareEnemyPrefab;     // ⭐ [추가] 새로 만든 희귀 몬스터 프리팹

    [Header("--- 희귀 몬스터 스폰 확률 (0 ~ 100) ---")]
    [Range(0f, 100f)]
    public float rareSpawnChance = 20f;    // ⭐ [추가] 기본 20% 확률로 설정 (인스펙터에서 조절 가능)

    [Header("--- 스폰 위치 설정 ---")]
    public Transform[] spawnPoints;   // 몬스터가 태어날 위치들

    private float spawnTimer = 0f;

    void Update()
    {
        // 💡 [핵심 연동] 우리가 만든 GameDataManager 내부의 ScriptableObject 데이터를 실시간으로 가져옵니다!
        if (GameDataManager.Instance == null || GameDataManager.Instance.gameSettingData == null) return;

        float interval = GameDataManager.Instance.gameSettingData.spawnInterval;
        int maxCount = GameDataManager.Instance.gameSettingData.maxEnemyCount;

        // [하드 모드 PlayerPrefs 연동]
        int isHardMode = PlayerPrefs.GetInt("HARD_MODE", 0);
        if (isHardMode == 1)
        {
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

        // 1. 여러 개의 스폰 포인트 중 무작위(랜덤)로 한 곳을 고릅니다.
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        // 2. ⭐ [확률 계산 로직] 0부터 100 사이의 랜덤 숫자를 하나 뽑습니다.
        float randomRoll = Random.Range(0f, 100f);
        GameObject selectedEnemyPrefab = enemyPrefab; // 기본값은 일반 몬스터

        // 예: 뽑은 숫자가 20보다 작고, 희귀 몬스터 프리팹이 등록되어 있다면?
        if (randomRoll < rareSpawnChance && rareEnemyPrefab != null)
        {
            selectedEnemyPrefab = rareEnemyPrefab; // 희귀 몬스터로 변경!
            Debug.Log("💎 희귀 몬스터가 낮은 확률을 뚫고 생성되었습니다! (확률: " + rareSpawnChance + "%)");
        }
        else
        {
            Debug.Log("💀 일반 몬스터 생성");
        }

        // 3. 최종 결정된 프리팹을 소환합니다.
        Instantiate(selectedEnemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
