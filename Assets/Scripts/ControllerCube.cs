using System;
using UnityEngine;

[RequireComponent(typeof(GameObject))]
public class ControllerCube : MonoBehaviour
{
    public event Action ClikedCube;

    private void OnMouseUpAsButton()
    {
        ClikedCube?.Invoke();
        Destroy(gameObject);
    }
}
