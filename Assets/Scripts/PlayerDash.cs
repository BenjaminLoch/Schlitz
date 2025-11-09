using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private Transform mousePos;
    public float dashCount = 7f;
    [SerializeField] private float dashSpeed = 45f;
    [SerializeField] private float dashTime = 0.15f;
    [SerializeField] private float bounceFactor = 0.6f;
    private Transform objPos;
    private Rigidbody thisRB;
    private GameObject thisGameObj;
    private Vector3 dashVektor;
    private float dashTimer;
    private bool isDashing = false;

    private void Start()
    {
        thisRB = GetComponent<Rigidbody>();
        thisRB.collisionDetectionMode = CollisionDetectionMode.Continuous;
        thisGameObj = gameObject;
        Transform child = transform.Find("ObjRefPoint");
        objPos = child;
    }
    void Update()
    {
        if (thisGameObj == null)
        return;
        Vector3 guckVek = (mousePos.position - objPos.position).normalized;
        transform.forward = guckVek;
        if (Input.GetMouseButtonDown(0) && !isDashing)
        {
            dashVektor = (mousePos.position - objPos.position).normalized;
            isDashing = true;
            dashTimer = dashTime;
            thisRB.AddForce(dashVektor * dashSpeed, ForceMode.VelocityChange);
        }
    }

    void FixedUpdate()
    {
        if (!isDashing) return;

        dashTimer -= Time.fixedDeltaTime;
        if (dashTimer <= 0f)
        {
            thisRB.linearVelocity = Vector3.zero;
            dashCount--;
            Debug.Log(dashCount);
            isDashing = false;
            if (dashCount == 0)
            {
                Die();
            }
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (!isDashing) return;

        EnemyDeath deathScript = collision.gameObject.GetComponentInParent<EnemyDeath>();
        if (deathScript != null && deathScript.isDead)
        {
            Debug.Log("Kollision mit totem Gegner ignoriert");
            return;
        }
    
        Debug.Log(collision.gameObject.name);
        Vector3 reflect = Vector3.Reflect(dashVektor, collision.contacts[0].normal);
        dashVektor = reflect.normalized;
        thisRB.linearVelocity = dashVektor * dashSpeed * bounceFactor;
    }
    
    private void Die()
    {
        Debug.Log("Spieler ist tot!");
        Destroy(thisGameObj); //Spieler wird zerstoert
        return;
    }
}
