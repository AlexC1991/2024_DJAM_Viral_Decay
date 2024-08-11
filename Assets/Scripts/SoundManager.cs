using System.Collections;
using UnityEngine;
namespace ViralDecay
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private ScriptableAudioFile backgroundSound;
        [SerializeField] private ScriptableAudioFile walkingSound;
        [SerializeField] private ScriptableAudioFile caughtByGasCloudSound;
        private TwoDCharacterMovement player;

        private void Start()
        {
            player = FindObjectOfType<TwoDCharacterMovement>();
            backgroundSound.PlayAudio();
        }

        public IEnumerator PlayerWalking()
        {
            while (player.moveInput != 0)
            {
                if (walkingSound.audioSource == null || !walkingSound.audioSource.isPlaying)
                {
                    walkingSound.PlayAudio();
                }
                
                if (walkingSound.audioSource == null || !walkingSound.audioSource.isPlaying)
                {
                    if (player.moveInput == 0)
                    {
                        walkingSound.StopAudio();
                    }
                }

                yield return null;
            }
        }

        public IEnumerator CaughtByGasCloud()
        {
            if (caughtByGasCloudSound.audioSource == null || !caughtByGasCloudSound.audioSource.isPlaying)
            {
                caughtByGasCloudSound.PlayAudio();
            }
            
            yield return null;
        }
    }
}
