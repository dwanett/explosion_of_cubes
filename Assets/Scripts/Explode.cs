using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    [SerializeField] private GeneratorCubes _generateCube;
    [SerializeField] private Cube _cube;
    [SerializeField] private float _forceExplode;
    [SerializeField] private float _radiusExplode;
    
    private void OnEnable()
    {
        _generateCube.GeneratedCubes += ExplodeCubes;
    }

    private void OnDisable()
    {
        _generateCube.GeneratedCubes -= ExplodeCubes;
    }
    
    private void ExplodeCubes(List<Rigidbody> cubesRigidbodies)
    {
        Vector3 explosionPos = _cube.gameObject.transform.position;
        
        foreach (Rigidbody rigidbody in cubesRigidbodies)
        {
            rigidbody.AddExplosionForce(_forceExplode, explosionPos, _radiusExplode);
        }
    }
}
