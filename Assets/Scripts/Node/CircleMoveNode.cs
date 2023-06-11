using System.Threading;
using Agents;
using NaughtyAttributes;
using UnityEngine;

namespace Node
{
    public class CircleMoveNode : NodeBase
    {
        [Label("移動半径")] public float radius;
        [Label("移動時間")] public float duration;
    }

    public class CircleMoveHandler : NodeBaseHandler<CircleMoveNode>
    {
        private RotatingObjectAgent _owner;

        public void Setup(RotatingObjectAgent owner)
        {
            _owner = owner;
        }

        protected override bool OnEnterInternal(CircleMoveNode node)
        {
            CancellationTokenSource = new CancellationTokenSource();
            node.radius = Random.Range(1f, 4f);
            UniTask = _owner.CircleMoveAsync(node.radius, node.duration, CancellationTokenSource.Token);
            return true;
        }
    }
}