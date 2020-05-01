using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour
{
	
	public GameEvent GenerateRandomEvent()
	{
		return new GameEvent();
	}
}


