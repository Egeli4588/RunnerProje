using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] road;
    [SerializeField] GameObject[] collectables;
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
        SpawnCollectable();
    }

    void SpawnCollectable()
    {
        /* GameObject collectableObject = Instantiate(collectables[Random.Range(0, collectables.Length)], Player.position + new Vector3(0, 0.5f, 50f), Quaternion.identity);


         Invoke("SpawnCollectable", Random.Range(3f, 10f));*/

        // x i�in 3 se�enek
        float[] possibleXPositions = new float[] { -2f, 0f, 2f };
        // Random index se�
        float spawnX = possibleXPositions[Random.Range(0, possibleXPositions.Length)];

        // Spawn pozisyonunu olu�tur
        Vector3 spawnPos = new Vector3(spawnX, Player.position.y + 0.5f, Player.position.z + 50f);

        // Collectable olu�tur
        GameObject collectableObject = Instantiate(collectables[Random.Range(0, collectables.Length)], spawnPos, Quaternion.identity);

        // Tekrar �a��rma
        Invoke("SpawnCollectable", Random.Range(3f, 10f));
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
