using UnityEngine;

public class JoystickPrint : MonoBehaviour 
{
	void Start () 
    {
	    var joysticks = Input.GetJoystickNames();
        Debug.Log(joysticks);
	}

}
