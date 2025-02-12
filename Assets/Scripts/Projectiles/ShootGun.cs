using UnityEngine;
using System.Collections.Generic;

public class ShootGun : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float launchVelocity = 700f;
    [SerializeField] private AudioClip m_ShootClip;
    private AudioSource m_AudioSource;


    private List<GameObject> bulletPool = new List<GameObject>();
    private int poolSize = 10;

    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(projectilePrefab);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }

    public void Shoot()
    {
        GameObject bullet = GetPooledBullet();
        if (bullet == null)
        {
            Debug.LogWarning("No available bullets in the pool!");
            return;
        }
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;


        bullet.SetActive(true);

        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            bulletRigidbody.linearVelocity = Vector3.zero;
            bulletRigidbody.AddRelativeForce(new Vector3(launchVelocity, 0, 0));
        }

        if (m_AudioSource != null && m_ShootClip != null)
        {
            m_AudioSource.PlayOneShot(m_ShootClip);
        }
    }

    private GameObject GetPooledBullet()
    {
        foreach (GameObject bullet in bulletPool)
        {
            if (!bullet.activeInHierarchy)
            {
                return bullet;
            }
        }

        GameObject newBullet = Instantiate(projectilePrefab);
        newBullet.SetActive(false);
        bulletPool.Add(newBullet);
        return newBullet;
    }
}