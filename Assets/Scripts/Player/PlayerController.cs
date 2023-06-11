using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] [Label("移動速度")] private float moveSpeed = 1f;
        private DefaultInputActions _input;
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _input = new DefaultInputActions();
            _input.Enable();
        }

        private void FixedUpdate()
        {
            var move = _input.Player.Move.ReadValue<Vector2>();
            var moveDirection = new Vector3(move.x, move.y, 0);
            var position = transform.position + moveDirection * Time.deltaTime * moveSpeed;
            _rigidbody2D.MovePosition(position);
        }
    }
}