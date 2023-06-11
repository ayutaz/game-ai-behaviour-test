using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Agent : MonoBehaviour
{
    /// <summary>
    /// 相対移動
    /// </summary>
    public async UniTask MoveAsync(Vector3 offsetPosition, float duration, CancellationToken ct) {
        var timer = duration;
        var startPos = transform.position;
        var endPos = transform.TransformPoint(offsetPosition);
        while (timer > 0.0f) {
            var ratio = Mathf.Clamp01(1 - timer / duration);
            transform.position = Vector3.Lerp(startPos, endPos, ratio);
            timer -= Time.deltaTime;
            if (timer <= 0.0f) {
                break;
            }
            await UniTask.Yield(ct);
        }

        transform.position = endPos;
    }

    /// <summary>
    /// 円運動
    /// </summary>
    /// <param name="radius"></param>
    /// <param name="duration"></param>
    /// <param name="ct"></param>
    public async UniTask CircleMoveAsync(float radius, float duration, CancellationToken ct)
    {
        var center = transform.position;  // 基準点（円運動の中心）
        var angle = 0.0f;  // 角度（ラジアン）
        var speed = 2.0f * Mathf.PI / duration;  // 速度（ラジアン/秒）

        while (!ct.IsCancellationRequested)
        {
            // 角度を更新（速度に応じて）
            angle += speed * Time.deltaTime;

            // 新しい位置を計算
            var x = radius * Mathf.Cos(angle);
            var y = radius * Mathf.Sin(angle);
            var newPosition = center + new Vector3(x, y, 0);

            // 新しい位置にオブジェクトを移動
            transform.position = newPosition;

            await UniTask.Yield(ct);
        }
    }

    /// <summary>
    /// 色の設定
    /// </summary>
    /// <param name="spriteRenderer"></param>
    /// <param name="color"></param>
    public static void SetColor(SpriteRenderer spriteRenderer, Color color) => spriteRenderer.color = color;
}
