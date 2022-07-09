using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // for Text 

public class DoorMotion : MonoBehaviour
{
    public Text doorText;
    public GameObject Key;
    private Animator animator;
    private AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(!Key.activeSelf)
        {
            animator.SetBool("DoorIsOpen", true);
            sound.PlayDelayed(0.7f);
        } 
        else
        {
            if(!doorText.IsActive() && other.gameObject.name == "DefendingPlayer")
                doorText.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(!Key.activeSelf)
        {
            animator.SetBool("DoorIsOpen", false);
            //sound.Play();
            sound.PlayDelayed(0.7f);
        } 
        else
        {
            if (doorText.IsActive() && other.gameObject.name == "DefendingPlayer")
                doorText.gameObject.SetActive(false);
        }
    }
}
