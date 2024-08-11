using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ViralDecay
{
    public class TwoDCharacterMovement : MonoBehaviour
    {
        [SerializeField] private InputActionAsset controllerSettings;
        [SerializeField] private TextMeshProUGUI distanceText;
        private SoundManager _soundManager;
        private GasCloud _gasCloud;
        public float characterWalkSpeed;
        [SerializeField] private float characterJumpingForce;
        private InputAction _characterIAC;
        private float characterRunSpeed;
        private Rigidbody2D _characterRD;
        private bool _isGrounded;
        private bool _isRunning;
        private bool _isJumping;
        private InputAction _jumpAction;
        private InputAction _forwardAction;
        private InputAction _backwardsAction;
        private InputAction _runAction;
        private ScrollingBackground _sB;
        public float _distanceTravelled = 0;
        [HideInInspector] public float moveInput;


        private void Awake()
        {
            _soundManager = FindObjectOfType<SoundManager>();
            _characterRD = GetComponent<Rigidbody2D>();
            characterRunSpeed = characterWalkSpeed * 2;
            _jumpAction = controllerSettings.FindActionMap("Movement").FindAction("Jump");
            _forwardAction = controllerSettings.FindActionMap("Movement").FindAction("Forward");
            _backwardsAction = controllerSettings.FindActionMap("Movement").FindAction("Backwards");
            _runAction = controllerSettings.FindActionMap("Movement").FindAction("Run");
            _sB = FindObjectOfType<ScrollingBackground>();
            _gasCloud = FindObjectOfType<GasCloud>();
        }

        private void Start()
        {
            StartCoroutine(CharacterMovementCheck());
            distanceText.text = "Distance Travelled: " + _distanceTravelled.ToString("00");
        }

        private void OnEnable()
        {
            _runAction.Enable();
            _jumpAction.Enable();
            _forwardAction.Enable();
            _backwardsAction.Enable();
        }

        private void OnDisable()
        {
            _runAction.Disable();
            _jumpAction.Disable();
            _forwardAction.Disable();
            _backwardsAction.Disable();
        }

        private IEnumerator CharacterMovementCheck()
        {
            while (true)
            {
               moveInput = _forwardAction.ReadValue<float>() - _backwardsAction.ReadValue<float>();

                if (_jumpAction.triggered && !_isJumping)
                {
                    Debug.Log("Jumping Activated");
                    StartCoroutine(CharacterJump());
                }
                
                if (_forwardAction.triggered || _forwardAction.ReadValue<float>() > 0)
                {
                    StartCoroutine(_sB.ScrollingForwards());
                    StartCoroutine(_soundManager.PlayerWalking());
                    _gasCloud.currentScaleX -= 0.008f;
                    _gasCloud.currentScaleY -= 0.008f;
                    _distanceTravelled += 0.1f * Time.deltaTime;
                    _distanceTravelled = Mathf.Clamp(_distanceTravelled, 0, float.MaxValue);
                    distanceText.text = "Distance Travelled: " + _distanceTravelled.ToString("F2");
                    
                    if (_runAction.triggered  && !_isJumping || _runAction.ReadValue<float>() > 0 && !_isJumping)
                    {
                        StartCoroutine(_soundManager.PlayerWalking());
                        _characterRD.velocity = new Vector2(characterRunSpeed, _characterRD.velocity.y);
                        _distanceTravelled += 0.2f * Time.deltaTime;
                        _distanceTravelled = Mathf.Clamp(_distanceTravelled, 0, float.MaxValue);
                        distanceText.text = "Distance Travelled: " + _distanceTravelled.ToString("F2");
                    }
                    else
                        _characterRD.velocity = new Vector2(characterWalkSpeed, _characterRD.velocity.y);
                    
                }
                
                if (_backwardsAction.triggered && !_isJumping || _backwardsAction.ReadValue<float>() > 0 && !_isJumping)
                {
                    StartCoroutine(_sB.ScrollingBackwards());
                    StartCoroutine(_soundManager.PlayerWalking());
                    _distanceTravelled -= 0.1f * Time.deltaTime;
                    _distanceTravelled = Mathf.Clamp(_distanceTravelled, 0, float.MaxValue);
                    distanceText.text = "Distance Travelled: " + _distanceTravelled.ToString("F2");
                    
                    if (_runAction.triggered || _runAction.ReadValue<float>() > 0)
                    {
                        StartCoroutine(_soundManager.PlayerWalking());
                        _characterRD.velocity = new Vector2(-characterRunSpeed, _characterRD.velocity.y);
                        _distanceTravelled -= 0.2f * Time.deltaTime;
                        _distanceTravelled = Mathf.Clamp(_distanceTravelled, 0, float.MaxValue);
                        distanceText.text = "Distance Travelled: " + _distanceTravelled.ToString("F2");
                    }
                    else
                        _characterRD.velocity = new Vector2(-characterWalkSpeed, _characterRD.velocity.y);
                }
                
                if (Mathf.Abs(moveInput) < 0.1f)
                {
                    _characterRD.velocity = new Vector2(0, _characterRD.velocity.y);
                }
                
                yield return null;
            }
        }
        
        private IEnumerator CharacterJump()
        {
            _isJumping = true;
            _characterRD.AddForce(Vector2.up * characterJumpingForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.5f);
            _isJumping = false;
        }
    }
}
