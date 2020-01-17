using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPathFinding : MonoBehaviour
{

    public List<Transform> destinationList;
    public int speed = 2;

    NavMeshAgent agent;
    int waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(1, 3);

        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        if (destinationList[waypointIndex] == null)
        {
            return;
        }
        agent.SetDestination(destinationList[waypointIndex].position);
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < agent.stoppingDistance)
        {
            waypointIndex++;
            if (waypointIndex >= destinationList.Count)
            {
                return;
            }
            agent.SetDestination(destinationList[waypointIndex].position);
        }
    }
}
