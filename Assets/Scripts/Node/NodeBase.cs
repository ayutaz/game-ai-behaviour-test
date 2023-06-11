using System.Threading;
using Agents;
using Cysharp.Threading.Tasks;
using GameAiBehaviour;

namespace Node
{
    public abstract class NodeBase : HandleableActionNode
    {
    }

    public class NodeBaseHandler<T> : ActionNodeHandler<T> where T : NodeBase
    {
        protected CancellationTokenSource _cancellationTokenSource;
        protected UniTask _uniTask;

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