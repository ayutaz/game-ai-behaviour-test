using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Agents
{
    public class EnemyAgent : MonoBehaviour
    {
        public async UniTask StableDistanceAsync(Transform targetTransform,float distance, CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                var direction = targetTransform.position - transform.position;
                var currentDistance = direction.magnitude;
                if (currentDistance > distance)
                {
                    transform.position += direction.normalized * (currentDistance - distance);
                }

                await UniTask.Yield(ct);
            }
        }
    }
}