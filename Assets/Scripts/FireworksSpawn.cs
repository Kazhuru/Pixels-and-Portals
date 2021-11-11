using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworksSpawn : MonoBehaviour
{
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] float interval;
    [SerializeField] float intervalRand;
    [SerializeField] GameObject[] fireworksPrefabs;

    private void Start()
    {
        StartCoroutine(spawnEndlessFireworks());
    }

    IEnumerator spawnEndlessFireworks()
    {
        while (true)
        {
            int randI = Random.Range(0, fireworksPrefabs.Length);
            Vector2 instancePosition = new Vector2(
                Random.Range(startPoint.position.x, endPoint.position.x),
                Random.Range(startPoint.position.y, endPoint.position.y));

            GameObject fireworkInstance = Instantiate(fireworksPrefabs[randI], instancePosition, Quaternion.identity);

            float randomInterval = Random.Range(interval, interval + intervalRand);
            yield return new WaitForSeconds(randomInterval);
        }
    }
}
