using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Shooting : MonoBehaviour
{
    public GameObject player;
    public GameObject aCamera;
    public GameObject aTarget;
    public GameObject[] attackingEnemies = new GameObject[9], defendingEnemies = new GameObject[4];
    private Animator[] attackingAnimators = new Animator[9], defendingAnimators = new Animator[4];
    public GameObject gun;
    private AudioSource shootingSound;
    private LineRenderer line;
    public GameObject gunMuzzle;
    public ParticleSystem muzzleFlash; 

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < attackingAnimators.Length; i++)
            attackingAnimators[i] = attackingEnemies[i].GetComponent<Animator>(); 
        for (int i = 0; i < defendingAnimators.Length; i++)
            defendingAnimators[i] = defendingEnemies[i].GetComponent<Animator>();
        shootingSound = gun.GetComponent<AudioSource>();
        line = gun.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(shootingSound.isActiveAndEnabled)
            {
                shootingSound.Play();
                muzzleFlash.Play();
            }
            if(Physics.Raycast(aCamera.transform.position,aCamera.transform.forward,out hit))
            {
                aTarget.transform.position = hit.point;
                // draw shooting for a moment
                StartCoroutine(drawFlash());
                for(int i = 0; i < attackingEnemies.Length; i++)
                {
                    if (hit.transform.gameObject == attackingEnemies[i].transform.gameObject && player.CompareTag("Defender"))
                    {
                        NavMeshAgent agent = attackingEnemies[i].GetComponent<NavMeshAgent>();
                        //aTarget.transform.position = hit.point;
                        // stop enemy motion
                        agent.enabled = true;
                        agent.SetDestination(agent.transform.position);
                        agent.enabled = false;
                        attackingAnimators[i].SetInteger("Status", 2);
                    } 
                    else if(hit.transform.gameObject == attackingEnemies[i].transform.gameObject && player.CompareTag("Attacker"))
                    {
                        NavMeshAgent agent = defendingEnemies[i].GetComponent<NavMeshAgent>();
                        //aTarget.transform.position = hit.point;
                        // stop enemy motion
                        agent.enabled = true;
                        agent.SetDestination(agent.transform.position);
                        agent.enabled = false;
                        defendingAnimators[i].SetInteger("Status", 2);
                    }
                }
            }
        }
    }

    IEnumerator drawFlash()
    {
        // 1.draw shooting line
        line.SetPosition(0, gunMuzzle.transform.position);
        line.SetPosition(1, aTarget.transform.position);

        // 2.delay
        yield return new WaitForSeconds(0.1f);

        // 3.erase shooting line
        line.SetPosition(0, gunMuzzle.transform.position);
        line.SetPosition(1, gunMuzzle.transform.position);


    }
}
