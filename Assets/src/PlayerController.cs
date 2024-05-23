using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rotation;
    public float speed;
    private Rigidbody rg;

    [Range(0.05f, 1f)]
    public float shootRate = 0.2f;
    private float nextShoot = 0f;
    public AudioClip shootAudio;
    public AudioClip hitAudio;

    public GameObject explosion; 
    public GameObject hit;
    public GameObject bullet;
    public Transform bulletSpawnLocation;
    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    void FixedUpdate() {
        float hMovement = Input.GetAxis("Horizontal");
        float vMovement = Input.GetAxis("Vertical");
        
        if (hMovement != 0f || vMovement != 0f) 
            rg.velocity = new Vector3(hMovement, 0f, vMovement) * speed * Time.deltaTime;
        else
            rg.velocity = new Vector3(0f,0f,0f);

        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        rg.rotation = Quaternion.Euler(0, 0, rg.velocity.x * -rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && Time.time > nextShoot)
        {
            nextShoot = Time.time + shootRate;
            audio.PlayOneShot(shootAudio ,1f);
            Instantiate(bullet, bulletSpawnLocation.position, bulletSpawnLocation.rotation);
        }
    }
    
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "EnemyBullet") {
            if(GameManager.Instance.loseHp())
            {
                GameManager.Instance.isGameOver = true;
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(gameObject);
                GameManager.Instance.GameOver();
            }
            else
            {
                GameObject hitFeedback = Instantiate(hit, transform.position - new Vector3(1,0,3), transform.rotation);
                hitFeedback.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                audio.PlayOneShot(hitAudio ,2f);
            }
        }
        if(other.gameObject.tag == "Loot")
        {
            Destroy(other.gameObject);
            GameManager.Instance.AddScore(GameManager.Instance.lootScore);
            audio.PlayOneShot(GameManager.Instance.lootAudio, 1f);
        }
    }

}
