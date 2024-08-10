using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ViralDecay
{
    public class TwoDCharacterMovement : MonoBehaviour
    {
        [SerializeField] private InputActionAsset controllerSettings;
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
        [HideInInspector] public float moveInput;


        private void Awake()
        {
            _characterRD = GetComponent<Rigidbody2D>();
            characterRunSpeed = characterWalkSpeed * 2;
            _jumpAction = controllerSettings.FindActionMap("Movement").FindAction("Jump");
            _forwardAction = controllerSettings.FindActionMap("Movement").FindAction("Forward");
            _backwardsAction = controllerSettings.FindActionMap("Movement").FindAction("Backwards");
            _runAction = controllerSettings.FindActionMap("Movement").FindAction("Run");
            _sB = FindObjectOfType<ScrollingBackground>();
        }

        private void Start()
        {
            StartCoroutine(CharacterMovementCheck());
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
                    //_sB.ScollingForwards();
                    
                    if (_runAction.triggered  && !_isJumping || _runAction.ReadValue<float>() > 0 && !_isJumping)
                    {
                        _characterRD.velocity = new Vector2(characterRunSpeed, _characterRD.velocity.y);
                    }
                    else
                        _characterRD.velocity = new Vector2(characterWalkSpeed, _characterRD.velocity.y);
                    
                }
                
                if (_backwardsAction.triggered && !_isJumping || _backwardsAction.ReadValue<float>() > 0 && !_isJumping)
                {
                   // _sB.ScrollBackwards();
                    
                    if (_runAction.triggered || _runAction.ReadValue<float>() > 0)
                    {
                        _characterRD.velocity = new Vector2(-characterRunSpeed, _characterRD.velocity.y);
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
