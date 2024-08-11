using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using ViralDecay;

public class BodyChanging : MonoBehaviour
{
    [SerializeField] private Transform zombieParent;
    [SerializeField] private GameObject deathSkull;
    private SoundManager _soundManager;
    private SaveHighScore _saveHighScore;
    private TwoDCharacterMovement _player;
    private int _childActivation;
    public Slider healthSlider; 
    public float maxHealth = 100f;
    private float currentHealth;
    private PlayerDeathChecker _playerDeathChecker;
    [SerializeField] private GameObject interactionText;

    private void Start()
    {
        _playerDeathChecker = FindObjectOfType<PlayerDeathChecker>();
        _saveHighScore = FindObjectOfType<SaveHighScore>();
        _player = FindObjectOfType<TwoDCharacterMovement>();
        _soundManager = FindObjectOfType<SoundManager>();
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    public IEnumerator ChangeBody()
    {
        if (zombieParent.childCount > 0)
        {
            _childActivation += 1;
            yield return new WaitForSeconds(0.2f);
            
            if (_childActivation > zombieParent.childCount)
            {
                float tempScore;
                Instantiate(deathSkull, Vector3.up, Quaternion.identity);
                tempScore = _saveHighScore.GetHighScore();
                if (_player._distanceTravelled > tempScore)
                {
                    _saveHighScore.SaveThisScore(_player._distanceTravelled);
                }
                StartCoroutine(_playerDeathChecker.PlayersDeath());
                _childActivation = 0;
                Destroy(this.gameObject,0.1f);
            }

            switch (_childActivation)
            {
                case 1:
                    zombieParent.GetChild(0).gameObject.SetActive(true);
                    TakeDamage(17f);
                    StartCoroutine(_soundManager.CaughtByGasCloud());
                    break;
                case 2:
                    zombieParent.GetChild(1).gameObject.SetActive(true);
                    TakeDamage(17f);
                    StartCoroutine(_soundManager.CaughtByGasCloud());
                    break;
                case 3:
                    zombieParent.GetChild(2).gameObject.SetActive(true);
                    TakeDamage(17f);
                    StartCoroutine(_soundManager.CaughtByGasCloud());
                    break;
                case 4:
                    zombieParent.GetChild(3).gameObject.SetActive(true);
                    TakeDamage(17f);
                    StartCoroutine(_soundManager.CaughtByGasCloud());
                    break;
                case 5:
                    zombieParent.GetChild(4).gameObject.SetActive(true);
                    TakeDamage(17f);
                    StartCoroutine(_soundManager.CaughtByGasCloud());
                    break;
                case 6:
                    zombieParent.GetChild(5).gameObject.SetActive(true);
                    TakeDamage(17f);
                    StartCoroutine(_soundManager.CaughtByGasCloud());
                    break;
            }
            
        }
        yield return null;
    }
    
    public IEnumerator ResetBody()
    {
        if (zombieParent.childCount > 0)
        {
            for (int i = 0; i < zombieParent.childCount; i++)
            {   
                Heal(100f);
                zombieParent.GetChild(i).gameObject.SetActive(false);

                yield return new WaitForSeconds(0.5f);
                interactionText.GetComponent<CanvasGroup>().alpha = 0;
            }
        }
        yield return null;
    }
    
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Prevent health from going below 0
        healthSlider.value = currentHealth;
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Prevent health from exceeding maxHealth
        healthSlider.value = currentHealth;
    }
}
