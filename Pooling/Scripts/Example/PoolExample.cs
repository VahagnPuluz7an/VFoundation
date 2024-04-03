using UnityEngine;

namespace VFoundation.Pooling.Scripts.Example
{
    public class PoolExample : MonoBehaviour
    {
        [SerializeField] private PoolPrefab prefab;
        [SerializeField] private int startCount;
        [SerializeField] private bool autoExpand;

        private PoolMono<PoolPrefab> _pool;

        private void Start() => _pool = new PoolMono<PoolPrefab>(prefab, startCount)
        {
            AutoExpand = autoExpand
        };

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0))
                return;

            PoolPrefab cube = _pool.GetFreeElement();
            cube.transform.position = Random.insideUnitSphere * 3;
        }
    }
}