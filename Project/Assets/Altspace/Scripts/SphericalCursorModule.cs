using UnityEngine;

public class SphericalCursorModule : MonoBehaviour {
	// This is a sensitivity parameter that should adjust how sensitive the mouse control is.
	public float Sensitivity;

	// This is a scale factor that determines how much to scale down the cursor based on its collision distance.
	public float DistanceScaleFactor;

	// This is the layer mask to use when performing the ray cast for the objects.
	// The furniture in the room is in layer 8, everything else is not.
	private const int ColliderMask = (1 << 8);

	// This is the Cursor game object. Your job is to update its transform on each frame.
	private GameObject Cursor;

	// This is the Cursor mesh. (The sphere.)
	private MeshRenderer CursorMeshRenderer;

	// This is the scale to set the cursor to if no ray hit is found.
	private Vector3 DefaultCursorScale = new Vector3(10.0f, 10.0f, 10.0f);

	// Maximum distance to ray cast.
	private const float MaxDistance = 100.0f;

	// Sphere radius to project cursor onto if no raycast hit.
	private const float SphereRadius = 10.0f;

	// Spherical coordinates to update each frame.
	private Vector2 cursorSphericalCoords = new Vector2(0.0f, 0.0f);

  void Awake() {
		Cursor = transform.Find("Cursor").gameObject;
		CursorMeshRenderer = Cursor.transform.GetComponentInChildren<MeshRenderer>();
    CursorMeshRenderer.GetComponent<Renderer>().material.color = new Color(0.0f, 0.8f, 1.0f);
  }

	void Update()
	{
		// Handle mouse movement to update cursor position.
		float horizontal = Input.GetAxis("Mouse X");
		float vertical = Input.GetAxis("Mouse Y");

		cursorSphericalCoords.x += horizontal * Sensitivity;
		cursorSphericalCoords.y += vertical * Sensitivity;

		// Start ray cast from camera position, head in direction specified by spherical coordinates
		Vector3 origin = transform.position;
		Vector3 direction = Quaternion.AngleAxis(cursorSphericalCoords.x, Vector3.up)
											* Quaternion.AngleAxis(cursorSphericalCoords.y, Vector3.left)
											* Vector3.forward;

		// Perform ray cast to find object cursor is pointing at.
		RaycastHit cursorHit = new RaycastHit();
		bool foundHit = Physics.Raycast(origin, direction, out cursorHit, MaxDistance, ColliderMask);

		// Update cursor transform.
		// Update highlighted object based upon the raycast.
		if (foundHit && cursorHit.collider != null) {
			float distanceToObject = cursorHit.distance;
			float scale = (distanceToObject * DistanceScaleFactor + 1.0f) / 2.0f;
			
			Selectable.CurrentSelection = cursorHit.collider.gameObject;
			Cursor.transform.position = cursorHit.point;
			Cursor.transform.localScale = new Vector3(scale, scale, scale);
		} else {
			Selectable.CurrentSelection = null;
			Cursor.transform.localPosition = direction * SphereRadius;
			Cursor.transform.localScale = DefaultCursorScale;
		}
	}
}
