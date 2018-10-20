using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour {

    [SerializeField] float health = 100f;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 02.2f;
    [SerializeField] float maxTimeBetweenShots = 6f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserSpeed = 10f;
    [SerializeField] GameObject particlePrefab;
    [SerializeField] AudioClip enemyDeathSound;
    [SerializeField] [Range(0,1)]float enemyDeathSoundVolume = 1f;
    [SerializeField] AudioClip laserSound;
    [SerializeField] [Range(0, 1)] float laserSoundVolume = 0.1f;


    // Use this for initialization
    void Start () {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
	}
	
	// Update is called once per frame
	void Update ()
    {
        CountDownAndShoot();
	}

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if(shotCounter <= 0)
        {
            Fire();
            shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);

        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(laserPrefab, 
                                        transform.position, 
                                        Quaternion.identity)
                            as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -laserSpeed);
        AudioSource.PlayClipAtPoint(laserSound, Camera.main.transform.position, laserSoundVolume);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(other, damageDealer);

    }

    private void ProcessHit(Collider2D other, DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Destroy(other.gameObject);
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        GameObject particle = Instantiate(particlePrefab,
            transform.position,
            Quaternion.identity) as GameObject;
        Destroy(particle, 1f);
        AudioSource.PlayClipAtPoint(enemyDeathSound, Camera.main.transform.position, enemyDeathSoundVolume);
    }
}
