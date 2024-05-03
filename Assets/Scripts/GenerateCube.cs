using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class GenerateCube : MonoBehaviour
{
    [SerializeField] private ControllerCube _controllerCube;
    [SerializeField] private float _scaleReduce;
    [SerializeField] private int _minCountGenerate;
    [SerializeField] private int _maxCountGenerate;
    [SerializeField] private float _radiusGenerate;
    public event Action<List<Collider>> GeneratedCubes;
    private int _maxChanceSplit = 100;
    private int _chanceSplit;
    private int _decreaceChance = 2;

    private void Awake()
    {
        _chanceSplit = _maxChanceSplit;
    }

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
        List<Collider> cubes = new List<Collider>();
        
        if (random.Next(_maxChanceSplit + 1) < _chanceSplit)
        {
            for (int i = 0; i < count; i++)
            {
                Vector3 oldCubePosition = _controllerCube.gameObject.transform.position;
                oldCubePosition.y += _radiusGenerate;
                GameObject newCube = Instantiate(_controllerCube.gameObject, UnityEngine.Random.insideUnitSphere * _radiusGenerate + oldCubePosition, Quaternion.identity);
                newCube.GetComponent<Renderer>().material.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
                newCube.GetComponent<GenerateCube>()._chanceSplit = _chanceSplit / _decreaceChance;
                newCube.transform.localScale /= _scaleReduce;
                
                cubes.Add(newCube.GetComponent<Collider>());
            }
        }

        GeneratedCubes?.Invoke(cubes);
    }
}
