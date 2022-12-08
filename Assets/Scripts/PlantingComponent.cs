using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlantingComponent : MonoBehaviour
{
    #region references
    /// <summary>
    /// Plant prefab to instantiate
    /// </summary>
    [SerializeField] private GameObject _plantPrefab;
    [SerializeField] private GameObject _myGameManager;
    private AppleComponent _myAppleComponent;
    /// <summary>
    /// Reference to camera
    /// </summary>
    private Camera _camera;
    /// <summary>
    /// Reference to own movement component
    /// </summary>
    private MovementComponent _myMovementComponent;
    /// <summary>
    /// Reference to own input component
    /// </summary>
    private InputComponent _myInputComponent;
    /// <summary>
    /// Reference to desired soil
    /// </summary>
    private SoilComponent _desiredSoilComponent;
    #endregion
    #region properties
    /// <summary>
    /// Hit info to store hit info
    /// </summary>
    private RaycastHit _myHitInfo;
    /// <summary>
    /// Layermast to filter desired layer
    /// </summary>
    private LayerMask _myLayerMask;
    /// <summary>
    /// Indicates the planting state
    /// </summary>
    private PlantingStates _plantingState;

    #endregion
    /// <summary>
    /// Defined planting states
    /// </summary>
    #region enums
    public enum PlantingStates { None, IsPlanting };
    #endregion
    #region methods
    /// <summary>
    /// Detects if the player has clicked a valid soil area and returns corresponding
   // component
 /// </summary>
 /// <param name="pointToEvaluate">Point to be evaluated</param>
 /// <returns></returns>
   private  SoilComponent EvaluatePoint(Vector3 pointToEvaluate)
    {
        Ray ray = _camera.ScreenPointToRay(pointToEvaluate);
        _myLayerMask = LayerMask.GetMask("Tierra");

        if (Physics.Raycast(ray, out _myHitInfo, 100, _myLayerMask))
        {
            _plantingState = PlantingStates.IsPlanting;
           SoilComponent soilComponent = _myHitInfo.collider.GetComponent<SoilComponent>();
           return soilComponent;
            

        }
        return null;
    }
    /// <summary>
    /// Tries to plant in a point. If valid point, sotres the component and goes to
   // desired point
 /// </summary>
 /// <param name="plantingPoint">Point where the player wants to plant in</param>
    public void TryPlant(Vector3 plantingPoint)
    {
        _myMovementComponent = GetComponent<MovementComponent>();
        
        SoilComponent auxSoilComponent = EvaluatePoint(plantingPoint);
        if (!auxSoilComponent.IsPlanted)
        {
            GetComponent<InputComponent>().enabled = false;
            _myMovementComponent.GoToPoint(plantingPoint);
        }
        

    }
    /// <summary>
    /// Detects Soil Component. If it is the desired soil, it gets planted.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tierra" && _plantingState == PlantingStates.IsPlanting) {
            GetComponent<InputComponent>().enabled = true;
            GameManager auxGameManager = _myGameManager.GetComponent<GameManager>();
            if (auxGameManager.Current >=1)
            {
                Debug.Log("Arbol Plantado");
                _desiredSoilComponent = other.GetComponent<SoilComponent>();
                _desiredSoilComponent.Plant(_plantPrefab);
                auxGameManager.OnPlantApple();
            }
            _plantingState = PlantingStates.None;
        }
    }
    #endregion
    /// <summary>
    /// Initialization of references, raycasthit and layermask.
    /// </summary>
    void Start()
    {
      
       _camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        
    }
}