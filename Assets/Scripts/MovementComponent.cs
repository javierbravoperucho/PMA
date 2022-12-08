using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MovementComponent : MonoBehaviour
{
    #region parameters
    [SerializeField] private float _movementSpeed = 1.0f;
    [SerializeField] private float _stopDistance = 1.0f;
    #endregion
    #region references
    private Transform _myTransform;
    private CharacterController _myCharacterController;
    private ScreenToWorldComponent _myScreenToWorldComp;
   
    
    #endregion
    #region properties
    private Vector3 _myTargetPoint;
    private Vector3 _movementSpeedVector;
    #endregion
    #region methods
    public void GoToPoint(Vector3 targetPoint)
    {
        _myTargetPoint = GetComponent<ScreenToWorldComponent>().ScreenToWorldPoint(targetPoint);
        this.enabled = true;

    }
    public bool Speed0()
    {
        if (_movementSpeedVector == Vector3.zero)
        {
            return true;
        }
        else return false;
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        this.enabled = false;
        _myCharacterController = GetComponent<CharacterController>();
        _myTransform = GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        _movementSpeedVector = _myTargetPoint - _myTransform.position;
        _movementSpeedVector.Normalize();
        _myCharacterController.SimpleMove(_movementSpeedVector * _movementSpeed);
    }
}
