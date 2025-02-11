using UnityEngine;

public class ShootGun : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject projectile;
    public float launchVelocity = 700f;
    [SerializeField] private AudioClip m_ShootCLip;
    private AudioSource m_AudioSource;
    void Start()
    {
        Shoot();
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(projectile, transform.position,
                                                     transform.rotation);
        bullet.AddComponent<Rigidbody>();
        bullet.GetComponent<Rigidbody>().AddRelativeForce(new Vector3
                                                (launchVelocity,0 , 0));

    }
}
