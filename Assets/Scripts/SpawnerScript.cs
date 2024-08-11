using UnityEngine;
namespace ViralDecay
{
    public class SpawnerScript : MonoBehaviour
    {
        [SerializeField] private GameObject[] objectToSpawn;
        [SerializeField] private float spawnRate;
        private float nextSpawn;
        private void Update()
        {
            if (Time.time > nextSpawn)
            {
                nextSpawn = Time.time + spawnRate;
                int randomIndex = Random.Range(0, objectToSpawn.Length);
                Instantiate(objectToSpawn[randomIndex], transform.position, Quaternion.identity);
            }
        }
    }
}
