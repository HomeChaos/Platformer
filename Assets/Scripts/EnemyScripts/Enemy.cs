using UnityEngine;

namespace EnemyScripts
{
    [RequireComponent(typeof(Animator))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyVision _vision;
        [SerializeField] private float _speed = 1f;
        [SerializeField] private Transform[] _patrolPoints;
        [SerializeField] private float _minContactDistance;

        private readonly Vector3 _rightDirection =  new Vector3(1, 1, 1);
        private readonly Vector3 _leftDirection =  new Vector3(-1, 1, 1);
        
        private readonly int _animationKeyAttack = Animator.StringToHash("Attack");
        
        private Animator _animator;
        private int _currentPoint = 0;

        private void OnEnable()
        {
            _vision.Detection += OnPlayerDetection;
        }

        private void Start()
        {
            ChangeDirectionScale();
            _animator = GetComponent<Animator>();
        }

        private void OnPlayerDetection()
        {
            _animator.SetTrigger(_animationKeyAttack);
        }

        private void ChangeDirectionScale()
        {
            var distance = transform.position.x - _patrolPoints[_currentPoint].position.x;

            if (distance > 0)
                transform.localScale = _leftDirection;
            else if (distance < 0)
                transform.localScale = _rightDirection;
        }

        private void Update()
        {
            var distance = Vector3.Distance(transform.position, _patrolPoints[_currentPoint].position);
            
            if (distance <= _minContactDistance)
            {
                _currentPoint = (_currentPoint + 1) % _patrolPoints.Length;
                ChangeDirectionScale();
            }

            transform.position = Vector2.MoveTowards(transform.position, _patrolPoints[_currentPoint].position,
                _speed * Time.deltaTime);
        }

        private void OnDisable()
        {
            _vision.Detection -= OnPlayerDetection;
        }
    }
}