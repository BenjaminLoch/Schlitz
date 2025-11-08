using UnityEngine;

public class DirectionalWeakSpotBlocker : MonoBehaviour
{
    [Header("Nur Treffer von hinten erlauben")]
    [Tooltip("Kegelweite (Grad) für die Hinterseite. 60° = streng, 90° = großzügiger.")]
    public float rearConeAngle = 70f;

    [Tooltip("Optional: Nur Angriffe dieses Tags berücksichtigen (leer = alle).")]
    public string attackerTag = ""; // z.B. "CapsuleB"

    [Header("Block-Verhalten (kein Bounce)")]
    [Tooltip("Kleiner Sicherheitsabstand beim Entklemmen.")]
    public float unpenetrateEpsilon = 0.01f;

    private bool IsFromBehind(Transform attacker)
    {
        // Richtung A->Angreifer
        Vector3 toAttacker = (attacker.position - transform.position).normalized;
        // Winkel zwischen A.forward (Vorderseite) und Richtung zum Angreifer
        float angle = Vector3.Angle(transform.forward, toAttacker);
        // Von hinten = Winkel nahe 180°
        return angle > (180f - rearConeAngle);
    }

    private void OnCollisionEnter(Collision collision)
    {
        TryResolve(collision, destroyIfBehind:true);
    }

    private void OnCollisionStay(Collision collision)
    {
        // fortlaufend blockieren, falls vorn/seitlich gedrückt wird
        TryResolve(collision, destroyIfBehind:false);
    }

    private void TryResolve(Collision collision, bool destroyIfBehind)
    {
        var otherCol = collision.collider;
        var rb = otherCol.attachedRigidbody;
        if (rb == null) return;

        if (!string.IsNullOrEmpty(attackerTag) && !otherCol.CompareTag(attackerTag))
            return;

        if (IsFromBehind(otherCol.transform))
        {
            if (destroyIfBehind)
            {
                Debug.Log($"✅ Weakspot-Hit von hinten durch {otherCol.name}. {name} wird zerstört.");
                Destroy(gameObject);
            }
            return;
        }

        // Vorn/seitlich: BLOCKIEREN (kein Bounce)
        // 1) Geschwindigkeit stoppen
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // 2) Leicht vom Collider wegschieben (Entklemmen) entlang der mittleren Kollisions-Normalen
        Vector3 avgNormal = Vector3.zero;
        float sep = 0f;
        int cnt = Mathf.Min(collision.contactCount, 4);
        for (int i = 0; i < cnt; i++)
        {
            var c = collision.GetContact(i);
            avgNormal += c.normal;
            sep += c.separation; // negativ bei Durchdringung
        }
        if (cnt > 0)
        {
            avgNormal.Normalize();
            float avgSep = sep / cnt; // meist <= 0
            float fix = Mathf.Max(unpenetrateEpsilon, -avgSep + unpenetrateEpsilon);
            rb.position += avgNormal * fix;
        }

        // 3) Optional: Wenn B einen eigenen Mover hat, kannst du ihn hier „blocken“
        // var mover = rb.GetComponent<YourMover>();
        // if (mover) mover.BlockOnceAlong(avgNormal);
    }

    // Gizmo: zeigt grob die Rückseite
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 back = -transform.forward;
        Gizmos.DrawRay(transform.position, back * 1.0f);
    }
}
