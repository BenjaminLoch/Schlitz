using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private Transform mousePos;
    private Transform objPos;
    private Rigidbody thisRB;
    private GameObject thisGameObj;
    [SerializeField] private float dashCount = 7f;
    [SerializeField] private float dashSpeed = 45f;
    [SerializeField] private float dashTime = 0.15f;

    private void Start()
    {
        thisRB = GetComponent<Rigidbody>();
        thisGameObj = gameObject;
        Transform child = transform.Find("ObjRefPoint");
        objPos = child;
    }
    private void Update()
    {
        Vector3 guckRichtung = (mousePos.position - objPos.position).normalized;
        transform.forward = guckRichtung;
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Dash());
        }
    }

    private void Die()
    {
        Debug.Log("Spieler ist tot!");
        Destroy(thisGameObj); //Spieler wird zerstoert
    }
    private System.Collections.IEnumerator Dash()
    {
        Vector3 moveVektor = (mousePos.position - objPos.position).normalized;
        thisRB.linearVelocity = dashSpeed * moveVektor;
        yield return new WaitForSeconds(dashTime);

        thisRB.linearVelocity = Vector3.zero;
        dashCount--;
        Debug.Log("Dash uebrig: " + dashCount); //Aktuellen dash Wert in Konsole
        if (dashCount == 0)
        {
            Die(); //Spieler stirbt
        }
    }
}
