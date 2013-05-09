using UnityEngine;
using System.Collections.Generic;

public struct TextureItem {
	public string textureName;
	public Vector2[] uvPosition;
	public Texture atlas;
}

public class Batcher {

	public Batcher(Mesh pMesh, Camera pGuiCamera){
		if(pMesh == null) {
			Debug.LogError("pMesh is null");
		}
		if(pGuiCamera == null) {
			Debug.LogError("pGuiCamera is null");
		}
		_combinedMesh = pMesh;
		_guiCamera = pGuiCamera;
	}
	
	Dictionary<string, TextureItem> _textureItems = new Dictionary<string, TextureItem>();
	Mesh _combinedMesh;
	Camera _guiCamera;

	List<Vector3> _vertices =new List<Vector3>();
	List<Vector2> _uvs =new List<Vector2>();
	List<int> _indices =new List<int>();

	float _w, _h;
	Vector3 _origo;

	///   A-----B   0----1
	///   |     |   |    |
	///   D-----C   3----2
	void AppendQuad( Vector3[] points, TextureItem pTexItem )
	{	
		//A
		_indices.Add (_vertices.Count);
		_vertices.Add(points[0]);
		_uvs.Add(pTexItem.uvPosition[0]);
		//C
		_indices.Add (_vertices.Count);
		_vertices.Add(points[2]);
		_uvs.Add(pTexItem.uvPosition[2]);
		//B
		_indices.Add (_vertices.Count);
		_vertices.Add(points[1]);
		_uvs.Add(pTexItem.uvPosition[1]);

		//A
		_indices.Add (_vertices.Count);
		_vertices.Add(points[0]);
		_uvs.Add(pTexItem.uvPosition[0]);
		//D
		_indices.Add (_vertices.Count);
		_vertices.Add(points[3]);
		_uvs.Add(pTexItem.uvPosition[3]);
		//C
		_indices.Add (_vertices.Count);
		_vertices.Add(points[2]);
		_uvs.Add(pTexItem.uvPosition[2]);

	}

	public void AddTextureItem(TextureItem pTextureItem)
	{
		_textureItems[pTextureItem.textureName] = pTextureItem;
	}

	public void AddTextureItems (TextureItem[] items)
	{
		items.ForEach(AddTextureItem);
	}

	public void BeginBuildMesh() {
		_combinedMesh.Clear();

//		var worldP = _guiCamera.ViewportToWorldPoint(Vector3.one) * 2f;
//		Vector3 size = _guiCamera.transform.worldToLocalMatrix * worldP;
//		_w = size.x;
//		_h = size.y;

		if(_guiCamera.isOrthoGraphic) {
			_h = 2.0f * _guiCamera.orthographicSize;
			_w = _h * _guiCamera.aspect;
		} else {
			float distance = 1.0f;
			_h = 2.0f * distance * Mathf.Tan(_guiCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);
			_w = _h * _guiCamera.aspect;
		}

		_origo = new Vector3(-(_w / 2), (_h / 2), 0f);
	}

	public void EndBuildMesh() {
		_combinedMesh.vertices = _vertices.ToArray();
		_combinedMesh.uv = _uvs.ToArray();
		_combinedMesh.SetIndices(_indices.ToArray(), MeshTopology.Triangles, 0);
		//_combinedMesh.RecalculateNormals();
		_vertices.Clear();
		_uvs.Clear();
		_indices.Clear();
	}

	Vector3 ScreenPointToMeshPoint(Vector3 pPoint)
	{
		float normalizedX = pPoint.x / Screen.width;
		float worldX = _origo.x + _w * normalizedX;

		float normalizedY = pPoint.y / Screen.height;
		float worldY = _origo.y - _h * normalizedY;
		
		float worldZ = pPoint.z;

		pPoint.x = worldX;
		pPoint.y = worldY;
		pPoint.z = worldZ;
		
		return pPoint;
	}

	public void Draw(string pTextureName, Vector3[] points)
	{
		points.Map(ScreenPointToMeshPoint);
		AppendQuad(points, _textureItems[pTextureName]);
	}
}

