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
        StartCoroutine(HideHelix());
        manager.comboCount++;
        GameEvent.Score();
        manager.isFallen = true;
    }

    private IEnumerator HideHelix()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<MeshCollider>().enabled = false;
        }
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
