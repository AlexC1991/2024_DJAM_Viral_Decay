using System;
using TMPro;
using UnityEngine;
namespace ViralDecay
{
    public class StartMenu : MonoBehaviour
    {
        [SerializeField] private GameObject startMenu;
        [SerializeField] private TextMeshProUGUI highestScoreText;
        [SerializeField] SaveHighScore saveHighScore;
        private GasCloud gasCloud;

        private void Awake()
        {
            gasCloud = FindObjectOfType<GasCloud>();
        }

        private void Start()
        {
            Time.timeScale = 0;
            startMenu.SetActive(true);

            float highScore;
            highScore = saveHighScore.GetHighScore();
            highestScoreText.text = "Highest Scored Distance: " + highScore.ToString("F0");
            
        }
        
        public void LetsStartThisGame()
        {
            Time.timeScale = 1;
            StartCoroutine(gasCloud.StartUp());
            startMenu.SetActive(false);
        }
    }
}
