using UnityEngine;

[RequireComponent(typeof(IHeightHandle))]
public class MinimumHeight : MonoBehaviour
{
    [SerializeField] private float m_MinimumWorldHeight;

    private IHeightHandle m_Handle;

    private void Start()
    {
        m_Handle = GetComponent<IHeightHandle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < m_MinimumWorldHeight)
        {
            m_Handle.OnHeightDestroy();
        }
    }
}

public interface IHeightHandle
{
    void OnHeightDestroy();
}
