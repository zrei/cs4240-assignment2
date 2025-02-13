using UnityEngine;

//[RequireComponent(typeof(HandPositioning))]
public class ShootHand : BaseHand
{
    //private HandPositioning m_HandPositioning;
    public GameObject bonusPickup;

    protected override void Start()
    {
        base.Start();
        //m_HandPositioning = GetComponent<HandPositioning>();
    }



    protected override void HandleHandInput()
    {

        ShootGun gun = GetComponentInChildren<ShootGun>();
        if (gun != null)
        {
            gun.Shoot();
        }
        else
        {
            Debug.LogWarning("No gun found as a child of the ShootHand.");
        }




    }
}
