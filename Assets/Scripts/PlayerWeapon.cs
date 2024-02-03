using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerWeapon : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public BulletScript bulletPrefab;
    public float bulletSpeed = 10;

    private ObjectPool<BulletScript> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<BulletScript>(CreateBullet, TakeBulletFromPool, OnPutBackInPool, OnDestroyBullet, false, 10, 15);
    }

    private BulletScript CreateBullet()
    {
        var bullet = Instantiate(bulletPrefab);

        bullet.SetPool(_pool);
        return bullet;
    }
    
    private void TakeBulletFromPool(BulletScript bullet)
    {
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.transform.rotation = bulletSpawnPoint.rotation;

        bullet.gameObject.SetActive(true);
    }
    
    private void OnPutBackInPool(BulletScript bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(BulletScript bullet)
    {
        Destroy(bullet.gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        OnPlayerTrigger();
    }

    public void OnPlayerTrigger()
    {
       if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            var bullet = _pool.Get(); 
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
        }
    }
}
