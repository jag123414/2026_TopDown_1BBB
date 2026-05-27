using UnityEngine;

public class EnemyTraceController : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public float raycastDistance = 0.6f; // 타일맵 크기에 맞춰 살짝 늘림

    private Transform player;

    private void Start()
    {
        // 1. 스크린샷의 하이어라키에 이름이 소문자 "player"로 되어 있으므로 이름을 기준으로 찾습니다.
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

        // 자신의 콜라이더 내부 충돌을 방지하기 위해 시작 지점을 살짝 앞으로 보정
        Vector2 rayStartPos = (Vector2)transform.position + (directionNormalized * 0.2f);

        // 2. 타일맵의 레이어가 "Default"이므로 태그 체크 없이 레이어에 걸리면 모두 장애물로 판단합니다.
        RaycastHit2D hit = Physics2D.Raycast(rayStartPos, directionNormalized, raycastDistance, LayerMask.GetMask("Default")); 
        Debug.DrawRay(rayStartPos, directionNormalized * raycastDistance, Color.red);

        // 레이어 충돌체(벽)가 감지되었을 때
        if (hit.collider != null)
        {
            // 옆으로 비켜가기 (회전된 방향으로 이동)
            Vector3 alternativeDirection = Quaternion.Euler(0f, 0f, -90f) * directionNormalized;
            transform.Translate(alternativeDirection * moveSpeed * Time.deltaTime);
        }
        else
        {
            // 장애물이 없으면 플레이어 방향으로 직진
            transform.Translate(directionNormalized * moveSpeed * Time.deltaTime);
        }
    }
}
