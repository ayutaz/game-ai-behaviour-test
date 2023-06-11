using System.Threading;
using Cysharp.Threading.Tasks;
using GameAiBehaviour;

namespace Node
{
    public abstract class NodeBase : HandleableActionNode
    {
    }

    public class NodeBaseHandler<T> : ActionNodeHandler<T> where T : NodeBase
    {
        protected CancellationTokenSource CancellationTokenSource;
        protected UniTask UniTask;

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
            if (UniTask.Status == UniTaskStatus.Pending)
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
            CancellationTokenSource.Dispose();
            CancellationTokenSource = null;
        }
    }
}