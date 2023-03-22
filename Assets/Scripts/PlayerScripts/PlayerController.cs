using Components;
using UnityEngine;

namespace Scripts.PlayerScripts
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _jumpForce = 100f;
        [SerializeField] private GroundCheck _groundCheck;
        
        private readonly int _animationKeyIsGround = Animator.StringToHash("isGround");
        private readonly int _animationKeyIsRunning = Animator.StringToHash("isRunning");
        private readonly int _animationKeyJump = Animator.StringToHash("Jump");
        private readonly int _animationKeyAttack = Animator.StringToHash("Attack");
        private readonly int _animationKeyDead = Animator.StringToHash("Dead");
        
        private readonly Vector3 _rightDirection =  new Vector3(1, 1, 1);
        private readonly Vector3 _leftDirection =  new Vector3(-1, 1, 1);

        private PlayerInput _playerInput;
        private Rigidbody2D _rigidbody2D;
        private Animator _animator;

        private float _horizontalMove;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _playerInput.Movement += PlayerInputOnMovement;
            _playerInput.Jump += PlayerInputOnJump;
            _playerInput.Attack += PlayerInputOnAttack;
        }

        private void PlayerInputOnMovement(float horizontalMove)
        {
            _horizontalMove = horizontalMove;
            ChangeDirectionScale();
        }

        private void PlayerInputOnJump()
        {
            if (_groundCheck.IsGrounded)
            {
                _rigidbody2D.AddForce(new Vector2(0f, _jumpForce), ForceMode2D.Force);
                _animator.SetTrigger(_animationKeyJump);
            }
        }

        private void PlayerInputOnAttack()
        {
            _animator.SetTrigger(_animationKeyAttack);
        }

        private void ChangeDirectionScale()
        {
            if (_horizontalMove > 0)
                transform.localScale = _rightDirection;
            else if (_horizontalMove < 0)
                transform.localScale = _leftDirection;
        }

        private void Update()
        {
            _animator.SetBool(_animationKeyIsGround, _groundCheck.IsGrounded);
            _animator.SetBool(_animationKeyIsRunning, _horizontalMove != 0);
            
            var x = _horizontalMove * _speed;
            var y = _rigidbody2D.velocity.y;
            _rigidbody2D.velocity = new Vector2(x, y);
        }

        private void OnDisable()
        {
            _playerInput.Movement -= PlayerInputOnMovement;
            _playerInput.Jump -= PlayerInputOnJump;
            _playerInput.Attack -= PlayerInputOnAttack;
        }
    }
}