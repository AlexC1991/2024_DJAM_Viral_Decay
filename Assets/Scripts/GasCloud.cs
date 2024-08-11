using System.Collections;
using UnityEngine;
namespace ViralDecay
{
    public class GasCloud : MonoBehaviour
    {
        [HideInInspector] public float currentScaleX;
        [HideInInspector] public float currentScaleY;
        private void Start()
        {
            currentScaleX = transform.localScale.x;
            currentScaleY = transform.localScale.y;
        }

        public IEnumerator StartUp()
        {
            while (true)
            {
                currentScaleX += 0.005f;
                currentScaleY += 0.005f;
                currentScaleX = Mathf.Clamp(currentScaleX, 0, float.MaxValue);
                currentScaleY = Mathf.Clamp(currentScaleY, 0, float.MaxValue);
                transform.localScale = new Vector2(currentScaleX, currentScaleY);
                
                yield return null;
            }
        }
    }
}
