using UnityEngine;
using System.Collections.Generic;

public class AtlasBuilder
{
	List<Texture2D> _textures = new List<Texture2D>();
	Texture2D _atlasTexture;

	public AtlasBuilder ()
	{
		_atlasTexture = new Texture2D(1024, 1024);
		_atlasTexture.filterMode = FilterMode.Point;
		_atlasTexture.Apply();
	}

	public void AddTexture(string pTextureName)
	{
		Texture2D texture = Resources.Load(pTextureName) as Texture2D;
		if(texture == null) {
			Debug.LogError("Texture2d is null! Name: " + pTextureName);
		}
		_textures.Add(texture);
	}

	public TextureItem[] Pack()
	{
		var result = new List<TextureItem>();
		Rect[] rects = _atlasTexture.PackTextures(_textures.ToArray(), 5);

		int i = 0;
		foreach(Texture2D texture in _textures) {
			TextureItem item = new TextureItem() {
				textureName = texture.name,
				atlas = _atlasTexture,
				uvPosition = GetUvCoordinates(rects[i]),
			};
			result.Add(item);
			i++;
		}

		_textures.Clear();
		return result.ToArray();
	}

	Vector2[] GetUvCoordinates (Rect rect)
	{
		return new float[]{
			rect.x,
			rect.y,

			rect.xMax,
			rect.y,

			rect.xMax,
			rect.yMax,

			rect.x,
			rect.yMax
		}.ToVector2Array();
	}

	public Texture2D atlasTexture {
		get {
			return _atlasTexture;
		}
	}
}