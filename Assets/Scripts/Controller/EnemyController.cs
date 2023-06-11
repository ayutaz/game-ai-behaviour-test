using Agents;
using GameAiBehaviour;
using NaughtyAttributes;
using Node;
using UnityEngine;

namespace Controller
{
    public class EnemyController : MonoBehaviour, IBehaviourTreeControllerProvider
    {
        [SerializeField] [Label("実行対象のツリー")] private BehaviourTree behaviourTree;
        [SerializeField] [Label("実行頻度")] private float tickInterval = 1.0f;

        private BehaviourTreeController _controller;
        private EnemyAgent _enemyAgent;
        private Rigidbody2D _playerRigidbody2D;
        private Transform _playerTransform;
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _enemyAgent = new EnemyAgent();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _controller = new BehaviourTreeController();
            _controller.TickInterval = tickInterval;
            _controller.Setup(behaviourTree);
            var player = GameObject.FindWithTag("Player");
            _playerTransform = player.transform;
            _playerRigidbody2D = player.GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _controller.BindActionNodeHandler<StableDistanceNode, StableDistanceNodeHandler>(handler =>
            {
                handler.Setup(_enemyAgent, _playerRigidbody2D, _rigidbody2D, transform);
            });
        }

        private void Update()
        {
            _controller.Update(Time.deltaTime);
        }

        private void OnDestroy()
        {
            _controller.ResetActionNodeHandlers();
            _controller.Cleanup();
        }

        BehaviourTreeController IBehaviourTreeControllerProvider.BehaviourTreeController => _controller;
    }
}