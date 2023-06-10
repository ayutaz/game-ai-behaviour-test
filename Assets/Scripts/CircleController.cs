using System;
using GameAiBehaviour;
using UnityEngine;

public class CircleController : MonoBehaviour
{
    private BehaviourTreeController _controller;
    [SerializeField] private BehaviourTree behaviourTree;

    private void Awake()
    
    {
        _controller = new BehaviourTreeController();
        _controller.TickInterval = 1.0f;
        _controller.Setup(behaviourTree);
    }

    private void Start()
    {
        _controller.Update(Time.deltaTime);
    }

    private void Update()
    {
        // _controller.Update(Time.deltaTime);
    }
}
