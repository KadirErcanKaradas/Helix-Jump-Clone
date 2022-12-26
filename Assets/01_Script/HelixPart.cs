using System.Collections;
using UnityEngine;

public class HelixPart : MonoBehaviour
{
    private GameManager manager;
    private ObjectPool pool;
    public bool obstacle = false;
    public bool isFinish = false;
    [SerializeField] private Material obstacleMaterial;

    private void Start()
    {
        pool = ObjectPool.Instance;
        manager = GameManager.Instance;
        if (obstacle)
            GetComponent<MeshRenderer>().material.color = obstacleMaterial.color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var interactable = collision.gameObject.GetComponent<IInteractable>();
        if (interactable != null && manager.GameStage == GameStage.Started)
        {
            if (!obstacle)
            {
                if (manager.comboCount >= 3)
                {
                    HideHelix();
                }
                else
                {
                    interactable.Interact();
                    SplahParticle(collision);
                }
            }
            else if(obstacle)
            {
                if (manager.comboCount >= 3)
                {
                 HideHelix();   
                }
                else
                {
                    manager.SetGameStage(GameStage.Fail);
                    GameEvent.Fail();
                }
            }
        }
    }

    private void HideHelix()
    {
        Transform parent = transform.parent;
        for (int i = 0; i < parent.childCount; i++)
        {
            parent.GetChild(i).gameObject.SetActive(false);
        }
        manager.comboCount = 0;
    }
    private void SplahParticle(Collision collision)
    {
        GameObject obj = pool.GetPooledObject(0);
        Vector3 pos = collision.gameObject.transform.position;
        obj.transform.position = new Vector3(pos.x, pos.y-0.19f, pos.z);
        obj.transform.parent = transform;
        manager.comboCount = 0;
        obj.SetActive(true);
    }
}
