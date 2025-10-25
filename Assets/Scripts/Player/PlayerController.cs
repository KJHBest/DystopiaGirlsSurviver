using UnityEngine;

namespace DystopiaGirls.Player
{
    /// <summary>
    /// 플레이어 이동 및 입력 처리
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 5f;

        private Rigidbody2D rb;
        private Vector2 moveInput;
        private Vector2 moveVelocity;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (GameManager.Instance != null && GameManager.Instance.IsGamePaused)
                return;

            // 입력 처리
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");
            moveInput.Normalize();

            moveVelocity = moveInput * moveSpeed;
        }

        private void FixedUpdate()
        {
            if (GameManager.Instance != null && GameManager.Instance.IsGamePaused)
            {
                rb.velocity = Vector2.zero;
                return;
            }

            // 이동 적용
            rb.velocity = moveVelocity;
        }

        public Vector2 GetMoveDirection()
        {
            return moveInput;
        }
    }
}
