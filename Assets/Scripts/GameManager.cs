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

    // Private game state vaiables
    private float CurrentTempIncrease = 0.1f; // Temporary increase value for the game state

    private EGameState GameState; // Current game state
    private enum EGameState
    {
        Play,
        Mission,
        Win,
        Lose,
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
                    StartMission(hit.collider.GetComponent<Mission>());
                }
            }        
        }
    }

    // Method to handle mission start
    void StartMission(Mission mission)
    {
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
        button.GetComponent<Button>().onClick.AddListener(MissionCorrectAnswer); // Bind the correct answer method
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
            button.GetComponent<Button>().onClick.AddListener(MissionIncorrectAnswer); // Bind the incorrect answer method
        }
        // Enable the Mission UI
        MissionUI.SetActive(true);
        // Set the game state to Mission
        GameState = EGameState.Mission;
    }

    void MissionCorrectAnswer()
    {
        // This method is called when the correct answer is selected
        Debug.Log("Correct answer selected!");
        // Enable the Mission UI
        MissionUI.SetActive(false);
        GameState = EGameState.Play; // Set the game state back to Play
    }

    void MissionIncorrectAnswer()
    {
        // This method is called when the incorrect answer is selected
        // You can add code here to handle what happens when the incorrect answer is selected
        Debug.Log("Incorrect answer selected!");
        MissionUI.SetActive(false);
        GameState = EGameState.Play; // Set the game state back to Play
    }   

}
