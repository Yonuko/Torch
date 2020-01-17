using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{

    public GameObject NPCPrefab;
    public int countOfNPC;
    public Transform Spawner;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < countOfNPC; i++)
        {
            GameObject go = Instantiate(NPCPrefab, Spawner.position, Spawner.rotation);
            go.GetComponent<AIPathFinding>().speed = Random.Range(1, 3);
         //   go.GetComponent<AIPathFinding>().speed = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
