using UnityEngine;

public class AnimationStachelnhochundrunter : MonoBehaviour
{
    private Animator anim;
    private bool isUp = false;
    private Collider hitbox;

    void Start()
    {
        anim = GetComponent<Animator>();
        hitbox = GetComponent<Collider>();
        hitbox.enabled = false; // Start deaktiviert
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isUp = !isUp;
            anim.SetBool("isUp", isUp);
            hitbox.enabled = isUp; // Hitbox nur aktiv, wenn Stacheln oben sind
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hitbox.enabled && other.CompareTag("Player"))
        {
            PlayerDeath player = other.GetComponent<PlayerDeath>();
            if (player != null)
            {
                player.Die();
            }
        }
    }
}
