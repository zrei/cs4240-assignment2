using UnityEngine;

public class Player : Singleton<Player>
{
    public Planet CurrPlanet { get; private set; } = null;

    public void SetPlanet(Planet planet)
    {
        CurrPlanet = planet;
    }
}
