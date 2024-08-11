using UnityEngine;
namespace ViralDecay
{
    public class KillBox : MonoBehaviour
    {
        [TagSelector][SerializeField] private string itemTag;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(itemTag))
            {
                Destroy(other.gameObject);
            }
        }
    }
}
