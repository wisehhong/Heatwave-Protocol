using UnityEngine;

public class GameManager : MonoBehaviour
{    
    // Update is called once per frame
    void Update()
    {
        // Input handling
        // Left mouse click
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.name);
            }
        }        
    }
}
