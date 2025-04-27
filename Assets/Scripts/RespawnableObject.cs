using System.Collections;
using UnityEngine;

public class RespawnableObject : MonoBehaviour
{
    private bool isDestroyed = false;

    private void OnMouseDown()
    {
        if (!isDestroyed)
        {
            StartCoroutine(DestroyAndRespawn());
        }
    }

    private IEnumerator DestroyAndRespawn()
    {
        isDestroyed = true;
        gameObject.SetActive(false); 

        Debug.Log("Respawning object false");

        yield return new WaitForSeconds(5f); 

        Debug.Log("Respawning object");

        gameObject.SetActive(true); 
        isDestroyed = false;
    }
}
