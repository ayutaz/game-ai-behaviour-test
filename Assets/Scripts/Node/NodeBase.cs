using System.Threading;
using Cysharp.Threading.Tasks;
using GameAiBehaviour;

namespace DefaultNamespace
{
    public abstract class NodeBase : HandleableActionNode
    {
    }

    public class NodeBaseHandler<T> : ActionNodeHandler<T> where T : NodeBase
    {
        protected Agent _owner;
        protected CancellationTokenSource _cancellationTokenSource;
        protected UniTask _uniTask;

        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <param name="owner"></param>
        public void Setup(Agent owner)
        {
            _owner = owner;
        }

        /// <summary>
        /// ノード開始時処理
        /// </summary>
        protected override bool OnEnterInternal(T node)
        {
            return true;
        }

        /// <summary>
        /// ノード更新時処理
        /// </summary>
        protected override IActionNodeHandler.State OnUpdateInternal(T node)
        {
            if (_uniTask.Status == UniTaskStatus.Pending)
            {
                return IActionNodeHandler.State.Running;
            }

            return IActionNodeHandler.State.Success;
        }

        /// <summary>
        /// ノード終了時処理
        /// </summary>
        protected override void OnExitInternal(T node)
        {
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;
        }
    }
}