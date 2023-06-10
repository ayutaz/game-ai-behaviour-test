using System.Threading;
using Cysharp.Threading.Tasks;
using GameAiBehaviour;
using NaughtyAttributes;
using UnityEngine;

public class MoveNode : HandleableActionNode
{
    [Label("相対移動量")] public Vector3 offset;
    [Label("移動時間")] public float duration;
}

public class MoveNodeHandler : ActionNodeHandler<MoveNode>
{
    private Agent _owner;
    private CancellationTokenSource _cancellationTokenSource;
    private UniTask _uniTask;
    
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
    protected override bool OnEnterInternal(MoveNode node) {
        _cancellationTokenSource = new CancellationTokenSource();
        _uniTask = _owner.MoveAsync(node.offset, node.duration, _cancellationTokenSource.Token);
        return true;
    }

    /// <summary>
    /// ノード更新時処理
    /// </summary>
    protected override IActionNodeHandler.State OnUpdateInternal(MoveNode node) {
        if (_uniTask.Status == UniTaskStatus.Pending) {
            return IActionNodeHandler.State.Running;
        }

        return IActionNodeHandler.State.Success;
    }

    /// <summary>
    /// ノード終了時処理
    /// </summary>
    protected override void OnExitInternal(MoveNode node) {
        _cancellationTokenSource.Dispose();
        _cancellationTokenSource = null;
    }
}