using UnityEngine;
using System.Collections;

public class SpawnBooks : MonoBehaviour {

	// Book prefab to spawn
	public GameObject bookObject;

	// Use this for initialization
	void Start () {
		StartCoroutine("GenerateBooks");
	}

	IEnumerator GenerateBooks() {
		// generate 32 books per shelf, 5 shelves
		for(int y = 0; y < 5; y++) {
			for(int x = 0; x < 16; x++) {
				GameObject book = (GameObject)Instantiate(bookObject);
				book.transform.position = this.transform.position
					+ this.transform.rotation * new Vector3(0, y+0.35f, x/4.0f - 2.0f);
				book.transform.localRotation = this.transform.rotation * Quaternion.Euler(0, 90, 90);
				book.transform.parent = this.transform.root;
				Rigidbody[] bodies = book.GetComponentsInChildren<Rigidbody>();

				// Make each book sleep initially to increase performance.
				foreach(Rigidbody body in bodies) {
					body.Sleep();
				}
			}

			// Do one shelf at a time to spread work out over a few frames.
			yield return null;
		}
		StopCoroutine("GenerateBooks");
	}
}
