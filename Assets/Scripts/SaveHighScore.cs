using UnityEngine;
namespace ViralDecay
{
    public class SaveHighScore : MonoBehaviour
    {
        private const string HighScoreKey = "HighScore";

        public void SaveThisScore(float score)
        {
            PlayerPrefs.SetFloat(HighScoreKey, score);
            PlayerPrefs.Save();
        }

        public float GetHighScore()
        {
            return PlayerPrefs.GetFloat(HighScoreKey, 0f);
        }
    }
}
