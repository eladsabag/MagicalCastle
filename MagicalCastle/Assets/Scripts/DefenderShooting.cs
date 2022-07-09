using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DefenderShooting : MonoBehaviour
{
    public GameObject[] attackingEnemies = new GameObject[9];
    private Animator[] attackingAnimators = new Animator[9];
    private bool startShooting = false;
    public GameObject defender;
    public GameObject gun;
    private AudioSource shootingSound;
    private LineRenderer line;
    public GameObject gunMuzzle;
    public ParticleSystem muzzleFlash;
    private int counter = 0;
    public GameObject enemyDownText;
    public GameObject defendersText;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < attackingAnimators.Length; i++)
            attackingAnimators[i] = attackingEnemies[i].GetComponent<Animator>();
        shootingSound = gun.GetComponent<AudioSource>();
        line = gun.GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if(startShooting && defender.GetComponent<NavMeshAgent>().enabled && counter < 5)
        {
            counter++;
            startShooting = false;
            Invoke(nameof(Shoot), 3f);
            
        }
        int checkIfAllDead = 0;
        for (int i = 0; i < attackingEnemies.Length; i++)
        {
            if(!attackingEnemies[i].GetComponent<NavMeshAgent>().enabled) { checkIfAllDead++; }
        }
        if(checkIfAllDead == attackingEnemies.Length - 1)
        {
            defendersText.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Attacker"))
        {
            counter = 0;
            startShooting = true;
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        startShooting = false;
    }

    private void Shoot()
    {
        RaycastHit hit;
        if (shootingSound.isActiveAndEnabled)
        {
            shootingSound.Play();
            muzzleFlash.Play();
        }
        if (Physics.Raycast(defender.transform.position, defender.transform.forward, out hit))
        {
            for (int i = 0; i < attackingEnemies.Length; i++)
            {
                if (hit.transform.gameObject == attackingEnemies[i].transform.gameObject)
                {
                    NavMeshAgent agent = attackingEnemies[i].GetComponent<NavMeshAgent>();
                    // stop enemy motion
                    agent.enabled = true;
                    agent.SetDestination(agent.transform.position);
                    attackingAnimators[i].SetInteger("Status", 2);
                    agent.enabled = false;

                    StartCoroutine(DisplayEnemyDownText());

                    return;
                }
            }
        }
        startShooting = true;
    }

    IEnumerator DisplayEnemyDownText()
    {
        enemyDownText.SetActive(true);
        yield return new WaitForSeconds(2f);
        enemyDownText.SetActive(false);
    }
}
