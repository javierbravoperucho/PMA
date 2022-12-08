using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ScreenToWorldComponent : MonoBehaviour
{
    #region parameters
    [SerializeField] private float _maxDistance = 100.0f;
    #endregion
    #region references
    [SerializeField]
    private GameObject _cameraObject;
    private Camera _camera;
    private Transform _cameraTransform;
    private Transform _playerTransform;
    #endregion
    #region properties
    private RaycastHit _myRaycastHit;
    private LayerMask _myLayerMask;
    #endregion
    #region methods
    public Vector3 ScreenToWorldPoint(Vector3 screenPoint)
    {
        Ray ray = _camera.ScreenPointToRay(screenPoint);

        if (Physics.Raycast(ray, out _myRaycastHit, _maxDistance))
        {
            return _myRaycastHit.point;
            
        }
        else return _playerTransform.position;
    }

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _camera=_cameraObject.GetComponent<Camera>();
        _playerTransform= GetComponent<Transform>();

    }
    // Update is called once per frame
    void Update()
    {
        //TODO
    }
}