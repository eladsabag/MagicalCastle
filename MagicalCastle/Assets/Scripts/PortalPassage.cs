using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalPassage : MonoBehaviour
{
    public Animator animator;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == player.gameObject.name)
        {
            StartCoroutine(PlayDelay());
        }
    }

    IEnumerator PlayDelay()
    {
        int numScene;
        // play fade in animation
        animator.SetTrigger("StartFadeIn");
        // delay
        yield return new WaitForSeconds(1);
        numScene = SceneManager.GetActiveScene().buildIndex;
        
        SceneManager.LoadScene((numScene+1)%2);
    }
}
