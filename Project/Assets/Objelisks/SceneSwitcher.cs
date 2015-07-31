using UnityEngine;
using System.Collections;

public class SceneSwitcher : MonoBehaviour {

	// Level to load when tab is pressed
	public int level;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("tab")) {
			Application.LoadLevel(level);
		}
	}
}
