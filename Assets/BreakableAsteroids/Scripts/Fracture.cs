using UnityEngine;

public class Fracture : MonoBehaviour
{
    [Tooltip("\"Fractured\" is the object that this will break into")]
    [SerializeField] private GameObject m_FracturedObj;
    public void FractureObject()
    {
        Instantiate(m_FracturedObj, transform.position, transform.rotation); //Spawn in the broken version
        //Destroy(gameObject); //Destroy the object to stop it getting in the way
    }
}
