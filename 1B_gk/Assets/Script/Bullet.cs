using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;

    void Start()
    {
        // 화살이 발사되면 3초 뒤에 자동으로 메모리에서 삭제됩니다.
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        // 앞으로 직선 이동
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 몬스터와 부딪히면 
        if (collision.CompareTag("Enemy"))
        {
            // 몬스터 스크립트를 가져와서 대미지를 줍니다.
            EnemyTraceController enemy = collision.GetComponent<EnemyTraceController>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject); // 화살 자신은 파괴
        }
    }
}
