using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections; // 💡 코루틴(IEnumerator) 사용을 위해 추가되었습니다.

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public Sprite[] spriteUp;
    public Sprite[] spriteDown;
    public Sprite[] spriteLeft;
    public Sprite[] spriteRight;
    public float frameTime = 0.15f;
    public int maxLives = 3;     // 최대 목숨

    // ⭐⭐⭐ [데이터 매니저 연동 변수] ⭐⭐⭐
    public int playerHP = 0;
    public int playerAttack = 0;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Vector2 input;
    private Vector2 velocity;
    private Sprite[] currentSprites;
    private int frameIndex = 0;
    private float timer = 0f;
    private int currentLives;    // 현재 목숨

    // 💡 [추가] 무적 상태를 제어하기 위한 변수들입니다.
    public float invincibleTime = 1.0f; // 무적 시간 (1초)
    private bool isInvincible = false;  // 현재 무적 상태인지 여부

    // ==========================================
    // 💡 [연동 추가] 하트 UI를 깎기 위해 LifeManager를 받아옵니다.
    // ==========================================
    public LifeManager lifeManager;

    void Start()
    {
        currentLives = maxLives; // 게임 시작 시 목숨 3개 부여

        // ⭐⭐⭐ [데이터 매니저에서 튜토리얼 정보 가져오기 및 체크] ⭐⭐⭐
        if (GameDataManager.Instance.isTutorialFinished == 0)
        {
            // 튜토리얼 안 했을 경우 튜토리얼 오픈
            Debug.Log("튜토리얼 오픈!");
            GameDataManager.Instance.isTutorialFinished = 1;
        }
        else
        {
            // 튜토리얼 했을 경우 아무것도 안 함
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        currentSprites = spriteDown;

        // 🛠️ [에러 수정 완료] 배열인 currentSprites를 직접 대입하지 않고, 0번째 칸의 Sprite를 명확히 지정했습니다.
        sr.sprite = currentSprites[0];

        // ⭐⭐⭐ [데이터 매니저에서 세이브 데이터 안전하게 불러오기] ⭐⭐⭐
        moveSpeed = GameDataManager.Instance.GetPlayerMoveSpeed();
        playerHP = GameDataManager.Instance.GetPlayerHp();
        playerAttack = GameDataManager.Instance.GetPlayerAttack();
    }

    public void OnMove(InputValue value)
    {
        input = value.Get<Vector2>();
        velocity = input.normalized * moveSpeed;

        if (input.sqrMagnitude > 0.01f)
        {
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            {
                if (input.x > 0)
                    ChangeSprites(spriteRight);
                else
                    ChangeSprites(spriteLeft);
            }
            else
            {
                if (input.y > 0)
                    ChangeSprites(spriteUp);
                else
                    ChangeSprites(spriteDown);
            }
        }
    }

    private void Update()
    {
        if (input.sqrMagnitude <= 0.01f)
        {
            frameIndex = 0;
            sr.sprite = currentSprites[frameIndex];
            return;
        }

        timer += Time.deltaTime;

        if (timer >= frameTime)
        {
            timer = 0f;
            frameIndex++;

            if (frameIndex >= currentSprites.Length)
                frameIndex = 0;

            sr.sprite = currentSprites[frameIndex];
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    private void ChangeSprites(Sprite[] newSprites)
    {
        if (currentSprites == newSprites)
            return;

        currentSprites = newSprites;
        frameIndex = 0;
        timer = 0f;
        sr.sprite = currentSprites[frameIndex];
    }

    // 💡 [변경] 기존 코드를 지우지 않고, 무적 상태(!isInvincible)일 때만 데미지가 들어가도록 변경했습니다.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isInvincible)
        {
            TakeDamage();
        }
    }

    // 💡 [변경] 기존 코드를 지우지 않고, 무적 상태(!isInvincible)일 때만 데미지가 들어가도록 변경했습니다.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !isInvincible)
        {
            TakeDamage();
        }
    }

    // 💡 [추가] 몬스터와 계속 겹쳐있는 상태(Stay)에서도 무적이 풀리면 바로 데미지를 받도록 Stay 함수를 추가했습니다.
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isInvincible)
        {
            TakeDamage();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !isInvincible)
        {
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        currentLives--;
        Debug.Log("남은 목숨: " + currentLives);

        // ==========================================
        // 💡 [연동 추가] 목숨이 깎일 때 하트 UI도 함께 한 개 숨깁니다.
        // ==========================================
        if (lifeManager != null)
        {
            lifeManager.TakeDamage();
        }

        if (currentLives <= 0)
        {
            // ⭐⭐⭐ [게임 오버 연동 부분 수정] ⭐⭐⭐
            // 목숨이 0개가 되면 슬라이드 내용대로 싱글톤 GameManager를 깨워 게임 오버를 처리합니다.
            Debug.Log("게임 오버! GameManager를 호출하여 데이터를 저장하고 타이틀로 이동합니다.");
            GameManager.Instance.GameOver();
        }
        else
        {
            // 💡 [추가] 목숨이 남아있다면 1초 무적 및 깜빡임 코루틴을 시작합니다.
            StartCoroutine(InvincibleRoutine());
        }
    }

    // 💡 [추가] 1초 동안 무적을 유지하며 캐릭터를 깜빡이게 만드는 함수입니다.
    IEnumerator InvincibleRoutine()
    {
        isInvincible = true; // 무적 시작

        float timer = 0f;
        while (timer < invincibleTime)
        {
            // 캐릭터 투명도를 0.3으로 낮춰 흐릿하게 만듭니다.
            Color color = sr.color;
            color.a = 0.3f;
            sr.color = color;
            yield return new WaitForSeconds(0.1f);

            // 캐릭터 투명도를 1.0으로 되돌려 원래대로 만듭니다.
            color.a = 1.0f;
            sr.color = color;
            yield return new WaitForSeconds(0.1f);

            timer += 0.2f; // 대기한 시간 누적 (0.1초 + 0.16초)
        }

        // 코루틴이 완전히 끝날 때 혹시 투명한 상태로 남지 않도록 알파값 원상복구
        Color finalColor = sr.color;
        finalColor.a = 1.0f;
        sr.color = finalColor;

        isInvincible = false; // 무적 종료
    }
}
