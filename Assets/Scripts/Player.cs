using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField]
    private float _playerSpeed;
    [SerializeField]
    private float _jumpHeight = 5.0f;
    [SerializeField]
    private float _gravityValue = 1f;
    private float _yVelocity;
    private bool _canDoubleJump=true;
    [SerializeField]
    private int _coins;
    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(horizontalInput, 0, 0);
        Vector3 velocity = direction * _playerSpeed;
      
        if (_controller.isGrounded ==true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
                _canDoubleJump = true;
            } 
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_canDoubleJump == true)
                { 
                    _yVelocity += _jumpHeight*1.1f;
                    _canDoubleJump = false;
                }
            }
            _yVelocity -= _gravityValue;
        }

         velocity.y = _yVelocity ;
        _controller.Move(velocity * Time.deltaTime);
    }
    public void AddCoins()
    {
        _coins++;
        _uiManager.UpdateCoinDisplay(_coins);
    }

}
