using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform targetPos, targetOG; // The central point to orbit around
    public Camera mainCamera; // Main Camera Object
    public float distance = 10.0f; // Distance from the target
    public float rotationSpeed = 100.0f; // Speed of rotation
    public float zoomSpeed = 10.0f; // Speed of zooming
    public float minDistance = 2.0f; // Minimum zoom distance
    public float maxDistance = 20.0f; // Maximum zoom distance

    public float fixedPitch = 30.0f; // Fixed pith to rotate around
    private float yaw = 0.0f; // Horizontal rotation

    private GameObject currentTile = null;

    void Start()
    {
        // Initialize the camera's position
        if (targetPos != null)
        {
            Vector3 direction = transform.position - targetPos.position;
            distance = direction.magnitude;
            yaw = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        }
    }

    void LateUpdate()
    {
        if (targetPos == null) return;

        if(Input.GetKeyDown(KeyCode.R))
        {
            targetPos = targetOG;

            currentTile.GetComponent<TileGlow>().SetSelected(false);
            currentTile = null;
        }

        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                TileGlow tile = hit.collider.GetComponent<TileGlow>();
                if (tile != null) // Assuming grid tiles are tagged
                {
                    if (currentTile != null)
                    {
                        currentTile.GetComponent<TileGlow>().SetSelected(false);
                    }

                    currentTile = tile.gameObject;
                    tile.SetSelected(true);
                    targetPos = hit.collider.transform;
                }
            }
        }

        // Get mouse input for rotation
        if (Input.GetMouseButton(1)) // Right mouse button
        {
            yaw += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        }

        // Get scroll input for zoom
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        // Calculate the new position and rotation of the camera
        Quaternion rotation = Quaternion.Euler(fixedPitch, yaw, 0);
        Vector3 offset = rotation * new Vector3(0, 0, -distance);
        transform.position = targetPos.position + offset;

        // Look at the target
        transform.LookAt(targetPos);
    }
}