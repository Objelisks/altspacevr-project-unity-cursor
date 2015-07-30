using UnityEngine;
using System.Collections;

public class NotifyRoomLoader : MonoBehaviour {

	// Reference to RoomLoader object.
 	public SpawnNeighbors roomLoader;

	void Start() {
		roomLoader = GameObject.Find("RoomLoader").GetComponent<SpawnNeighbors>();
	}

	void OnTriggerEnter(Collider other) {
		// If the player enters a new room
		if(other.tag == "Player") {
			roomLoader.SendMessage("SpawnRooms", this.transform.parent.gameObject);
		}
	}
}
