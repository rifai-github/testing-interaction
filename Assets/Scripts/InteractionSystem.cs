using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask interactableLayer;

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;
            Ray ray = mainCamera.ScreenPointToRay(touchPosition);
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, interactableLayer))
            {
                if (hit.collider.TryGetComponent<Interactable>(out var interactable))
                {
                    Debug.Log(hit.collider.gameObject.name + " - Touched");
                    interactable.OnInteract?.Invoke();
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = mainCamera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, interactableLayer))
            {
                if (hit.collider.TryGetComponent<Interactable>(out var interactable))
                {
                    Debug.Log(hit.collider.gameObject.name + " - Touched");
                    interactable.OnInteract?.Invoke();
                }
            }
        }
    }
}
