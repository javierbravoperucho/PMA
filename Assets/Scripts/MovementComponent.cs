using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MovementComponent : MonoBehaviour
{
    #region parameters
    /// <summary>
    /// Movement speed magnitude
    /// </summary>
    [SerializeField] private float _movementSpeed = 1.0f;
    /// <summary>
    /// Distance to target to stop movement
    /// </summary>
    [SerializeField] private float _stopDistance = 1.0f;
    #endregion

    #region references
    /// <summary>
    /// Reference to own transform
    /// </summary>
    private Transform _myTransform;
    /// <summary>
    /// Reference to own character controller
    /// </summary>
    private CharacterController _myCharacterController;
    /// <summary>
    /// Reference to screen to world component
    /// </summary>
    private ScreenToWorldComponent _myScreenToWorldComp;
    #endregion

    #region properties
    /// <summary>
    /// Target point the player wants to move towards
    /// </summary>
    private Vector3 _myTargetPoint;
    /// <summary>
    /// Movement speed vector
    /// </summary>
    private Vector3 _movementSpeedVector;
    #endregion

    #region methods
    /// <summary>
    /// Method to move towards desired point
    /// </summary>
    /// <param name="targetPoint"></param>
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
    /// <summary>
    /// References initialization
    /// </summary>
    void Start()
    {
        this.enabled = false;
        _myCharacterController = GetComponent<CharacterController>();
        _myTransform = GetComponent<Transform>();
    }

    /// <summary>
    /// Move with desired speed and direction
    /// </summary>
    void Update()
    {
        _movementSpeedVector = _myTargetPoint - _myTransform.position;
        _movementSpeedVector.Normalize();
        _myCharacterController.SimpleMove(_movementSpeedVector * _movementSpeed);
    }
}
