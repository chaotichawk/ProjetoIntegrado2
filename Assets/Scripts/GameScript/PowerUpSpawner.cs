using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject PowerUp;
    public List<GameObject> PowerUpStorage = new List<GameObject>();

    void Start()
    {
        InvokeRepeating("SpawnPowerUp", 0.1f, 1.0f);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            Debug.Log(PowerUpStorage.Count);
        }
    }

    private void SpawnPowerUp()
    {
        GameObject g;
        g = Instantiate(PowerUp, new Vector3(Random.Range(-64, 64), Random.Range(-64, 64), 0), Quaternion.identity);
        PowerUpStorage.Add(g);
    }
}
