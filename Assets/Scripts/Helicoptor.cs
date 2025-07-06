using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicoptor : MonoBehaviour
{
    [SerializeField] private float thrust = 20f;
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            _rigidbody2D.AddForce(transform.up * thrust);
        }
    }
}
