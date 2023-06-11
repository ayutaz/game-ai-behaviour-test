using System.Threading;
using Cysharp.Threading.Tasks;
using GameAiBehaviour;
using NaughtyAttributes;

namespace DefaultNamespace
{
    public class CircleMoveNode : HandleableActionNode
    {
        [Label("移動半径")] public float radius;
        [Label("移動時間")] public float duration;
    }

    public class CircleMoveHandler : ActionNodeHandler<CircleMoveNode>
    {
        private Agent _owner;
        private CancellationTokenSource _cancellationTokenSource;
        private UniTask _uniTask;
        
        public void Setup(Agent owner)
        {
            _owner = owner;
        }

        protected override bool OnEnterInternal(CircleMoveNode node)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _uniTask = _owner.CircleMoveAsync(node.radius, node.duration, _cancellationTokenSource.Token);
            return true;
        }

        protected override IActionNodeHandler.State OnUpdateInternal(CircleMoveNode node)
        {
            if (_uniTask.Status == UniTaskStatus.Pending)
            {
                return IActionNodeHandler.State.Running;
            }
            
            return IActionNodeHandler.State.Success;
        }
        
        protected override void OnExitInternal(CircleMoveNode node)
        {
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;
        }
    }
}