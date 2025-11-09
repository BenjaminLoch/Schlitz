using System.Collections;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    private GameObject thisGameObj;
    private Collider shieldCol;

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
    
    private IEnumerator DestroyAfterPhysics()
    {
        yield return new WaitForFixedUpdate(); // Warte bis Physik-Update abgeschlossen ist
        Destroy(thisGameObj);
    }
}
