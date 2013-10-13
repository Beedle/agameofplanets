using UnityEngine;
using System.Collections;

public class PathGuide : MonoBehaviour {
	public Body mBody;
	public Texture mTexture;
	
	private Vector3[] mVertices = new Vector3[128];
	private Vector2[] mTexCoord = new Vector2[128];
	private Vector3[] mNormals = new Vector3[128];
	private int[] mIndices = new int[378];
	private Mesh mMesh;
	
	void Start () {
		mMesh = new Mesh();
		GetComponent<MeshFilter>().mesh = mMesh;
		
		// Prepare dummy vertices
		for (int i=0; i<128; i++) {
			mVertices[i] = new Vector3(0f, 0f, 0f);	
		}
		
		// Set the UV coordinates
		for (int i=0; i<32; i++) {
			mTexCoord[i*4+0] = new Vector2(0f, 0f);
			mTexCoord[i*4+1] = new Vector2(0f, 1f);
			mTexCoord[i*4+2] = new Vector2(1f, 0f);
			mTexCoord[i*4+3] = new Vector2(1f, 1f);
		}
		
		// Set the normals
		for (int i=0; i<128; i++) {
			mNormals[i] = new Vector3(0f, 0f, -1f);	
		}
		
		// Set the indices
		for (int i=0; i<63; i++) {
			mIndices[i*6 + 0] = i+0;
			mIndices[i*6 + 1] = i+1;
			mIndices[i*6 + 2] = i+2;
			
			mIndices[i*6 + 3] = i+2;
			mIndices[i*6 + 4] = i+1;
			mIndices[i*6 + 5] = i+3;
		}
		
		mMesh.vertices = mVertices;
		mMesh.uv = mTexCoord;
		mMesh.normals = mNormals;
		mMesh.triangles = mIndices;
	}
	
	void Update () {
		Vector3 position = new Vector3(0f, 0f, 0f);
		Vector3 prevPosition = position;
		prevPosition.x -= 0.1f;
		
		Vector2 velocity = GetInitialVelocity();
		
		// Calculate the vertices
		for (int i=0; i<64; i++) {
			Vector3 diff = position - prevPosition;
			diff.Normalize();
			//diff *= 5f;
			
			// Set the vertex positions
			mVertices[i*2+0] = position;
			mVertices[i*2+0].x += diff.y;
			mVertices[i*2+0].y -= diff.x;
			
			mVertices[i*2+1] = position;
			mVertices[i*2+1].x -= diff.y;
			mVertices[i*2+1].y += diff.x;
			
			Vector2 accel = mBody.GetAccelerationOfBody(transform.position+position, 1f);
			prevPosition = position;
			
			velocity += accel * 0.1f;
			position.x += velocity.x * 0.1f;
			position.y += velocity.y * 0.1f;
		}
		
		mMesh.vertices = mVertices;
	}
	
	private Vector2 GetInitialVelocity() {
		float rotZ = 120f;
		
		// Get the aimed direction
		Aim aim = transform.parent.GetComponentInChildren<Aim>();
		if (aim != null) {
			rotZ = aim.transform.rotation.eulerAngles.z + 90f;
		}
		
		Quaternion rotation = Quaternion.Euler(0f, rotZ, 0f);
		Vector2 velocity = mBody.Velocity;
		
		velocity.x += Mathf.Cos(Mathf.Deg2Rad * rotZ) * 100f;
		velocity.y += Mathf.Sin(Mathf.Deg2Rad * rotZ) * 100f;
		
		return velocity;
	}
}
