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
    
    private void Explode()
    {
        Vector3 explosionPos = _controllerCube.gameObject.transform.position;
        List<Collider> colliders = new List<Collider>(Physics.OverlapSphere(explosionPos, _radiusExplode));
        colliders.Remove(_controllerCube.gameObject.GetComponent<Collider>());
        
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(_forceExplode, explosionPos, _radiusExplode);
        }
    }
}
