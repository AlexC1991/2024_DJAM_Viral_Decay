using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ViralDecay
{
    public class ScrollingBackground : MonoBehaviour
    {
        private float scrollPosition = 0f;
        
        public IEnumerator ScrollingForwards()
        {
            float scrollSpeed = 0.5f;
            scrollPosition += scrollSpeed * Time.deltaTime;
            gameObject.GetComponent<RawImage>().uvRect = new Rect(scrollPosition, 0, 1, 1);
            yield return null;
        }
        
        public IEnumerator ScrollingBackwards()
        {
            float scrollSpeed = -0.5f;
            scrollPosition += scrollSpeed * Time.deltaTime;
            gameObject.GetComponent<RawImage>().uvRect = new Rect(scrollPosition, 0, 1, 1);
            yield return null;
        }
    }
    
    
}
