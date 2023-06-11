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
        
        public void Setup(RotatingObjectAgent rotatingObjectAgent, Transform targetTransform)
        {
            _owner = rotatingObjectAgent;
            this._targetTransform = targetTransform;
        }
        protected override bool OnEnterInternal(StableDistanceNode node)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _uniTask = _owner.StableDistanceAsync(_targetTransform,node.distance, _cancellationTokenSource.Token);
            return true;
        }
    }
}