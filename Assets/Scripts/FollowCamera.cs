using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FollowCamera : MonoBehaviour
{
    #region parameters
    /// <summary>
    /// Camera horizontal offset to target
    /// </summary>
    [SerializeField] private float _horizontalOffset = 1.0f;
    /// <summary>
    /// Camera vertica offset to target
    /// </summary>
    [SerializeField] private float _verticalOffset = 1.0f;
    /// <summary>
    /// Look at point vertical offset to target
    /// </summary>
    [SerializeField] private float _lookatVerticalOffset = 1.0f;
    /// <summary>
    /// Follow factor to regulate camera responsiveness
    /// </summary>
    [SerializeField] private float _followFactor = 1.0f;
    #endregion
    #region references
    /// <summary>
    /// Reference to target's transform
    /// </summary>
    [SerializeField] private Transform _targetTransform;
    /// <summary>
    /// Reference to own transform
    /// </summary>
    private Transform _myTransform;
    #endregion
    /// <summary>
    /// Initialiation of desired position and lookat point
    /// </summary>
    public void ActiveCamera()
    {
        this.enabled = true;
    }
    void Start()
    {
        
        _myTransform = transform;
    }
    /// <summary>
    /// Updates camera position
    /// </summary>
    void LateUpdate()
    {
        Vector3 desiredPosition = _targetTransform.position+ new Vector3(_horizontalOffset,_verticalOffset,_lookatVerticalOffset);
        Vector3 smoothedPosition = Vector3.Lerp(_myTransform.position,desiredPosition, _followFactor);
        _myTransform.position = smoothedPosition;
    }
}
