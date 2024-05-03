using System;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorCubes : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private float _scaleReduce;
    [SerializeField] private int _minCountGenerate;
    [SerializeField] private int _maxCountGenerate;
    [SerializeField] private float _radiusGenerate;
    
    private int _maxChanceSplit = 100;
    private int _chanceSplit;
    private int _decreaceChance = 2;
    
    public event Action<List<Rigidbody>> GeneratedCubes;

    public void SetChance(int newChance)
    {
        _chanceSplit = newChance;
    }
    
    private void Awake()
    {
        _chanceSplit = _maxChanceSplit;
    }

    private void OnEnable()
    {
        _cube.ClikedCube += GenerateCubes;
    }

    private void OnDisable()
    {
        _cube.ClikedCube -= GenerateCubes;
    }

    private void GenerateCubes()
    {
        System.Random random = new System.Random();
        int count = random.Next(_minCountGenerate, _maxCountGenerate + 1);
        List<Rigidbody> cubes = new List<Rigidbody>();
        
        if (random.Next(_maxChanceSplit + 1) < _chanceSplit)
        {
            for (int i = 0; i < count; i++)
            {
                Vector3 oldCubePosition = _cube.gameObject.transform.position;
                oldCubePosition.y += _radiusGenerate;
                Rigidbody newCube = Instantiate(_cube.Rigidbody, UnityEngine.Random.insideUnitSphere * _radiusGenerate + oldCubePosition, Quaternion.identity);
                newCube.GetComponent<Renderer>().material.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
                newCube.GetComponent<GeneratorCubes>().SetChance(_chanceSplit / _decreaceChance);
                newCube.transform.localScale /= _scaleReduce;
                
                cubes.Add(newCube.GetComponent<Rigidbody>());
            }
        }

        GeneratedCubes?.Invoke(cubes);
    }
}
