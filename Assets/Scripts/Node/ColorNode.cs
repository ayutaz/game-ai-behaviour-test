using Agents;
using NaughtyAttributes;
using UnityEngine;

namespace Node
{
    /// <summary>
    /// 色の設定 Node
    /// </summary>
    public class ColorNode : NodeBase
    {
        [Label("色")] public Color color;
    }

    /// <summary>
    /// 指定した色に変更するNodeHandler
    /// </summary>
    public class ColorNodeHandler : NodeBaseHandler<ColorNode>
    {
        private SpriteRenderer _spriteRenderer;
        private RotatingObjectAgent _owner;

        public void Setup(RotatingObjectAgent owner, SpriteRenderer spriteRenderer)
        {
            _owner = owner;
            _spriteRenderer = spriteRenderer;
        }

        /// <summary>
        /// ノード開始時処理
        /// </summary>
        protected override bool OnEnterInternal(ColorNode node)
        {
            RotatingObjectAgent.SetColor(_spriteRenderer, node.color);
            return true;
        }
    }
}