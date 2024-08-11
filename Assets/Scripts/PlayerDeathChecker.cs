using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace ViralDecay
{
    public class PlayerDeathChecker : MonoBehaviour
    {
        public IEnumerator PlayersDeath()
        {
            SceneManager.LoadScene("SampleScene");
            Debug.Log("Player is dead");
            yield break;
        }
    }
}
