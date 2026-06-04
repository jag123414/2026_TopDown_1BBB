using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnemyTraceController : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public float raycastDistance = 0.6f;

    private bool isStopped = false;   // 3초간 멈춤 상태인지 체크하는 변수

    private Transform player;

    private void Start()
    {
        // 하이어라키의 "player" 오브젝트를 찾아옵니다.
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

        // 디버그용 선 출력
        Debug.DrawRay(transform.position, directionNormalized * raycastDistance, Color.red);

        // 장애물 판단 없이 무조건 플레이어 방향으로 직진
        transform.Translate(directionNormalized * moveSpeed * Time.deltaTime);

        {
            // 만약 '멈춤 상태'라면 아래의 이동 코드를 실행하지 않고 멈춥니다.
            if (isStopped) return;

            // [기존 몬스터 이동 로직 코드]
            // 예시: 오른쪽으로 이동하는 코드 (회원님의 기존 이동 코드를 유지하시면 됩니다)
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
    }

    // 몬스터가 플레이어와 부딪혔을 때 체력을 깎는 함수 (여기에 1개만 있어야 합니다)

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            StartCoroutine(StopRoutine());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            StartCoroutine(StopRoutine());
        }
    }

    // 3초 동안 이동을 정지시키는 코루틴 함수
    IEnumerator StopRoutine()
    {
        isStopped = true;
        yield return new WaitForSeconds(3.0f);
        isStopped = false;
    }

    void Update()
    {
        if (isStopped) return;
        // 이동 코드...
    } // ◀ Update 함수가 여기서 확실히 닫혀야 합니다!

}