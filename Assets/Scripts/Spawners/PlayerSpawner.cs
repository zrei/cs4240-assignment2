using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Player m_Player;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Player player = Instantiate(m_Player);
        Planet randomPlanet = GetRandomPlanet();
        randomPlanet.PlacePlayer(player.transform);
        player.SetPlanet(randomPlanet);
    }

    private Planet GetRandomPlanet()
    {
        return transform.GetChild(Random.Range(0, transform.childCount)).GetComponent<Planet>();
    }
}
