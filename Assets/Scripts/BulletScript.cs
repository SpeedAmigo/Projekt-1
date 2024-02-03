using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletScript : MonoBehaviour
{
    public float life = 3f;

    private ObjectPool<BulletScript> _pool;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _coroutine = StartCoroutine(DeactivateBulletAfterTime());
    }

    private void OnCollisionEnter(Collision collision)
    {
        _pool.Release(this);
    }

    public void SetPool(ObjectPool<BulletScript> pool)
    {
        _pool = pool;
    }

    private IEnumerator DeactivateBulletAfterTime()
    {
        float elapsedTime = 0f;
        while (elapsedTime < life)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _pool.Release(this);
    }
}
