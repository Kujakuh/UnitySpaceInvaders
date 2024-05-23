using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject explosion; 
    public float speed;

    public GameObject bullet;
    public Transform bulletSpawnLocation;
    public float fireRate;
    public float delay;


    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed * Time.deltaTime;
        InvokeRepeating("Fire", delay, fireRate);
    }

    void Fire()
    {
        Instantiate(bullet, bulletSpawnLocation.position, bulletSpawnLocation.rotation);
        GetComponent<AudioSource>().Play();
    }

    void Update()
    {
        
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag != "Enemy" && other.gameObject.tag != "EnemyBullet") 
            Destroy(gameObject);
        if(other.gameObject.tag == "Bullet"){
            Instantiate(explosion, transform.position, transform.rotation);
            if(Random.Range(0, 100) <= GameManager.Instance.lootDropChance)
                Instantiate(GameManager.Instance.loot, transform.position, transform.rotation);
        }
    }
}
