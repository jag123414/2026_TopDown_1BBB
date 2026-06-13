using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("추적할 플레이어 캐릭터 오브젝트")]
    public Transform player;

    [Header("--- 카메라가 넘어가지 못할 맵 경계선 설정 ---")]
    // 유저님이 보내주신 사막 맵 크기에 맞춘 최종 연산 좌표값입니다.
    public float minX = -17.8f;
    public float maxX = 17.8f;
    public float minY = -11.0f;
    public float maxY = 11.0f;

    void LateUpdate()
    {
        if (player == null) return;

        // 1. 플레이어의 X, Y 좌표를 실시간으로 추적합니다.
        Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z);

        // 2. 계산된 카메라 좌표가 사막 타일맵 경계선을 절대 넘어가지 못하도록 강제로 가둡니다.
        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);

        // 3. 최종적으로 제한된 안전한 좌표 영역으로 카메라를 이동시킵니다.
        transform.position = targetPosition;
    }
}
