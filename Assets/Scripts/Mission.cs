using UnityEngine;
using System.Collections.Generic; // Importing generic collections for using lists and arrays

public class Mission : MonoBehaviour
{
    // Name of the mission
    public string MissionName = "Mission Name";
    // Description of the mission
    public string MissionDescription = "Mission Description";
    public string CorrectAnswer = "Correct Answer"; // The correct answer for the mission
    public List<string> IncorrectAnswers = new List<string>(3); // The incorrect answer for the mission
    public float MissionPoints;

    private int CorrectAnswerIndex = 0; // The index of the correct answer in the list of answers

    private void OnValidate()
    {
        if (IncorrectAnswers.Count < 3)
        {
            // Ensure there are at least 3 incorrect answers
            while (IncorrectAnswers.Count < 3)
            {
                IncorrectAnswers.Add("Incorrect Answer " + (IncorrectAnswers.Count + 1));
            }
        }
        if (IncorrectAnswers.Count > 3)
        {
            IncorrectAnswers.RemoveRange(3, IncorrectAnswers.Count - 3);
        }
    }

    public int ShuffleCorrectAnswerIndex()
    {
        // Set the index of the correct answer in the list of answers
        CorrectAnswerIndex = Random.Range(0, IncorrectAnswers.Count + 1);
        return CorrectAnswerIndex;
    }
}
