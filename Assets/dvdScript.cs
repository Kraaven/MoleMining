using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dvdScript : MonoBehaviour
{
    public RectTransform dvdLogo; // Reference to the RectTransform of the image
    public float speed = 200f; // Speed of the logo movement
    public float rotationSpeed = 100f; // Speed of the logo rotation

    private Vector2 direction; // Current direction of the logo's movement

    void Start()
    {
        // Start the logo moving in a random direction
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        transform.position = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    void Update()
    {
        // Move the logo
        dvdLogo.anchoredPosition += direction * speed * Time.deltaTime;

        // Rotate the logo
        dvdLogo.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        // Get the screen bounds in terms of the RectTransform
        Vector2 minBounds = new Vector2(dvdLogo.rect.width / 2, dvdLogo.rect.height / 2);
        Vector2 maxBounds = new Vector2(Screen.width, Screen.height) - minBounds;

        // Check for collision with screen bounds and reverse direction if necessary
        if (dvdLogo.anchoredPosition.x <= minBounds.x || dvdLogo.anchoredPosition.x >= maxBounds.x)
        {
            direction.x = -direction.x; // Reverse the horizontal direction
            ClampPosition(ref minBounds, ref maxBounds); // Clamp position to avoid getting stuck
        }

        if (dvdLogo.anchoredPosition.y <= minBounds.y || dvdLogo.anchoredPosition.y >= maxBounds.y)
        {
            direction.y = -direction.y; // Reverse the vertical direction
            ClampPosition(ref minBounds, ref maxBounds); // Clamp position to avoid getting stuck
        }
    }

    private void ClampPosition(ref Vector2 minBounds, ref Vector2 maxBounds)
    {
        // Ensure the logo stays within bounds
        dvdLogo.anchoredPosition = new Vector2(
            Mathf.Clamp(dvdLogo.anchoredPosition.x, minBounds.x, maxBounds.x),
            Mathf.Clamp(dvdLogo.anchoredPosition.y, minBounds.y, maxBounds.y)
        );
    }

}
