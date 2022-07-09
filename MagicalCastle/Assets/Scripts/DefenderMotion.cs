using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DefenderMotion : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;
    private Vector3 randomDestination;
    private bool isRandomNeeded = true;
    public GameObject target;
    private bool defending;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;
        animator.SetInteger("Status", 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(isRandomNeeded && agent.enabled)
        {
            float dx, dy, dz;
            dx = Random.Range(-250, 550);
            dy = 0;
            dz = Random.Range(-300, 500);

            randomDestination = new Vector3(dx, dy, dz);

            target.transform.position = randomDestination;

            agent.SetDestination(randomDestination);

            isRandomNeeded = false;
        }
        if(defending && agent.transform.position == target.transform.position)
        {
            defending = false;
            isRandomNeeded = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == target.gameObject.name && !defending) 
        { 
            isRandomNeeded = true; 
        }
        else if(other.CompareTag("Attacker"))
        {
            defending = true;
            target.transform.position = other.transform.position; // change the target to enemy position
            agent.SetDestination(target.transform.position);
        }
    }
}
