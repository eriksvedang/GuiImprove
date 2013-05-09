using UnityEngine;
using System.Collections.Generic;

public class Tester : MonoBehaviour {

	Container _root;
	Batcher _batcher;

	void Start () {
		var mesh = this.GetComponent<MeshFilter>().mesh;
		_batcher = new Batcher(mesh, this.GetComponent<Camera>());

		AtlasBuilder atlasBuilder = new AtlasBuilder();

		var watch = new System.Diagnostics.Stopwatch();
		watch.Start();

		foreach(char c in "abcdefghijklmnopqrst")
		{
			atlasBuilder.AddTexture("Textures/" + c.ToString());
		}
		TextureItem[] items = atlasBuilder.Pack();

		watch.Stop();
		Debug.Log("Time to pack: " + watch.ElapsedMilliseconds);

		_batcher.AddTextureItems(items);

		renderer.material.mainTexture = atlasBuilder.atlasTexture;
	
		_root = new Container(new Vector2(0, 0));

//		Container upperLeft = new Container("Textures/b", new Vector2(0, 0));
//		upperLeft.batcher = _batcher;
//
//		Container lowerRight = new Container("Textures/b", new Vector2(Screen.width - 512f, Screen.height - 512f));
//		lowerRight.batcher = _batcher;
//
//		_root.AddChild(upperLeft);
//		_root.AddChild(lowerRight);

		for(int i = 0; i < 100; i++) {
			AddStuffToContainer(_root);
		}
	}

	void AddStuffToContainer(Container pContainer)
	{
		foreach(char c in "abcdefghijklmnopqrst")
		{
			Container newChild = new Container("Textures/" + c.ToString(), new Vector2(Random.Range(0, Screen.width), Random.Range (0, Screen.height)));
			newChild.batcher = _batcher;
			pContainer.AddChild(newChild);
		}
	}

	void Update () {
		_batcher.BeginBuildMesh();
		_root.Draw();
		_batcher.EndBuildMesh();
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(camera.ScreenToWorldPoint(new Vector3(0f, Screen.height - 0f, 1f)), 0.2f);
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height - Screen.height, 1f)), 0.2f);
	}

	void OnGUI()
	{

	}
}

