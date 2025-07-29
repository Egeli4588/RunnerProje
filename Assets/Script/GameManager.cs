using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] road;

    [SerializeField] Transform Player;
    [SerializeField] Transform roadParent;

    float roadLength = 20f;
    int startRoadCount = 6;

    private void Start()
    {
        Instantiate(road[0], transform.position, Quaternion.identity, roadParent);

        for (int i = 0; i < startRoadCount; i++)
        {
            GenerateRoad();
        }
    }


    private void Update()
    {
        if (Player.position.z > roadLength / 2 - 20)
        {
            GenerateRoad();
        }
    }
    void GenerateRoad()
    {
        Instantiate(road[Random.Range(0, road.Length)], transform.position + new Vector3(0, 0, roadLength), Quaternion.identity, roadParent);
        roadLength += 20;
    }
}
