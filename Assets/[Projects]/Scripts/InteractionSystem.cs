using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask interactableLayer;

    private void Update()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            var touchPosition = Input.GetTouch(0).position;
            ProcessInteraction(touchPosition);
        }
#else
        if (Input.GetMouseButtonDown(0))
        {
            var touchPosition = Input.mousePosition;
            ProcessInteraction(touchPosition);
        }
#endif
    }

    private void ProcessInteraction(Vector2 screenPosition)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, interactableLayer))
        {
            if (hit.collider.TryGetComponent<Interactable>(out var interactable))
            {
                Debug.Log($"Interacted with: {hit.collider.gameObject.name}");
                interactable.OnInteract?.Invoke();
            }
        }
    }
}
