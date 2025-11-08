using UnityEngine;

public class Mouseposition : MonoBehaviour {
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask layerMask;

    private void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }
    private void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask))
        {
            transform.position = raycastHit.point;
        }
    }
}
