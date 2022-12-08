using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ScreenToWorldComponent))]
public class InputComponent : MonoBehaviour
{

    #region references
    /// <summary>
    /// Reference to own screentoworld component
    /// </summary>
    private ScreenToWorldComponent _myScreenToWorldComp;
    /// <summary>
    /// Reference to own movement component
    /// </summary>
    private MovementComponent _myMovementComponent;
    /// <summary>
    /// Reference to own planting component
    /// </summary>
    private PlantingComponent _myPlantingComponent;
    #endregion

    #region properties
    private LayerMask _myLayerMask;
    /// <summary>
    /// Mouse position
    /// </summary>
    public Vector3 _mousePosition;
    #endregion

    /// <summary>
    /// References initialization
    /// </summary>
    void Start()
    {
        _myMovementComponent = GetComponent<MovementComponent>();

    }
    /// <summary>
    /// Receives input and calls required methods
    /// </summary>
    void Update()
    {
        _mousePosition = Input.mousePosition;
        

        if (Input.GetMouseButtonDown(0))
        {
             _myMovementComponent.GoToPoint(_mousePosition);
             GetComponent<ScreenToWorldComponent>().ScreenToWorldPoint(_mousePosition);

        }
        else if (Input.GetMouseButtonDown(1))
        {
            _myPlantingComponent = GetComponent<PlantingComponent>();
            _myPlantingComponent.TryPlant(_mousePosition);

        }

    }
}