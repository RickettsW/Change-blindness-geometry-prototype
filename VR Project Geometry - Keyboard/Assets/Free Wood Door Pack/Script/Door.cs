using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DoorScript
{
    [RequireComponent(typeof(AudioSource))]
    public class Door : MonoBehaviour
    {
        public bool open = false;
        public float smooth = 2f;
        public float openAngle = 90f;
        public float closedAngle = 0f;

        public AudioClip openSound, closeSound;

        public GameObject pathToMove;              // The object to move (set this in the Inspector)
        public Vector3 shiftAmount;                // How much to move the path when the door opens
        private bool pathMoved = false;            // Prevents moving the path multiple times

        private AudioSource audioSource;
        private Collider doorCollider;
        private float targetAngle;

        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            doorCollider = GetComponent<Collider>();
            targetAngle = closedAngle;
        }

        void Update()
        {
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * smooth);
        }

        public void OpenDoor()
        {
            open = !open;
            targetAngle = open ? openAngle : closedAngle;

            if (audioSource != null)
                audioSource.PlayOneShot(open ? openSound : closeSound);

            if (doorCollider != null)
                doorCollider.enabled = !open;

            // Move the path if door is opening and hasn't moved path yet
            if (open && !pathMoved && pathToMove != null)
            {
                Debug.Log("Moving path " + pathToMove.name + " by " + shiftAmount);
                pathToMove.transform.position += shiftAmount;
                pathMoved = true;
            }
        }
    }
}
