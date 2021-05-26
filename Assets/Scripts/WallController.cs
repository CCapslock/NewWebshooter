using UnityEngine;

public class WallController : MonoBehaviour
{
	private GameObject[] _wallObjects;
	public GameObject Web;
	public void PrepearWalls()
	{
		_wallObjects = GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.Wall));
		for (int i = 0; i < _wallObjects.Length; i++)
		{
			if (_wallObjects[i].GetComponent<Wall>() == null)
			{
				Wall WallObject = _wallObjects[i].AddComponent<Wall>();
				WallObject.Web = Web;
			}
		}
		_wallObjects = GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.Buildings1));
		for (int i = 0; i < _wallObjects.Length; i++)
		{
			if (_wallObjects[i].GetComponent<Wall>() == null)
			{
				_wallObjects[i].tag = TagManager.GetTag(TagType.Wall);
				Wall WallObject = _wallObjects[i].AddComponent<Wall>();
				WallObject.Web = Web;
			}
		}
		_wallObjects = GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.Buildings2));
		for (int i = 0; i < _wallObjects.Length; i++)
		{
			if (_wallObjects[i].GetComponent<Wall>() == null)
			{
				_wallObjects[i].tag = TagManager.GetTag(TagType.Wall);
				Wall WallObject = _wallObjects[i].AddComponent<Wall>();
				WallObject.Web = Web;
			}
		}
		_wallObjects = GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.Buildings3));
		for (int i = 0; i < _wallObjects.Length; i++)
		{
			if (_wallObjects[i].GetComponent<Wall>() == null)
			{
				_wallObjects[i].tag = TagManager.GetTag(TagType.Wall);
				Wall WallObject = _wallObjects[i].AddComponent<Wall>();
				WallObject.Web = Web;
			}
		}
		_wallObjects = GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.Buildings4));
		for (int i = 0; i < _wallObjects.Length; i++)
		{
			if (_wallObjects[i].GetComponent<Wall>() == null)
			{
				_wallObjects[i].tag = TagManager.GetTag(TagType.Wall);
				Wall WallObject = _wallObjects[i].AddComponent<Wall>();
				WallObject.Web = Web;
			}
		}
		_wallObjects = GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.RoofBuilding));
		for (int i = 0; i < _wallObjects.Length; i++)
		{
			if (_wallObjects[i].GetComponent<Wall>() == null)
			{
				_wallObjects[i].tag = TagManager.GetTag(TagType.Wall);
				Wall WallObject = _wallObjects[i].AddComponent<Wall>();
				WallObject.Web = Web;
			}
		}
		_wallObjects = GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.BuildingCube));
		for (int i = 0; i < _wallObjects.Length; i++)
		{
			if (_wallObjects[i].GetComponent<Wall>() == null)
			{
				_wallObjects[i].tag = TagManager.GetTag(TagType.Wall);
				Wall WallObject = _wallObjects[i].AddComponent<Wall>();
				WallObject.Web = Web;
			}
		}
		_wallObjects = GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.BuildingConstr1));
		for (int i = 0; i < _wallObjects.Length; i++)
		{
			if (_wallObjects[i].GetComponent<Wall>() == null)
			{
				_wallObjects[i].tag = TagManager.GetTag(TagType.Wall);
				Wall WallObject = _wallObjects[i].AddComponent<Wall>();
				WallObject.Web = Web;
			}
		}
		_wallObjects = GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.BuildingConstr2));
		for (int i = 0; i < _wallObjects.Length; i++)
		{
			if (_wallObjects[i].GetComponent<Wall>() == null)
			{
				_wallObjects[i].tag = TagManager.GetTag(TagType.Wall);
				Wall WallObject = _wallObjects[i].AddComponent<Wall>();
				WallObject.Web = Web;
			}
		}
	}
}
