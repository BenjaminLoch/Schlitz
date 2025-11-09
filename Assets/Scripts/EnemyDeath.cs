using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDeath : MonoBehaviour
{
    public bool isDead = false;
    private GameObject thisGameObj;
    private Collider shieldCol;
    [SerializeField] private UnityEvent countEnemydown;

    void Awake()
    {
        thisGameObj = gameObject;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.name != "Player") return;

        if (thisGameObj.name == "ShieldEnemy")
        {
            Transform childTransform = thisGameObj.transform.Find("Cube");
            shieldCol = childTransform.GetComponent<Collider>();
            Physics.IgnoreCollision(other, shieldCol, true);
        }
        StartCoroutine(DestroyAfterPhysics());
    }
    
    private IEnumerator DestroyAfterFrame()
    {
        yield return new WaitForFixedUpdate(); // Warte bis Physik-Update abgeschlossen ist
        Destroy(thisGameObj);
    }
}
