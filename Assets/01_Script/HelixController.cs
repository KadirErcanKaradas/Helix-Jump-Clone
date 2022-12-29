using System.Collections;
using UnityEngine;

public class HelixController : MonoBehaviour
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
                    StartCoroutine(HideHelix());
                }
                else
                {
                    interactable.Interact();
                    StartCoroutine(SplahParticle(collision));
                    isFinish = false;
                }
            }
            else if(obstacle)
            {
                if (manager.comboCount >= 3)
                {
                    StartCoroutine(HideHelix());
                    isFinish = true;
                }
                else if(!isFinish)
                {
                    manager.SetGameStage(GameStage.Fail);
                    GameEvent.Fail();
                }
            }
        }
    }
    
    private IEnumerator HideHelix()
    {
        manager.comboCount = 0;
        Transform parent = transform.parent;
        for (int i = 0; i < parent.childCount; i++)
        {
            parent.GetChild(i).gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < parent.childCount; i++)
        {
            parent.GetChild(i).gameObject.GetComponent<MeshCollider>().enabled = false;
        }
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < parent.childCount; i++)
        {
            parent.GetChild(i).gameObject.SetActive(false);
        }
    }
    private IEnumerator SplahParticle(Collision collision)
    {
        GameObject obj = pool.GetPooledObject(0);
        Vector3 pos = collision.gameObject.transform.position;
        obj.transform.position = new Vector3(pos.x, pos.y-0.10f, pos.z);
        obj.transform.rotation = Quaternion.Euler(-90, 0, 0);
        obj.transform.parent = transform;
        manager.comboCount = 0;
        obj.SetActive(true);
        yield return new WaitForSeconds(3f);
        obj.SetActive(false);
    }
}
