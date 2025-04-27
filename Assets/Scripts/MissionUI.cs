using UnityEngine;

public class MissionUI : MonoBehaviour
{
    public Mission mission; // Reference to the Mission script

    public void Completed()
    {
        // This method is called when the mission is completed
        // You can add code here to handle what happens when the mission is completed
        Debug.Log("Mission completed: " + mission.MissionName);
        Debug.Log("You earned " + mission.MissionPoints + " points!"); 
    }
}
