using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    public event Action ClikedCube;
    
    public Rigidbody Rigidbody { get; private set; }
    
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnMouseUpAsButton()
    {
        ClikedCube?.Invoke();
        Destroy(gameObject);
    }
}
