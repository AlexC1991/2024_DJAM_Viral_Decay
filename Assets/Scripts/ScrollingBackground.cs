using UnityEngine;

namespace ViralDecay
{
    public class ScrollingBackground : MonoBehaviour
    {
        
        [Range(-1,1)] public float speed;
        private float offset;
        private Material mat;
        private static readonly int MainTex = Shader.PropertyToID("_MainTex");

        private void Start()
        {
            mat = GetComponent<Renderer>().material;
        }
        
        void Update()
        {
            offset += Time.deltaTime * speed;
            mat.SetTextureOffset(MainTex, new Vector2(offset, 0));
        }
    }
}
