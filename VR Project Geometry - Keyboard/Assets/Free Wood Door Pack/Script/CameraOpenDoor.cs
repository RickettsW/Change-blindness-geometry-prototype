using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraDoorScript
{
    public class CameraOpenDoor : MonoBehaviour
    {
        public float DistanceOpen = 3f;
        public GameObject text; // Drag your UI text object here

        void Update()
        {
            Debug.DrawRay(transform.position, transform.forward * DistanceOpen, Color.red);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, DistanceOpen))
            {
                Debug.Log("Ray hit: " + hit.transform.name);

                DoorScript.Door door = hit.transform.GetComponent<DoorScript.Door>();
                if (door == null)
                    door = hit.transform.GetComponentInParent<DoorScript.Door>();

                if (door != null)
                {
                    if (text != null)
                        text.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.E))
                        door.OpenDoor();
                }
                else
                {
                    if (text != null)
                        text.SetActive(false);
                }
            }
            else
            {
                if (text != null)
                    text.SetActive(false);
            }
        }
    }
}
