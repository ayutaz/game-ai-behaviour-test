using System.Threading;
using Agents;
using NaughtyAttributes;
using UnityEngine;

namespace Node
{
    public class StableDistanceNode : NodeBase
    {
        [Label("保つ距離")] public float distance;
    }
    
    public class StableDistanceNodeHandler : NodeBaseHandler<StableDistanceNode>
    {
        private Transform _targetTransform;
        private EnemyAgent _agent;

        public void Setup(EnemyAgent agent)
        {
            _agent = agent;
        }

        public void Setup(EnemyAgent enemyAgent, Transform targetTransform)
        {
            _agent = enemyAgent;
            this._targetTransform = targetTransform;
        }
        protected override bool OnEnterInternal(StableDistanceNode node)
        {
            CancellationTokenSource = new CancellationTokenSource();
            UniTask = _agent.StableDistanceAsync(_targetTransform,node.distance, CancellationTokenSource.Token);
            return true;
        }
    }
}