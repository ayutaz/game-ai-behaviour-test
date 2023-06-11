using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Agents
{
    public class EnemyAgent
    {
        private Transform _myselfTransform;
        private Rigidbody2D _rigidbody2D;
        private Rigidbody2D _targetRigidbody2D;

        public void Setup(Rigidbody2D targetRigidbody2D, Rigidbody2D myselfRigidbody2D,
            Transform myselfTransform)
        {
            _rigidbody2D = myselfRigidbody2D;
            _targetRigidbody2D = targetRigidbody2D;
            _myselfTransform = myselfTransform;
        }

        public async UniTask StableDistanceAsync(float distance, CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                var direction = _targetRigidbody2D.position - (Vector2)_myselfTransform.position;
                var currentDistance = direction.magnitude;
                if (currentDistance > distance)
                {
                    var position = direction.normalized * (currentDistance - distance) +
                                   (Vector2)_myselfTransform.position;
                    _rigidbody2D.MovePosition(position);
                }

                await UniTask.Yield(ct);
            }
        }
    }
}