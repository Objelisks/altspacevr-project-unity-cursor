using UnityEngine;
using System.Collections;

public class SpawnBooks : MonoBehaviour {

	public GameObject bookObject;

	// Use this for initialization
	void Start () {
		// generate 32 books per shelf, 5 shelves
		for(int y = 0; y < 5; y++) {
			for(int x = 0; x < 32; x++) {
				GameObject book = (GameObject)Instantiate(bookObject);
				book.transform.position = this.transform.TransformVector(new Vector3(0, y+0.4f, x/6.0f - 0.15f));
				book.transform.rotation = this.transform.rotation * Quaternion.Euler(0, 90, 90);
				TextMesh text = book.GetComponentInChildren<TextMesh>();
				text.text = "";
				var chars = new char[61*32];
				for(int line = 0; line < 32; line++) {
					for(int charIndex = 0; charIndex < 60; charIndex++) {
						chars[charIndex+line*61] = (char)(Mathf.Floor(Random.Range(32, 96)));
					}
					chars[60+line*61] = '\n';
				}
				text.text = new System.String(chars);
			}
		}
	}

	// Update is called once per frame
	void Update () {

	}
}
