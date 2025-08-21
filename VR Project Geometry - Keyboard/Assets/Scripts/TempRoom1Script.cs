using UnityEngine;

public class BookChangeBlindnessTrigger : MonoBehaviour
{
    public GameObject promptUI;

    public GameObject wallToCoverOldDoor;
    public GameObject actualDoorToDisable;  // NEW: actual door object to hide
    public GameObject wallToUncoverNewDoor;
    public GameObject newRoom;

    // Optional: reference to this book object to disable
    public GameObject bookObjectToHide; // could be 'this.gameObject' or a mesh child

    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (wallToCoverOldDoor) wallToCoverOldDoor.SetActive(true);
            if (actualDoorToDisable) actualDoorToDisable.SetActive(false); // Hide door
            if (wallToUncoverNewDoor) wallToUncoverNewDoor.SetActive(false);
            if (newRoom) newRoom.SetActive(true);
            if (promptUI) promptUI.SetActive(false);

            if (bookObjectToHide) bookObjectToHide.SetActive(false); // "Pick up" the book

            enabled = false; // Prevent re-trigger
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (promptUI) promptUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (promptUI) promptUI.SetActive(false);
        }
    }
}
