using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject roomHolder;
    public float roomYOffset = 6f; // Amount to move each group down

    void Start()
    {
        SwapAndShiftRooms();
    }

    public void SwapAndShiftRooms()
    {
        if (roomHolder == null)
        {
            Debug.LogError("RoomHolder not assigned.");
            return;
        }

        Transform room1 = roomHolder.transform.Find("1");
        Transform room2 = roomHolder.transform.Find("2");
        Transform room3 = roomHolder.transform.Find("3");
        Transform room4 = roomHolder.transform.Find("4");

        if (room1 == null)
        {
            Debug.LogError("Room 1 not found under RoomHolder.");
            return;
        }

        // 1. Disable room 1-3
        Transform room13 = room1.Find("room 1-3");
        if (room13 != null)
            room13.gameObject.SetActive(false);

        // 2. Swap positions of room 1-1 and room 1-2 with room 1-3
        Transform room11 = room1.Find("room 1-1");
        Transform room12 = room1.Find("room 1-2");

        if (room11 != null && room12 != null && room13 != null)
        {
            Vector3 room13Pos = room13.position;
            Vector3 avgPos = (room11.position + room12.position) / 2f;

            // Swap room 1-1 and 1-2 to room13's position (average to keep them grouped)
            Vector3 offset11 = room11.position - avgPos;
            Vector3 offset12 = room12.position - avgPos;

            room11.position = room13Pos + offset11;
            room12.position = room13Pos + offset12;

            // Move room13 to the original avg position of 1-1 and 1-2
            room13.position = avgPos;
        }

        // 3. Move rooms 2, 3, 4 downward
        MoveRoomDown(room2);
        MoveRoomDown(room3);
        MoveRoomDown(room4);
    }

    void MoveRoomDown(Transform room)
    {
        if (room == null) return;
        room.position -= new Vector3(0f, roomYOffset, 0f);
    }
}
