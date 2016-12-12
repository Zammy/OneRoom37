using UnityEngine;

public class JoystickPrint : MonoBehaviour 
{
	void Start () 
    {
	    var joysticks = Input.GetJoystickNames();
        foreach (var i in joysticks)
        {
            Debug.Log(i);    
        }

	}

}
