using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPathFinding : MonoBehaviour
{

    public Transform destination;
    public int speed = 2;

    NavMeshAgent agent;
    bool test = false;

    // Start is called before the first frame update
    void Start()
    {
        destination = GameObject.Find("Pedestal LOD Test").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.SetDestination(destination.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < agent.stoppingDistance && !test)
        {
            test = true;
            Debug.Log("Salut " + transform.name);
            transform.Translate(new Vector3(0, 0, 10));
        }
    }
}
