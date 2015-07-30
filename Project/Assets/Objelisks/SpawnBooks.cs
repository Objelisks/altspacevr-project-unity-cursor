using UnityEngine;
using System.Collections;

public class SpawnBooks : MonoBehaviour {

	public GameObject bookObject;

	// Use this for initialization
	void Start () {
		StartCoroutine("GenerateBooks");
	}

	// Update is called once per frame
	void Update () {

	}

	IEnumerator GenerateBooks() {
		// generate 32 books per shelf, 5 shelves
		for(int y = 0; y < 5; y++) {
			for(int x = 0; x < 16; x++) {
				GameObject book = (GameObject)Instantiate(bookObject);
				//book.transform.position = this.transform.position + new Vector3(0, y+0.4f, x/6.0f - 0.15f);
				book.transform.position = this.transform.position + this.transform.rotation * new Vector3(0, y+0.4f, x/4.0f - 2.0f);
				//book.transform.rotation = this.transform.rotation;
				book.transform.localRotation = this.transform.rotation * Quaternion.Euler(0, 90, 90);
				book.transform.parent = this.transform.root;
				Rigidbody[] physics = book.GetComponentsInChildren<Rigidbody>();
				foreach(Rigidbody body in physics) {
					body.Sleep();
				}
				/*
				TextMesh text = book.GetComponentInChildren<TextMesh>();
				var alphabet = "abcdefghijklmnopqrstuvwxyz ,.";
				// Allocate the characters first to speed things up.
				var chars = new char[61*32];
				for(int line = 0; line < 32; line++) {
					for(int charIndex = 0; charIndex < 60; charIndex++) {
						chars[charIndex+line*61] = alphabet[Random.Range(0, alphabet.Length)];
					}
					chars[60+line*61] = '\n';
				}
				text.text = new System.String(chars);
				*/
			}
			yield return null;
		}
		StopCoroutine("GenerateBooks");
	}
}
