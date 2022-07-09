using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GrenadeMotion : MonoBehaviour
{
    public GameObject player;
    public GameObject aCamera;
    public GameObject Explosion;
    public GameObject part1;
    public GameObject part2;
    private Rigidbody rb;
    private AudioSource sound;
    private bool throwGrenade = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // component of grenade
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.G) && throwGrenade && part1.activeSelf) // throw grenade
        {
            Vector3 direction = aCamera.transform.forward;
            direction.y = 1;
            rb.AddForce(2f*direction, ForceMode.Impulse); 
            rb.useGravity = true;
            StartCoroutine(Explode());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Defender"))
            throwGrenade = true;
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(3f);
        Explosion.SetActive(true);
        part1.SetActive(false);
        part2.SetActive(false);
        sound.Play();

        // add explosion influence on other objects
        Collider[] objectsCollider = Physics.OverlapSphere(transform.position,20); // position of grenade and radius of 20

        for(int i = 0; i < objectsCollider.Length; i++)
        {
            Rigidbody r = objectsCollider[i].GetComponent<Rigidbody>();
            NavMeshAgent agent = objectsCollider[i].GetComponent<NavMeshAgent>();
            if (r != null) // it has rigidbody
            {
                r.AddExplosionForce(300, transform.position, 10);
                if(agent != null)
                {
                    agent.enabled = false;
                }
            }
        }
    }
}
