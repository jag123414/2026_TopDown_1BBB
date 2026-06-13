using UnityEngine;

[CreateAssetMenu(menuName = "Game Data/Game Setting Data")]
public class GameSettingData : ScriptableObject
{
    [Header("기본 능력치 설정")]
    public int startHp = 100;
    public int startAttack = 10;
    public float playerMoveSpeed = 5f;

    [Header("사망 보너스 설정")]
    public int hpBonusPerDeath = 5;
    public int atkBonusPerDeath = 1;

    // ⭐⭐⭐ [과제 조건 3번 추가] 레벨 디자인 및 난이도 조절 변수들 ⭐⭐⭐
    [Header("--- 몬스터 스폰 난이도 설정 ---")]
    public float spawnInterval = 2.0f; // 몇 초마다 몬스터를 소환할 것인가
    public int maxEnemyCount = 10;     // 맵에 동시에 존재할 수 있는 최대 몬스터 수
}
