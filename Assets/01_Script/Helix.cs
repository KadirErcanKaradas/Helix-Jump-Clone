using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helix : MonoBehaviour,IHelix
{
    private GameManager manager;
    private void Start()
    {
        manager = GameManager.Instance;
    }
    
    public void Interact()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        manager.comboCount++;
        GameEvent.Score();
    }
}
