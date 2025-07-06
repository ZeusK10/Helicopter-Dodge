using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicoptor : MonoBehaviour
{
    public static Action OnGameOver;

    [SerializeField] private float thrust = 20f;
    private Rigidbody2D _rigidbody2D;
    private bool _allowMove;

    private void Start()
    {
        GameManager.OnGamestart += Play;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.gravityScale = 0f;
    }

    private void OnDestroy()
    {
        GameManager.OnGamestart -= Play;
    }

    private void Play()
    {
        _allowMove = true;
        transform.position = new Vector3(transform.position.x, 2.5f, transform.position.z);
        _rigidbody2D.gravityScale = 1f;
    }
    

    void Update()
    {
        if (!_allowMove) return;
        if(Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            _rigidbody2D.AddForce(transform.up * thrust);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            _allowMove = false;
            OnGameOver?.Invoke();
            _rigidbody2D.gravityScale = 0f;
        }
    }
}
