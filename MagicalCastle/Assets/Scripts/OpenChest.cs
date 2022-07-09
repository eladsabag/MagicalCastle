using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenChest : MonoBehaviour
{
    public GameObject ClosedChest;
    public GameObject OpenedChest;
    public GameObject Key;
    public GameObject aCamera;
    public GameObject SeeThroughCrossHair;
    public GameObject TouchCrossHair;
    public GameObject Chest;
    public Text ChestText,doorText;
    private bool chestClosed = true;
    private AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(aCamera.transform.position, aCamera.transform.forward, out hit))
        {
            // THIS is the chest. So we want check if the hit object is the chest.
            if (hit.transform.gameObject == this.gameObject || hit.transform.gameObject == Chest.gameObject || hit.transform.gameObject == Key.gameObject)
            {
                // change crosshair
                if (!TouchCrossHair.activeSelf)
                {
                    SeeThroughCrossHair.SetActive(false);
                    TouchCrossHair.SetActive(true);
                }
            }
            else
            {
                // change crosshair
                if (TouchCrossHair.activeSelf)
                { 
                    TouchCrossHair.SetActive(false);
                    SeeThroughCrossHair.SetActive(true);
                }
            }
            // check if we hit the chest
            if (hit.transform.gameObject == Chest.gameObject)
            {
                if (!ChestText.IsActive())
                {
                    ChestText.gameObject.SetActive(true);
                }
                StartCoroutine(ChestOpenClose());

            } 
            else if(hit.transform.gameObject == Key.gameObject)
            {
                ChestText.text = "Press [E] to Pick up Key";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    sound.Play();
                    Key.gameObject.SetActive(false);
                }
            }
            else
            {
                if (ChestText.IsActive())
                {
                    ChestText.gameObject.SetActive(false);
                }
            }
        }

        // change text only after animations played
        IEnumerator ChestOpenClose()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                sound.Play();
                OpenedChest.SetActive(chestClosed);
                chestClosed = !chestClosed;
                ClosedChest.SetActive(chestClosed);
            }

            yield return new WaitForSeconds(2);

            if (chestClosed)
                ChestText.text = "Press [SPACE] To OPEN";
            else
                ChestText.text = "Press [SPACE] To CLOSE";

        }
    }
}
