using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Player m_Player;
    [SerializeField] private Planet[] m_Planets;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Player player = Instantiate(m_Player);
        Planet randomPlanet = GetRandomPlanet();
        randomPlanet.PlacePlayer(player.transform);
        player.SetPlanet(randomPlanet);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Planet GetRandomPlanet()
    {
        return m_Planets[Random.Range(0, m_Planets.Length)];
    }
}
