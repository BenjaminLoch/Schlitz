using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private Animator anim;
    public bool isUp = false;
    private Collider hitbox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        hitbox = GetComponent<Collider>();
        hitbox.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isUp = !isUp;
            anim.SetBool("isUp", isUp);
            hitbox.enabled = isUp; 
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (hitbox.enabled && other.CompareTag("Player")) {
            PlayerDash player = other.GetComponent<PlayerDash>();
            if (player != null)
            {
                Debug.Log("Spieler soll sterben");
                player.Die();
            }
        }
    }
}
