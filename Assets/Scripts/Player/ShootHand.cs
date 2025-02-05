using UnityEngine;

[RequireComponent(typeof(HandPositioning))]
public class ShootHand : BaseHand
{
    private HandPositioning m_HandPositioning;

    private void Start()
    {
        m_HandPositioning = GetComponent<HandPositioning>();
    }

    protected override void HandleHandInput()
    {
        // replace with your implementation! you can access the linear velocity
        // and angular velocity from the hand positioning component
    }
}
