using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loot : MonoBehaviour
{
    private Rigidbody rg;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody>();
        rg.velocity = new Vector3(0f, 0f, -1f) * speed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate( new Vector3(0.2f, 0.3f, 0.1f) );
    }

    void OnCollisionEnter(Collision other)
    {
        if((other.gameObject.tag != "Enemy") && (other.gameObject.tag != "EnemyBullet")) 
            Destroy(gameObject);
    }
}
