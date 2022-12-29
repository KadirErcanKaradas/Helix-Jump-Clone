using System;
using DG.Tweening;
using UnityEngine;

public class BallController : MonoBehaviour,IInteractable
{

    private Rigidbody rb;
    private GameManager manager;
    [SerializeField] private float jumpPower = 5f;
    [SerializeField] private GameObject partFire;
    [SerializeField] private GameObject partBlood;

    private void Start()
    {
        manager = GameManager.Instance;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (manager.GameStage == GameStage.Started)
            rb.isKinematic = false;
        if (manager.comboCount >= 3)
            partFire.SetActive(true);
        else
            partFire.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var interactable = other.GetComponent<IHelix>();
        if (interactable != null)
        {
            interactable.Interact();
        }

        if (other.CompareTag("Finish"))
        {
            manager.SetGameStage(GameStage.Win);
            manager.comboCount = 0;
            GameEvent.Win();
        }
    }

    public void Interact()
    {
        if (manager.GameStage == GameStage.Started)
        {
            rb.velocity = new Vector3(0, jumpPower, 0);
            partBlood.SetActive(true);
            manager.isFallen = false;
        }
    }
}
