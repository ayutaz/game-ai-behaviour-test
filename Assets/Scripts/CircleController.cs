using System;
using DefaultNamespace;
using GameAiBehaviour;
using NaughtyAttributes;
using UnityEngine;

public class CircleController : MonoBehaviour,IBehaviourTreeControllerProvider
{
    private BehaviourTreeController _controller;
    [SerializeField,Label("実行対象のツリー")] private BehaviourTree behaviourTree;
    
    [SerializeField,Label("試行頻度")]
    private float tickInterval = 1.0f;

    [SerializeField] private Agent agent;
    BehaviourTreeController IBehaviourTreeControllerProvider.BehaviourTreeController => _controller;

    private SpriteRenderer _spriteRenderer;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _controller = new BehaviourTreeController();
        _controller.TickInterval = tickInterval;
        _controller.Setup(behaviourTree);
    }

    private void Start()
    {
        _controller.BindActionNodeHandler<CircleMoveNode,CircleMoveHandler>(handler =>
        {
            handler.Setup(agent);
        });
    }

    private void Update()
    {
        _controller.Update(Time.deltaTime);
        var theta = Util.ColorUtil.GetAngleFromVectors(Vector3.zero, transform.position);
        var color = Util.ColorUtil.HSVtoRGB(theta, 100f, 100f);
        _spriteRenderer.color = new Color(color.R, color.G, color.B, 1.0f);
    }

    private void OnDestroy()
    {
        _controller.ResetActionNodeHandlers();
        _controller.Cleanup();
    }
}
