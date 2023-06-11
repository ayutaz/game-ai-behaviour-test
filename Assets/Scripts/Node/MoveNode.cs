using System.Threading;
using Agents;
using NaughtyAttributes;
using UnityEngine;

namespace Node
{
    public class MoveNode : NodeBase
    {
        [Label("相対移動量")] public Vector3 offset;
        [Label("移動時間")] public float duration;
    }

    public class MoveNodeHandler : NodeBaseHandler<MoveNode>
    {
        private RotatingObjectAgent _owner;
        
        public void Setup(RotatingObjectAgent owner)
        {
            _owner = owner;
        }
        
        /// <summary>
        /// ノード開始時処理
        /// </summary>
        protected override bool OnEnterInternal(MoveNode node) {
            CancellationTokenSource = new CancellationTokenSource();
            UniTask = _owner.MoveAsync(node.offset, node.duration, CancellationTokenSource.Token);
            return true;
        }
    }
    
}
