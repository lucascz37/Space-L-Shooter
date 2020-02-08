using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy;
    void Start()
    {
        StartCoroutine("SpawnRoutine");
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            Instantiate(_enemy, new Vector3(Random.Range(-8f, 8f), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(5);
        }
    }
}
