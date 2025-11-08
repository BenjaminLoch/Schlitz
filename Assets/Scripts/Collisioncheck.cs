using UnityEngine;

public class HitboxCheck : MonoBehaviour
{
    [Tooltip("Optional: Name oder Tag der anderen Kapsel")]
    public string targetName = "CapsuleB";

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{name}: Eintritt von {other.name}");

        if (other.name == targetName || other.CompareTag(targetName))
        {
            Debug.Log($"💥 {name} wurde von {targetName} getroffen und wird gelöscht!");
            Destroy(gameObject);   // <-- löscht Capsule A komplett aus der Szene
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Wird jedes Frame aufgerufen, solange beide überlappen
        // Debug.Log($"{name} hält Kontakt mit {other.name}");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"{name}: Kontakt mit {other.name} beendet");
    }
}
