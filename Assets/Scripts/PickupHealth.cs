using System;
using TMPro;
using UnityEngine;
namespace ViralDecay
{
    public class PickupHealth : MonoBehaviour
    {
        [TagSelector][SerializeField] private string medsTag;
        private bool playerCanInteract;
        [SerializeField] private TextMeshProUGUI interactText;
        private BodyChanging bodyChanging;
        private GameObject thisMed;

        private void Awake()
        {
            bodyChanging = FindObjectOfType<BodyChanging>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(medsTag))
            {
                thisMed = other.gameObject;
                playerCanInteract = true;
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
            if (playerTouch)
            {
                interactText.text = "Press E to pickup MedRecall4329";
                interactText.GetComponent<CanvasGroup>().alpha = 1;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    playerCanInteract = false;
                    StartCoroutine(bodyChanging.ResetBody());
                    Destroy(thisMed.gameObject,0.1f);
                    interactText.GetComponent<CanvasGroup>().alpha = 0;
                }
            }
        }
    }
}
