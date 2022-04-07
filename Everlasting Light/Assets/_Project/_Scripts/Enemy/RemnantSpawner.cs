using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemnantSpawner : MonoBehaviour
{
    [SerializeField] int maxRemnant = 5;
    [SerializeField] GameObject[] _enemyPrefabs = new GameObject[2];

    private List<Remnant> remnantToSpawn = new List<Remnant>();

    public void SpawnRemnant()
    {
        //Debug.Log(remnantToSpawn.Count);
        foreach (var remnant in remnantToSpawn)
        {
            Instantiate(remnant.RemnantPrefabs, remnant.RemnantPosition, Quaternion.identity, gameObject.transform);
        }
    }

    public void AddRemnant(int index, Vector3 position)
    {
        //Debug.Log("RemnantAdded");
        var playerRemnant = new Remnant { RemnantPrefabs = _enemyPrefabs[index], RemnantPosition = position };
        remnantToSpawn.Add(playerRemnant);
        if(remnantToSpawn.Count >= maxRemnant) { remnantToSpawn.RemoveAt(0); }
    }
}

public class Remnant
{
    public GameObject RemnantPrefabs { get; set; }
    public Vector3 RemnantPosition { get; set; }
}

