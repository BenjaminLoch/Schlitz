using UnityEngine;

public class ShieldEnemy : MonoBehaviour
{
    [SerializeField] private Transform playerPos;
    private Transform objPos;
    void Start()
    {
        objPos = GetComponent<Transform>();
    }

    void Update()
    {
        Vector3 faceenemyvek = (playerPos.position - objPos.position).normalized;
        objPos.rotation = Quaternion.LookRotation(faceenemyvek);
    }
}
