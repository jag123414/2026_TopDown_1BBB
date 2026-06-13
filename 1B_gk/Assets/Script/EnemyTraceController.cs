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
        GameObject playerObj = GameObject.Find("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    private void Update()
    {
        if (player == null) return;

        // 멈춤 상태라면 아래의 이동 로직을 실행하지 않고 리턴합니다.
        if (isStopped) return;

        // 플레이어와의 방향 계산
        Vector2 direction = player.position - transform.position;
        Vector2 directionNormalized = direction.normalized;

        // 디버그용 선 출력
        Debug.DrawRay(transform.position, directionNormalized * raycastDistance, Color.red);

        // 플레이어 방향으로 추적 이동
        transform.Translate(directionNormalized * moveSpeed * Time.deltaTime);
    }

    // 물리적인 충돌(Collider2D가 Is Trigger Off 일 때)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(StopRoutine());
        }
    }

    // 트리거 충돌(Collider2D가 Is Trigger On 일 때)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
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

    void Die()
    {
        // ⭐⭐⭐ [추가] 죽으면서 데이터 매니저에 골드와 처치 수 누적 명령 ⭐⭐⭐
        GameDataManager.Instance.AddKillCount();   // 처치 수 +1
        GameDataManager.Instance.AddGold(100);     // 한 마리당 100골드 지급 (원하는 액수로 변경 가능)

        Destroy(gameObject); // 기존에 있던 몬스터 파괴 코드
    }


    // 🛠️ 몬스터 스크립트 내부에 추가할 코드
    public int enemyHP = 1; // 몬스터 체력

    public void TakeDamage(int amount)
    {
        enemyHP -= amount;
        if (enemyHP <= 0)
        {
            Die();
        }
    }
}
