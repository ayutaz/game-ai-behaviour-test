using Agents;
using GameAiBehaviour;
using NaughtyAttributes;
using Node;
using UnityEngine;
using Util;

namespace Controller
{
    /// <summary>
    ///     回転するオブジェクトのコントローラー
    /// </summary>
    public class CircleController : MonoBehaviour, IBehaviourTreeControllerProvider
    {
        [SerializeField] [Label("実行対象のツリー")] private BehaviourTree behaviourTree;

        [SerializeField] [Label("試行頻度")] private float tickInterval = 1.0f;

        private BehaviourTreeController _controller;

        private RotatingObjectAgent _rotatingObjectAgent;

        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rotatingObjectAgent = new RotatingObjectAgent(transform);
            _controller = new BehaviourTreeController();
            _controller.TickInterval = tickInterval;
            _controller.Setup(behaviourTree);
        }

        private void Start()
        {
            _controller.BindActionNodeHandler<CircleMoveNode, CircleMoveHandler>(handler =>
            {
                handler.Setup(_rotatingObjectAgent);
            });
        }

        private void Update()
        {
            _controller.Update(Time.deltaTime);
            var theta = ColorUtil.GetAngleFromVectors(Vector3.zero, transform.position);
            var color = ColorUtil.HSVtoRGB(theta, 100f, 100f);
            _spriteRenderer.color = new Color(color.R, color.G, color.B, 1.0f);
        }

        private void OnDestroy()
        {
            _controller.ResetActionNodeHandlers();
            _controller.Cleanup();
        }

        BehaviourTreeController IBehaviourTreeControllerProvider.BehaviourTreeController => _controller;
    }
}