using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ScreenToWorldComponent))]
public class InputComponent : MonoBehaviour
{
    private float _maxDistance = 100.0f;
    #region references
    private ScreenToWorldComponent _myScreenToWorldComp;
    private MovementComponent _myMovementComponent;
    //private Camera _camera;
    #endregion
    #region properties
    //private RaycastHit _myRaycastHit;
    private LayerMask _myLayerMask;
    public Vector3 _mousePosition;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
       // _camera = _cameraObject.GetComponent<Camera>();

    }
    // Update is called once per frame
    void Update()
    {
        _mousePosition = Input.mousePosition;
        

        if (Input.GetMouseButtonDown(0))
        {
       
             GetComponent<MovementComponent>().GoToPoint(_mousePosition);
             GetComponent<ScreenToWorldComponent>().ScreenToWorldPoint(_mousePosition);

            /*Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
           if (Physics.Raycast(ray, out _myRaycastHit, _maxDistance))
           {

               Destroy(_startButton);
               GetComponent<FollowCamera>().ActiveCamera();
           }*/


        }
        else if (Input.GetMouseButtonDown(1))
        {
            PlantingComponent auxPlantingComponent = GetComponent<PlantingComponent>();
            auxPlantingComponent.TryPlant(_mousePosition);
            

            //while (!_myMovementComponent.Speed0()) ;
        }

    }
}