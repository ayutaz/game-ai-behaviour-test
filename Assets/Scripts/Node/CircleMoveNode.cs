﻿using System.Threading;
using Agents;
using NaughtyAttributes;

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
            _cancellationTokenSource = new CancellationTokenSource();
            _uniTask = _owner.CircleMoveAsync(node.radius, node.duration, _cancellationTokenSource.Token);
            return true;
        }
    }
}