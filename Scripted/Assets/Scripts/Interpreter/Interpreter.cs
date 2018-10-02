using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interpreter : MonoBehaviour
{
    [SerializeField]
    private Text script;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Run()
    {
        if (script.text.Length > 0)
        {

        }
        else
        {
            // TODO: Add pop-up or in-game feedback
            Debug.LogWarning("Script error!");
        }
    }
}
