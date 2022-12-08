using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AppleComponent : MonoBehaviour
{

    #region references
    #endregion

    #region methods
    /// <summary>
    /// Informs Game Manager that the apple has been picked and destroys the gameobject
    /// </summary>
    /// <param name="other"></param>


    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.tag == "Player")
        {
            GameManager.Instance.OnPickApple();
            Debug.Log("Apple collected");
            Destroy(gameObject);
            
        }

    }
    #endregion


}
