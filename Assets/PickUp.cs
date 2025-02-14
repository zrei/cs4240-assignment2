using UnityEngine;

public class ProjectilePickUp : Projectile
{
    public void PickUp()
    {
        ScoreManager.Instance.AddScore(5);
        Despawn();
    }
}
