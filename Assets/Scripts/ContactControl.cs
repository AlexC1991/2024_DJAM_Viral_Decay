using System;
using System.Collections;
using UnityEngine;
public class ContactControl : MonoBehaviour
{
    [TagSelector][SerializeField] private string gasTag;
    [SerializeField] private float effectInterval = 0.5f; 
    [SerializeField] private BodyChanging bodyChanging;

    private bool isInGasCloud = false;
    private bool canApplyEffect = true;  

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(gasTag))
        {
            isInGasCloud = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag(gasTag))
        {
            isInGasCloud = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(gasTag))
        {
            isInGasCloud = false;
        }
    }

    private void FixedUpdate()
    {
        if (isInGasCloud && canApplyEffect)
        {
            StartCoroutine(ApplyChanges());
        }
    }

    private IEnumerator ApplyChanges()
    {
        canApplyEffect = false; 

        StartCoroutine(bodyChanging.ChangeBody());
        
        yield return new WaitForSeconds(effectInterval);

        canApplyEffect = true;
    }
    
}
