using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private Transform mousePos;
    [SerializeField] private Transform objPos;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;

    private void Update()
    {
        Vector3 guckRichtung = (mousePos.position - objPos.position).normalized;
        transform.forward = guckRichtung;
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Dash());
        }
    }

    private System.Collections.IEnumerator Dash()
    {
        Vector3 moveVektor = (mousePos.position - objPos.position).normalized;
        rb.linearVelocity = dashSpeed * moveVektor;
        yield return new WaitForSeconds(dashTime);

        rb.linearVelocity = Vector3.zero;
    }
}
