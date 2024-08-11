using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ChestInteraction : MonoBehaviour
{
    [TagSelector] [SerializeField] private string player;
    private bool playerCanInteract = false;
    private TextMeshProUGUI interactText;
    [SerializeField] private GameObject medicineSpawn;
    private GameObject thisItem;
    


    private void Start()
    {
        interactText = GameObject.Find("InteractionText").GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(player))
        {
            playerCanInteract = true;
            Debug.Log("Feeling The Player");
        }
    }
    
    private void Update()
    {
        if (playerCanInteract)
        {
            InteractionActivated(playerCanInteract);
        }
    }

    private void InteractionActivated(bool playerTouch)
    {
        interactText.text = "Press E to open chest";
        if (playerTouch)
        {
            if (thisItem == null)
            {
                interactText.GetComponent<CanvasGroup>().alpha = 1;
            }
            else
            {
                interactText.GetComponent<CanvasGroup>().alpha = 0;
            }
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(ThrowMeds());
            }
        }
    }

    private IEnumerator ThrowMeds()
    {
        Debug.Log("Interacting with chest");
        thisItem = Instantiate(medicineSpawn, Vector3.up, Quaternion.identity);
        Rigidbody2D rb = thisItem.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * 1, ForceMode2D.Impulse);
        rb.gravityScale = 50;
        Destroy(this.gameObject, 0.1f);
        yield return null;
    }
}
