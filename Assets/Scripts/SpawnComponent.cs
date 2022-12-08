using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnComponent : MonoBehaviour
{
    #region parameters
    /// <summary>
    /// Min time to spawn a new apple
    /// </summary>
    [SerializeField] private float _minSpawnInterval;
    /// <summary>
    /// Mast time to spawn a new apple
    /// </summary>
    [SerializeField] private float _maxSpawnInterval;
    
    #endregion
    #region references
    /// <summary>
    /// Apple prefab to be instantiated
    /// </summary>
    [SerializeField] private GameObject _applePrefab;
    /// <summary>
    /// Reference to last instantiated apple
    /// </summary>
    private GameObject _apple;
    /// <summary>
    /// Reference to own transform
    /// </summary>
    private Transform _myTransform;
    #endregion
    #region properties
    /// <summary>
    /// Time for next spawm
    /// </summary>
    private float _timeToSpawn;
    /// <summary>
    /// Elapsed time since apple was collected  
    /// </summary>
    private float _elapsedTime = 0.0f;
    #endregion
    #region methods
  /* private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Apple")
        {           
           _apple = other.gameObject;
        }

        Debug.Log("hay manzana");
    }*/
    #endregion
    /// <summary>
    /// Initialization of references and stuff
    /// </summary>
    void Start()
    {
        _myTransform = gameObject.transform;
        _timeToSpawn = Random.Range(_minSpawnInterval, _maxSpawnInterval);

    }
    /// <summary>
    /// Spawning logic
    /// </summary>
    void Update()
    {
        _elapsedTime += Time.deltatime;

        if (_apple==null && _elapsedTime > _timeToSpawn)
        {
            Debug.Log("manzana");
            _timeToSpawn = Random.Range(_minSpawnInterval, _maxSpawnInterval);
            _elapsedTime = 0.0f;

            _apple = Instantiate(_applePrefab, _myTransform.position, Quaternion.identity);
        }

    }
}

