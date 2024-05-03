using System.Collections.Generic;
using UnityEngine;

public class ExplodeCube : MonoBehaviour
{
    [SerializeField] private GenerateCube _generateCube;
    [SerializeField] private ControllerCube _controllerCube;
    [SerializeField] private float _forceExplode;
    [SerializeField] private float _radiusExplode;
    private void OnEnable()
    {
        _generateCube.GeneratedCubes += Explode;
    }

    private void OnDisable()
    {
        _generateCube.GeneratedCubes -= Explode;
    }
    
    private void Explode(List<Collider> cubes)
    {
        Vector3 explosionPos = _controllerCube.gameObject.transform.position;
        
        foreach (Collider hit in cubes)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(_forceExplode, explosionPos, _radiusExplode);
        }
    }
}
