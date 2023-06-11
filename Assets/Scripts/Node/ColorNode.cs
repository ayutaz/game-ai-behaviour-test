using Cysharp.Threading.Tasks;
using GameAiBehaviour;
using NaughtyAttributes;
using UnityEngine;

namespace DefaultNamespace
{
    /// <summary>
    /// 色の設定 Node
    /// </summary>
    public class ColorNode : HandleableActionNode
    {
        [Label("色")] public Color color;
    }

    /// <summary>
    /// 指定した色に変更するNodeHandler
    /// </summary>
    public class ColorNodeHandler : ActionNodeHandler<ColorNode>
    {
        private Agent _owner;
        private UniTask _uniTask;
        private SpriteRenderer _spriteRenderer;

        public void Setup(Agent owner,SpriteRenderer spriteRenderer)
        {
            _owner = owner;
            _spriteRenderer = spriteRenderer;
        }

        /// <summary>
        /// ノード開始時処理
        /// </summary>
        protected override bool OnEnterInternal(ColorNode node)
        {
            Agent.SetColor(_spriteRenderer, node.color);
            return true;
        }
    }
}