using UnityEngine;

public class LastDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject interactableUI;
    [SerializeField] private InteractUI interactUI;
    [SerializeField] private ItemData needItem;
    private bool isInteractable;

    public void Interact()
    {
        interactableUI.SetActive(false);
        interactUI.gameObject.SetActive(true);
        interactUI.SetupInteractUI(needItem);
    }

    private void Update()
    {
        if (isInteractable && Input.GetKeyDown(KeyCode.T))
            Interact();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collector"))
        {
            interactableUI.SetActive(true);
            isInteractable = true;
        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Collector"))
        {
            interactableUI.SetActive(false);
            isInteractable = false;
        }
    }
}
