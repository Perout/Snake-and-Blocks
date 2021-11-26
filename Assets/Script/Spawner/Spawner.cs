using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private Transform _container;
    [SerializeField] private int _repeatCount;
    [SerializeField] private int _distanceBetweenFullLine;
    [SerializeField] private int _distanceBetweenRandomLine;

    [Header("Block")]
    [SerializeField] private Block _blockTemplate;
    [SerializeField] private int _blockSpawnChange;

    [Header("Bonus")]
    [SerializeField] private Bonus _bonusTemplate;
    [SerializeField] private int _bonusSpawnChange;

    [Header("Wall")]
    [SerializeField] private Wall _wallTemplate;
    [SerializeField] private int _wallSpawnChance;
    [Header("End Road")]
    [SerializeField] private Road _roadTemplate;
    [SerializeField] private int _roadside;

    private BlockSpawnPoint[] _blockSpawnPont;
    private WallSpawnPoint[] _wallSpawnPoints;
    private BonusSpawnPoint[] _bonusSpawnPoint;
    private RoadsideSpawn[] _roadSpawnPoints;

    private void Start()
    {
        _roadSpawnPoints = GetComponentsInChildren<RoadsideSpawn>();
        _bonusSpawnPoint = GetComponentsInChildren<BonusSpawnPoint>();
        _blockSpawnPont = GetComponentsInChildren<BlockSpawnPoint>();
        _wallSpawnPoints = GetComponentsInChildren<WallSpawnPoint>();
        for (int i = 0; i < _repeatCount; i++)
        {
            MoveSpawner(_distanceBetweenFullLine);
            //GenerateRandomElements(_roadSpawnPoints, _roadTemplate.gameObject, _roadside, 1, _distanceBetweenRandomLine / 2f);
            GenerateFullLine(_roadSpawnPoints, _roadTemplate.gameObject);
            GenerateRandomElements(_wallSpawnPoints, _wallTemplate.gameObject, _wallSpawnChance,_distanceBetweenFullLine, _distanceBetweenFullLine/ 2f);
            GenerateFullLine(_blockSpawnPont, _blockTemplate.gameObject);
            MoveSpawner(_distanceBetweenRandomLine);
            GenerateRandomElements(_wallSpawnPoints, _wallTemplate.gameObject, _wallSpawnChance,_distanceBetweenRandomLine,_distanceBetweenRandomLine/2f);
            GenerateRandomElements(_blockSpawnPont, _blockTemplate.gameObject, _blockSpawnChange);
            GenerateRandomElements(_bonusSpawnPoint, _bonusTemplate.gameObject, _bonusSpawnChange,1,_distanceBetweenRandomLine);
        }
    }
    private void GenerateFullLine(SpawnPoint[] spawnPoints,GameObject generatedElemet)
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GenerateElement(spawnPoints[i].transform.position, generatedElemet);
           
        }
    }
    private void GenerateRandomElements(SpawnPoint[] spawnPoints,GameObject generatedElement,int spawnChance,int scalyY=0,float offsetY=0)
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (Random.Range(0,100)<spawnChance)
            {
                GameObject element = GenerateElement(spawnPoints[i].transform.position, generatedElement,offsetY);
                element.transform.localScale = new Vector3(element.transform.localScale.x, scalyY, element.transform.localScale.z);
            }
        }
    }
    private GameObject GenerateElement(Vector3 spawnPoint,GameObject generatedElement,float offsetY=0)
    {
        spawnPoint.y -= offsetY;
        return Instantiate(generatedElement, spawnPoint, Quaternion.identity, _container);
    }
    private void MoveSpawner(int distanceY)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + distanceY, transform.position.z);
    }
}
