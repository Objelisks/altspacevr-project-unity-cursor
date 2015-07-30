using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SpawnNeighbors : MonoBehaviour {

	// Room prefab to instantiate.
	public Transform roomPrefab;

	// Distance to start removing rooms when they are too far away.
	public float cutoffDistance;

	// Reference to player.
	public Transform player;

	// Reference to existing room in scene.
	public Transform initialRoom;

	// List of currently existing rooms.
	private List<Transform> openRooms = new List<Transform>();

	private bool doonce;

	// Use this for initialization
	void Start () {
		openRooms.Add(initialRoom);
    doonce = false;
	}

	public void SpawnRooms(GameObject currentRoom) {
		if(doonce) {
			return;
		}
		/*
		// Trying having objects already exist and just moving them around
		NeighborSpawn[] neighborLocations = currentRoom.GetComponentsInChildren<NeighborSpawn>(false);
		foreach(NeighborSpawn spawn in neighborLocations) {
			if(!openRooms.Exists(t => Vector3.Distance(t.position, spawn.transform.position) < 3.0)) {
				Transform furthest = null;
				float dist = -1.0f;
				foreach(Transform room in openRooms) {
					var thisDist = Vector3.Distance(room.position, currentRoom.transform.position);
					if(thisDist > dist) {
						furthest = room;
						dist = thisDist;
					}
				}

				if(furthest != null) {
					furthest.transform.position = spawn.transform.position;
					furthest.transform.rotation = spawn.transform.rotation;
				}
			}
		}
		*/

		// Spawn new rooms in neighbor locations of the current room.
		NeighborSpawn[] neighborLocations = currentRoom.GetComponentsInChildren<NeighborSpawn>(false);
		foreach(NeighborSpawn spawn in neighborLocations) {
			if(!openRooms.Exists(t => Vector3.Distance(t.position, spawn.transform.position) < 3.0)) {
				var newRoom = GameObject.Instantiate(roomPrefab);
				newRoom.transform.position += spawn.transform.position;
				newRoom.transform.rotation *= spawn.transform.rotation;
				openRooms.Add(newRoom);
			}
		}

		// Go through all rooms and remove any that are too far away.
		for(int i = openRooms.Count-1; i >= 0; i--) {
			Transform room = openRooms[i];
			if(Vector3.Distance(room.position, player.position) > cutoffDistance) {
				openRooms.RemoveAt(i);
				Destroy(room.gameObject);
			}
		}

	}
}
