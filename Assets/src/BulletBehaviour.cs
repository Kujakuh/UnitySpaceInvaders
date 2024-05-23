using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public bool isEnemyBullet;
    public int enemyKillPoints;
    public float scaleFactor;
    public float bulletSpeed;
    public Rigidbody rg;
    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float flip = 1f;
        float rot = 0f;
        if(isEnemyBullet) {
            flip = -1f;
            rot = 90f;
        }
        rg.velocity = new Vector3(0f, 0f, flip) * bulletSpeed * Time.deltaTime;
        rg.rotation = Quaternion.Euler(rot, 0f, 0f);
        if (transform.localScale.x > 0.0008f)
            transform.localScale *= scaleFactor;
        else
            Destroy(gameObject);
    }
    void OnCollisionEnter(Collision other)
    {
        if((other.gameObject.tag == "Enemy") && (gameObject.tag != "EnemyBullet")){
            Destroy(gameObject);
            GameManager.Instance.AddScore(enemyKillPoints);
        }
        if(other.gameObject.tag == "BoundaryBack") 
            Destroy(gameObject);
    }
}
