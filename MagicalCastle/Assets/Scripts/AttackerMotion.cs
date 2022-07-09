using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackerMotion : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;
    public GameObject target;
    public GameObject attackersText;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        // start walking
        if (Input.GetKeyDown(KeyCode.Q))
        {
            agent.SetDestination(target.transform.position);
            animator.SetInteger("Status", 1);        
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            agent.SetDestination(agent.transform.position);
            animator.SetInteger("Status", 3);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            attackersText.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == target.gameObject.name)
        {
            attackersText.SetActive(true);
        }
    }

}
