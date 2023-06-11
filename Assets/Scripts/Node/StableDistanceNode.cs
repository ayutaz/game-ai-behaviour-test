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
        private EnemyAgent _agent;

        public void Setup(EnemyAgent enemyAgent, Rigidbody2D targetRigidbody2D,
            Rigidbody2D myselfRigidbody2D, Transform transform)
        {
            _agent = enemyAgent;
            _agent.Setup(targetRigidbody2D, myselfRigidbody2D, transform);
        }

        protected override bool OnEnterInternal(StableDistanceNode node)
        {
            CancellationTokenSource = new CancellationTokenSource();
            UniTask = _agent.StableDistanceAsync(node.distance, CancellationTokenSource.Token);
            return true;
        }
    }
}