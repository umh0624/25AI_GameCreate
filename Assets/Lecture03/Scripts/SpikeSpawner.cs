using UnityEngine;
using System.Collections;

public class SpikeSpawner : MonoBehaviour
{
    public GameObject SpikePrefab;

    void Start()
    {
        StartCoroutine(SpawnSpikeRoutine());
    }

    IEnumerator SpawnSpikeRoutine()
    {
        while (true)
        {
            // 1초 ~ 4초 랜덤 대기 (네가 적용한 범위 유지)
            float waitTime = Random.Range(1f, 4f);
            yield return new WaitForSeconds(waitTime);

            // 스파이크 생성
            Debug.Log("Spawner : Spike 생성!");
            GameObject spike = Instantiate(SpikePrefab);
            spike.transform.position = transform.position;
        }
    }
}
