using System;
using UnityEngine;

public class GenerateCube : MonoBehaviour
{
    [SerializeField] private ControllerCube _controllerCube;
    [SerializeField] private float _scaleReduce;
    [SerializeField] private int _minCountGenerate;
    [SerializeField] private int _maxCountGenerate;
    [SerializeField] private float _radiusGenerate;
    public event Action GeneratedCubes;
    private int _chanceSplit = 100;
    private void OnEnable()
    {
        _controllerCube.ClikedCube += GenerateCubes;
    }

    private void OnDisable()
    {
        _controllerCube.ClikedCube -= GenerateCubes;
    }

    private void GenerateCubes()
    {
        System.Random random = new System.Random();
        int count = random.Next(_minCountGenerate, _maxCountGenerate + 1);

        if (_chanceSplit > 0)
        {
            for (int i = 0; i < count; i++)
            {
                Vector3 oldCubePosition = _controllerCube.gameObject.transform.position;
                oldCubePosition.y += _radiusGenerate;
                GameObject newCube = Instantiate(_controllerCube.gameObject, UnityEngine.Random.insideUnitSphere * _radiusGenerate + oldCubePosition, Quaternion.identity);
                newCube.GetComponent<Renderer>().material.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
                newCube.GetComponent<GenerateCube>()._chanceSplit = _chanceSplit / 2;
                newCube.transform.localScale /= _scaleReduce;
            }
        }

        GeneratedCubes?.Invoke();
    }
}
