﻿using System.Threading;
using DefaultNamespace;
using NaughtyAttributes;
using UnityEngine;

public class MoveNode : NodeBase
{
    [Label("相対移動量")] public Vector3 offset;
    [Label("移動時間")] public float duration;
}

public class MoveNodeHandler : NodeBaseHandler<MoveNode>
{
    /// <summary>
    /// ノード開始時処理
    /// </summary>
    protected override bool OnEnterInternal(MoveNode node) {
        _cancellationTokenSource = new CancellationTokenSource();
        _uniTask = _owner.MoveAsync(node.offset, node.duration, _cancellationTokenSource.Token);
        return true;
    }
}