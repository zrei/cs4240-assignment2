using UnityEngine;

public class Fracture : MonoBehaviour
{
    [Tooltip("\"Fractured\" is the object that this will break into")]
    [SerializeField] private GameObject m_FracturedObj;
    public void FractureObject()
    {
        Instantiate(m_FracturedObj, transform.position, transform.rotation); 
        ParticleSystem particleEffect = GetComponentInChildren<ParticleSystem>();
        if (particleEffect != null)
        {
            particleEffect.Play();
        }
        Destroy(gameObject);  
           
    }
}
