using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Packsack : Inventory
{
    private static Packsack instance;
    public static Packsack Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.Find("Canvas").transform.Find("Packsack").GetComponent<Packsack>();
            }
            return instance;
        }
    }
}
