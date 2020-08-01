using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 0.5f;
    public Transform interactionTransform;
    Transform player;
    bool isFocus = false;
    bool hasInteracted = false;
    public virtual void Interact()
    {
        Debug.Log("interact with" + interactionTransform.name);
    }
    public void Update()
    {
        if(isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius)
            hasInteracted = true;
            Interact();
        }
    }
    private void OnDrawGizmos()
    {
        if(interactionTransform == null)
        {
            interactionTransform = transform;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(interactionTransform.position, radius);
    }
    public void OnFocus(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }
}
