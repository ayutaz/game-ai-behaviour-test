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
        _controller.BindActionNodeHandler<MoveNode,MoveNodeHandler>(handler =>
        {
            handler.Setup(agent);
        });
        
        _controller.BindActionNodeHandler<ColorNode,ColorNodeHandler>(handler => handler.Setup(agent,_spriteRenderer));
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
}
