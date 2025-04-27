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

    // Game state variables
    private float TempIncrease = 0.1f; // Temporary increase value for the game state
    private float CurrentTemp = 40.0f; // Current temperature value for the game state

    // Update is called once per frame
    void Update()
    {
        // Input handling

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
        MissionUI.GetComponent<Transform>().GetChild(buttonIndex + correctAnswerIndex)
            .GetComponent<Transform>().GetChild(0).GetComponent<TMP_Text>().text
            = mission.CorrectAnswer;
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
            MissionUI.GetComponent<Transform>().GetChild(i + buttonIndex)
                .GetComponent<Transform>().GetChild(0).GetComponent<TMP_Text>().text
                = mission.IncorrectAnswers[incorrectAnswerIndex];
        }
        // Enable the Mission UI
        MissionUI.SetActive(true);
    }

    // Method to handle mission completion 
    void CompleteMission(Mission mission)
    {
        // This method is called when a mission is completed
        // You can add code here to handle what happens when the mission is completed
        Debug.Log("Mission completed: " + mission.MissionName);
        Debug.Log("You earned " + mission.MissionPoints + " points!"); 
    }
}
