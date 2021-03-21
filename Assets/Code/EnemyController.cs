using System;
using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    Player player;
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] Transform bulletSpawnPoint; 
    [SerializeField, Range(0,100)] float shootDistance = 20;
    [SerializeField] float shootForce = 10;

    void Awake() {
        player = FindObjectOfType<Player>();
        StartCoroutine(ShootCoroutine());
    }

    IEnumerator ShootCoroutine() {
        while (true) {
            yield return new WaitForSeconds(0.5f);
            if (IsPlayerCloseEnough())
                Shoot();
        }
    }

    void Shoot() {
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity, GameObject.Find("/Bullets").transform);
        
        var playerDirection = (player.transform.position - bulletSpawnPoint.position).normalized;
        bullet.GetComponent<Rigidbody>().AddForce(playerDirection * shootForce, ForceMode.VelocityChange);
    }

    bool IsPlayerCloseEnough() {
        return Vector3.Distance(player.transform.position, transform.position) < shootDistance;
    }

    void Update() {
        transform.LookAt(player.transform.position);
    }

    void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, shootDistance);
    }
}