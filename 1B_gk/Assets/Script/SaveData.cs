using System;

[System.Serializable]
public class SaveData
{
    // 여기에 저장하고 싶은 데이터들을 적어줍니다.
    public int deathCount;

    // ⭐⭐⭐ [추가] 과제 조건을 위한 누적 재화 및 스코어 칸 ⭐⭐⭐
    public int totalGold;       // 누적 골드
    public int totalKillCount;  // 누적 몬스터 처치 수
}