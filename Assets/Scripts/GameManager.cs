using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public enum GameStates { START, GAME, GAMEOVER };
    #region references
    /// <summary>
    /// Reference to UI Manager
    /// </summary>
    private UIManager _UIManager;
    /// <summary>
    /// Reference to Level Manager
    /// </summary>
    private LevelManager _levelManager;
    /// <summary>
    /// Reference to player
    /// </summary>
    [SerializeField] GameObject _player;
    /// <summary>
    /// Array of levels. A level includes a sriptable object with data and a level prefab
    /// </summary>
    [SerializeField] LevelData[] _levels;
    #endregion
    #region properties
    /// <summary>
    /// Game manager instance
    /// </summary>
    static private GameManager _instance;
    /// <summary>
    /// Public reference to GameManager instance
    /// </summary>
    static public GameManager Instance { get { return _instance; } }
    /// <summary>
    /// Current game state
    /// </summary>
    private GameManager.GameStates _currentState;
    /// <summary>
    /// Next game state
    /// </summary>
    private GameManager.GameStates _nextState;
    /// <summary>
    /// Public access to Current State
    /// </summary>
    public GameManager.GameStates CurrentState { get { return _currentState; } }
    /// <summary>
    /// Level settings: Goal
    /// </summary>
    private int _goal;
    /// <summary>
    /// Level settings: Remaining time
    /// </summary>
    private float _remainingTime;
    /// <summary>
    /// Level settings: Round
    /// </summary>
    private int _nRound;
    /// <summary>
    /// Level settings: Current amount of apples
    /// </summary>
    private int _current;
    /// <summary>
    /// Public access to current amount of apples
    /// </summary>
    private int i;
    private GameObject levelObject;
    public int Current { get { return _current; } }
    #endregion
    #region methods
    /// <summary>
    /// Public method to register UI Manager
    /// </summary>
    /// <param name="uiManager">UI manager to register</param>
    public void RegisterUIManager(UIManager uiManager)
    {
       _UIManager= uiManager;
    }
    /// <summary>
    /// Public methods to register Level Manager
    /// </summary>
    /// <param name="levelManager">Level manager to register</param>
    public void RegisterLevelManager(LevelManager levelManager)
    {
        _levelManager = levelManager;
    }
    /// <summary>
    /// Method to be called when an apple is picked
    /// </summary>
    public void OnPickApple()
    {
        _current++;
    }
    /// <summary>
    /// Methods to be called when an apple is planted
    /// </summary>
    public void OnPlantApple()
    {
        _current--;
    }
    /// <summary>
    /// GameManager instance initialization
    /// </summary>
    private void Awake()
    {
        _instance= this;
    }
    /// <summary>
    /// Method to be called when game enters a new state
    /// </summary>
    /// <param name="newState">New state</param>
    private void EnterState(GameStates newState)
    {
        _currentState = newState;
    }
    /// <summary>
    /// Methods to be called when a game state is exited
    /// </summary>
    /// <param name="newState">Exited game state</param>
    private void ExitState(GameStates newState)
    {

    }
    /// <summary>
    /// Method called to uptate the game manager according to the current state
    /// </summary>
    /// <param name="state">Current game state</param>
    private void UpdateState(GameStates state)
    {
        switch (state)
        {
            case GameStates.START:

                _UIManager.SetMenu(state);
                break;

            case GameStates.GAME:         
                _UIManager.SetMenu(state);
                if (!_player.activeSelf)
                {
                    LoadLevel();
                    _UIManager.SetUpGameHUD(_nRound, _goal, _remainingTime);
                }
               if(_current == _goal)
                {
                    UnloadLevel();
                    LoadLevel();
                    _nRound++;
                    _current = 0;
                    _UIManager.SetUpGameHUD(_nRound, _goal, _remainingTime);
                }
                if (_remainingTime <= 0)
                {
                    RequestStateChange(GameStates.GAMEOVER);
                    UnloadLevel();
                }
                break;

            case GameStates.GAMEOVER:

                _UIManager.SetMenu(state);
                break;  
        }
    }
    /// <summary>
    /// Public method for other scripts to request a game state change
    /// </summary>
    /// <param name="newState">Requested state</param>
    public void RequestStateChange(GameManager.GameStates newState)
    {
        _currentState = newState;
    }
    /// <summary>
    /// Loads a new level choosing among the available levels.
    /// Initializes player and game parameters as well.
    /// </summary>
    private void LoadLevel()
    {
        i = Random.Range(0, _levels.Length);
      
        _levelManager = _levels[i]._levelPrefab.GetComponent<LevelManager>();
        levelObject = Instantiate(_levels[i]._levelPrefab, _levels[i]._levelPrefab.transform.position, Quaternion.identity);
        _levelManager.SetPlayer(_player);
        _player.GetComponent<MovementComponent>().GoToPoint(_player.transform.position);
        _goal = _levels[i]._levelGoal;
        _remainingTime = _levels[i]._matchDuration;
        

    }
    /// <summary>
    /// Unloads the current level.
    /// Disables player as well.
    /// </summary>
    private void UnloadLevel()
    {
        _player.SetActive(false);
        DestroyImmediate(levelObject, true);
        GameObject[] plantas = GameObject.FindGameObjectsWithTag("Plant");
        foreach (GameObject planta in plantas)
            GameObject.Destroy(planta);
        GameObject[] manzanas = GameObject.FindGameObjectsWithTag("Apple");
        foreach (GameObject manzana in manzanas)
            GameObject.Destroy(manzana);


    }
    #endregion
    /// <summary>
    /// Game state initialization
    /// </summary>
    private void Start()
    {
        _UIManager = GameObject.Find("UI").GetComponent<UIManager>();
        _UIManager.SetUpGameHUD(_nRound,_goal,_remainingTime);
        _currentState = GameStates.START;
        _nRound = 1;
    }
    /// <summary>
    /// Game state transition management.
    /// Current game state update call.
    /// </summary>
    private void Update()
    {
        _remainingTime -= Time.deltaTime;
        _UIManager.UpdateGameHUD(Current, _remainingTime);
        UpdateState(_currentState);
    }
}