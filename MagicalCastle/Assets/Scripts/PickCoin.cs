using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // for Text 

public class PickCoin : MonoBehaviour
{
    public AudioSource pickSound; // need to be connected in UNITY
    public Text coinsText;
    static int pickedCoins = 0;
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
        if(other.CompareTag("Defender"))
        {
            this.gameObject.SetActive(false);
            pickSound.Play();
            pickedCoins++;
            coinsText.text = "Gold Coins: " + pickedCoins;
        }
    }
}
