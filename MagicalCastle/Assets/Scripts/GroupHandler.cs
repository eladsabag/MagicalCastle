using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupHandler : MonoBehaviour
{
    public int group; // 1 - attacker, 0 - defender
    public GameObject attacker;
    public GameObject defender;
    public GameObject coinText;
    public GameObject[] coins = new GameObject[6];
    // Start is called before the first frame update
    void Start()
    {
        group = PlayerPrefs.GetInt("group", 0);
        if(group == 1)
        {
            defender.SetActive(false);
            attacker.SetActive(true);
            coinText.SetActive(false);
            for(int i = 0; i < coins.Length; i++)
            {
                coins[i].SetActive(false);
            }
        }
    }

}
