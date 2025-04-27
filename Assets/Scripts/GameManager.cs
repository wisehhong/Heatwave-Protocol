using UnityEngine;
using TMPro; // Importing TextMeshPro namespace for text handling
using UnityEngine.UI; // Importing Unity UI namespace for UI handling

using System.Collections.Generic; // Importing generic collections for using lists and arrays

// Main game manager script that handles the game state and input
// This script is responsible for managing the game state, handling input, and coordinating between different game components
public class GameManager : MonoBehaviour
{
    // Reference to GameObjects
    public GameObject Background; // Reference to the background GameObject
    public GameObject Missions; // Reference to the player GameObject
    
    // Reference to UI elements
    public GameObject MissionUI; // Reference to the Mission UI prefab

    // Public game state vaiables
    public float GameTime = 0.0f; // Game time variable
    public float GameTimeLimit = 180.0f; // Game time limit variable
    public float InitialTempIncrease = 0.0f; // Initial increase value for the game state
    public float MinTempIncrease = -4.5f; // Minimum temperature increase value for the game state (F)
    public float MaxTempIncrease = 4.5f; // Maximum temperature increase value for the game state (F)
    public float TempIncreasePerMission = 0.1f; // Temperature increase value for each mission (F)

    // Private game state vaiables
    private float CurrentTempIncrease = 0.0f; // Temporary increase value for the game state

    private EGameState GameState; // Current game state
    private enum EGameState
    {
        Play,
        Mission,
        Win,
        Lose,
    }

    void Start()
    {
        // Initialize the game state to Play
        GameState = EGameState.Play;
        // Set the initial temperature increase value
        CurrentTempIncrease = InitialTempIncrease;
        // Set mission GameObjects to active at the start
        foreach (Transform child in Missions.transform)
        {
            child.gameObject.SetActive(true); // Set each mission GameObject to active
            CurrentTempIncrease += TempIncreasePerMission; // Increase the temperature value for each mission GameObject
        }
        // Set the Mission UI to inactive at the start
        if (MissionUI != null)
        {
            MissionUI.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Handle input only for the Play state
        // Other states will not respond to input since UI system will be active
        if (GameState == EGameState.Play)
        {
            // Left mouse click
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

                if (hit.collider != null)
                {
                    StartMission(hit.collider.gameObject); // Start the mission if a collider is hit
                }
            }        
        }

        // Visual Update
        // Update thermometer value based on the current temperature increase
        // if (Background != null)
        // {
        //     Background.GetComponent<Transform>().GetChild(0).GetComponent<TMP_Text>().text = "Temperature: " + CurrentTempIncrease.ToString("F1") + "°F"; // Update the thermometer value in the UI
        // }
        // Update backgournd color based on the current temperature increase
        if (Background != null)
        {
            // Clamp the color value between 0 and 1
            float colorValue = Mathf.Clamp(CurrentTempIncrease / MaxTempIncrease, 0.0f, 1.0f);
             // Update the background color based on the temperature increase
            Background.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f - colorValue, 1.0f - colorValue);
        }
    }

    // Method to handle mission start
    void StartMission(GameObject gameObject)
    {
        Mission mission = gameObject.GetComponent<Mission>(); // Get the Mission component from the GameObject
         // Check if mission is null
        if (mission == null)
        {
            Debug.LogError("Mission is null");
            return;
        }
        Debug.Log("Mission started: " + mission.MissionName);
        // Show Mission UI and set the mission
        if (MissionUI == null)
        {
            Debug.LogError("Mission UI prefab is not assigned in the GameManager.");
            return;
        }
        // Set the mission description in the UI
        MissionUI.GetComponent<Transform>().GetChild(1).GetComponent<TMP_Text>().text = mission.MissionDescription;
        int correctAnswerIndex = mission.ShuffleCorrectAnswerIndex(); // Shuffle the correct answer index
        int buttonIndex = 2; // Start from the first button index
        // Set the correct answer in the UI
        Debug.Log("Binding correct answer: " + correctAnswerIndex);
        var button = MissionUI.GetComponent<Transform>().GetChild(buttonIndex + correctAnswerIndex);
        button.GetComponent<Transform>().GetChild(0).GetComponent<TMP_Text>().text = mission.CorrectAnswer;
        button.GetComponent<Button>().onClick.RemoveAllListeners(); // Remove all listeners to avoid duplicate bindings
        button.GetComponent<Button>().onClick.AddListener(delegate{MissionCorrectAnswer(gameObject);}); // Bind the correct answer method
        // Set the incorrect answers in the UI 
        for (int i = 0; i < mission.IncorrectAnswers.Count + 1; i++)
        {
            int incorrectAnswerIndex = i;
            if (i == correctAnswerIndex)
            {
                continue; // Skip the correct answer index
            }
            else if (i > correctAnswerIndex)
            {
                incorrectAnswerIndex = i - 1; // Adjust the index for incorrect answers
            }
            // Set the incorrect answer in the UI
            Debug.Log("Binding incorrect answer: " + i);
            button = MissionUI.GetComponent<Transform>().GetChild(i + buttonIndex);
            button.GetComponent<Transform>().GetChild(0).GetComponent<TMP_Text>().text = mission.IncorrectAnswers[incorrectAnswerIndex];
            button.GetComponent<Button>().onClick.RemoveAllListeners(); // Remove all listeners to avoid duplicate bindings
            button.GetComponent<Button>().onClick.AddListener(delegate{MissionIncorrectAnswer(gameObject);}); // Bind the incorrect answer method
        }
        // Enable the Mission UI
        MissionUI.SetActive(true);
        // Set the game state to Mission
        GameState = EGameState.Mission;
    }

    void MissionCorrectAnswer(GameObject gameObject)
    {
        // This method is called when the correct answer is selected
        Debug.Log("Correct answer selected!");
        Mission mission = gameObject.GetComponent<Mission>(); // Get the Mission component from the GameObject
        if (mission == null)
        {
            Debug.LogError("Mission is null");
            return;
        }

        // Hide the Mission GameObject
        gameObject.SetActive(false);
        // Hide the Mission UI
        MissionUI.SetActive(false);
        // Set the game state back to Play
        GameState = EGameState.Play;
        // Decrease the temperature value for each correct answer
        CurrentTempIncrease -= TempIncreasePerMission; 
        Debug.Log("CurrentTempIncrease = " + CurrentTempIncrease.ToString("F1") + "°F");
    }

    void MissionIncorrectAnswer(GameObject gameObject)
    {
        // This method is called when the incorrect answer is selected
        Debug.Log("Incorrect answer selected!");
        Mission mission = gameObject.GetComponent<Mission>(); // Get the Mission component from the GameObject
        if (mission == null)
        {
            Debug.LogError("Mission is null");
            return;
        }
        // Hide the Mission UI
        MissionUI.SetActive(false);
        // Set the game state back to Play
        GameState = EGameState.Play;
        // Increase the temperature value for each incorrect answer
        CurrentTempIncrease += TempIncreasePerMission; 
        Debug.Log("CurrentTempIncrease = " + CurrentTempIncrease.ToString("F1") + "°F");
    }   

}
