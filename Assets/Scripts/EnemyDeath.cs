using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDeath : MonoBehaviour
{
    public bool isDead = false;
    private GameObject thisGameObj;
    private Collider shieldCol;
    [SerializeField] private UnityEvent countEnemyDown;

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
            shieldCol.enabled = false;
        }
        if (thisGameObj.name == "BatteryEnemy")
        {
            PlayerDash otherScript = other.gameObject.GetComponent<PlayerDash>();
            otherScript.dashCount++;
        }
        isDead = true;
        Destroy(thisGameObj);
        //StartCoroutine(DestroyAfterFrame());
    }

    private IEnumerator DestroyAfterFrame()
    {
        yield return null;              // warte bis NÄCHSTER Frame (nicht FixedUpdate)
        yield return new WaitForFixedUpdate(); // +1 Physik-Frame
        Destroy(thisGameObj);
    }
}