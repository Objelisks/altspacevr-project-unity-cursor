using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SpawnNeighbors : MonoBehaviour {

	// Room prefab to instantiate.
	public Transform roomPrefab;

	// Reference to player.
	public Transform player;

	// List of currently existing rooms.
	public Transform[] openRooms;

	// This method is called when the player enters any room trigger
	public void SpawnRooms(GameObject currentRoom) {
		NeighborSpawnLocation[] neighborLocations = currentRoom.GetComponentsInChildren<NeighborSpawnLocation>(false);
		foreach(NeighborSpawnLocation spawn in neighborLocations) {
			if(!Array.Exists(openRooms, t => Vector3.Distance(t.position, spawn.transform.position) < 3.0)) {
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
	}
}
