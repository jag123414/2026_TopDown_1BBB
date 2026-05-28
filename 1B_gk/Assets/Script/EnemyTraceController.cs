using UnityEngine;

public class EnemyTraceController : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    // raycastDistance 필드는 기존 코드 유지용으로 남겨둡니다. (사용 안 함)
    public float raycastDistance = 0.6f;

    private Transform player;

    private void Start()
    {
        // 1. 하이어라키의 "player" 오브젝트를 찾아옵니다.
        GameObject playerObj = GameObject.Find("player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    private void Update()
    {
        if (player == null) return;

        // 플레이어와의 방향 계산
        Vector2 direction = player.position - transform.position;
        Vector2 directionNormalized = direction.normalized;

        // [변경] 벽을 통과하고 플레이어에게 직진하도록 레이캐스트 조건문을 제거했습니다.
        // 디버그용 선은 그대로 출력됩니다.
        Debug.DrawRay(transform.position, directionNormalized * raycastDistance, Color.red);

        // 장애물 판단 없이 무조건 플레이어 방향으로 직진 (접촉 가능)
        transform.Translate(directionNormalized * moveSpeed * Time.deltaTime);
    }
}
