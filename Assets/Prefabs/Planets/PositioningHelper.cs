
using UnityEngine;
using UnityEditor;

public class PositioningHelper : MonoBehaviour
{
    [SerializeField] private Transform m_FaceObject;
    [SerializeField] private float m_CircularRadius;
    [SerializeField] private ObjectSpawner m_AsteroidSpawner;
    [SerializeField] private float m_AsteroidSpawnHeight;
    public void RotateAllPlanets()
    {
        foreach (Transform childPlanet in transform)
        {
            childPlanet.LookAt(m_FaceObject, Vector3.up);
        }
    }

    public void PositionAllPlanets()
    {
        int numPlanets = transform.childCount;
        float angularDivision = 360 / numPlanets;
        Vector3 center = m_FaceObject.position;
        for (int i = 0; i < numPlanets; ++i)
        {
            Transform childPlanet = transform.GetChild(i);
            childPlanet.position = center + Quaternion.AngleAxis(angularDivision * i, Vector3.up) * Vector3.forward * m_CircularRadius;
        }
    }

    public void PositionAsteroidSpawner()
    {
        m_AsteroidSpawner.transform.position = m_FaceObject.position + new Vector3(0, m_AsteroidSpawnHeight);
        m_AsteroidSpawner.transform.rotation = m_FaceObject.rotation;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(PositioningHelper))]
public class RotatorHelperEditor : Editor
{
    private PositioningHelper m_Target;

    private void OnEnable()
    {
        m_Target = (PositioningHelper)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Position planets"))
        {
            m_Target.PositionAllPlanets();
        }

        if (GUILayout.Button("Rotate planets"))
        {
            m_Target.RotateAllPlanets();
        }

        if (GUILayout.Button("Position asteroid spawner"))
        {
            m_Target.PositionAsteroidSpawner();
        }
    }
}
#endif