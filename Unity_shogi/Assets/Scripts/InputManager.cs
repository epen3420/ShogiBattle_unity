using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Vector3 mouseStartPos;
    private Vector3 mouseEndPos;
    public Vector3 GetSwipeDistance;
    private Camera mainCamera;
    public bool MouseUp = false;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseStartPos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            mouseEndPos = Input.mousePosition;
            MouseUp = true;
        }
    }
    public Vector3 SwipeMouse()
    {
        Ray rayStart = mainCamera.ScreenPointToRay(mouseStartPos);
        Ray rayEnd = mainCamera.ScreenPointToRay(mouseEndPos);

        if (Physics.Raycast(rayStart, out RaycastHit hitStart) && Physics.Raycast(rayEnd, out RaycastHit hitEnd))
        {
            Vector3 worldStartPos = hitStart.point;
            Vector3 worldEndPos = hitEnd.point;
            GetSwipeDistance = worldEndPos - worldStartPos;
        }
        return GetSwipeDistance;
    }
}
