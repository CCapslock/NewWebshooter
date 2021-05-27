using NaughtyAttributes;
using UnityEngine;

public class ArtController : MonoBehaviour
{
	public ArtPreset[] ArtPresets;
	public bool NeedTochangeDecorativeObjects;

	private GameObject _changingObject;
	private SkinnedMeshRenderer _enemyRenderer;
	private MeshRenderer _buildingRenderer;
	private Material[] _newMaterials;
	private int _presetNum = 2;
	private string _presetNumPlayerPref = "PresetNum";


	[Button]
	public void SetMaterials()
	{
		_presetNum = PlayerPrefs.GetInt(_presetNumPlayerPref);
		ChangeEnemyesMaterial();
		ChangeBuildingsMaterial();
		if (NeedTochangeDecorativeObjects)
		{
			ChangeDecorativesMaterial();
		}
	}
	public void ChangeMaterials()
	{
		try
		{
			if (PlayerPrefs.GetInt(_presetNumPlayerPref) == ArtPresets.Length - 1)
			{
				PlayerPrefs.SetInt(_presetNumPlayerPref, 0);
			}
			else
			{
				PlayerPrefs.SetInt(_presetNumPlayerPref, PlayerPrefs.GetInt(_presetNumPlayerPref) + 1);
			}
		}
		catch
		{
			PlayerPrefs.SetInt(_presetNumPlayerPref, 0);
		}
	}
	private void ChangeEnemyesMaterial()
	{
		_changingObject = Resources.Load<GameObject>(PrefabAssetPath.LevelParts["EnemyPrefabWithRagdoll"]);
		_enemyRenderer = _changingObject.GetComponentInChildren<SkinnedMeshRenderer>();
		_newMaterials = _enemyRenderer.sharedMaterials;
		for (int i = 1; i < _newMaterials.Length; i++)
		{
			_newMaterials[i] = ArtPresets[_presetNum].EnemyMaterial;
		}
		_enemyRenderer.materials = _newMaterials;

		_changingObject = Resources.Load<GameObject>(PrefabAssetPath.LevelParts["EnemyThrowingBombs"]);
		_enemyRenderer = _changingObject.GetComponentInChildren<SkinnedMeshRenderer>();
		_newMaterials = _enemyRenderer.sharedMaterials;
		for (int i = 1; i < _newMaterials.Length; i++)
		{
			_newMaterials[i] = ArtPresets[_presetNum].EnemyMaterial;
		}
		_enemyRenderer.materials = _newMaterials;

		_changingObject = Resources.Load<GameObject>(PrefabAssetPath.LevelParts["EnemyFalling"]);
		_enemyRenderer = _changingObject.GetComponentInChildren<SkinnedMeshRenderer>();
		_newMaterials = _enemyRenderer.sharedMaterials;
		for (int i = 1; i < _newMaterials.Length; i++)
		{
			_newMaterials[i] = ArtPresets[_presetNum].EnemyMaterial;
		}
		_enemyRenderer.materials = _newMaterials;
		
		_changingObject = Resources.Load<GameObject>(PrefabAssetPath.LevelParts["EnemyPrefabWithShield"]);
		_enemyRenderer = _changingObject.GetComponentInChildren<SkinnedMeshRenderer>();
		_newMaterials = _enemyRenderer.sharedMaterials;
		for (int i = 1; i < _newMaterials.Length; i++)
		{
			_newMaterials[i] = ArtPresets[_presetNum].EnemyMaterial;
		}
		_enemyRenderer.materials = _newMaterials;

		_changingObject = Resources.Load<GameObject>(PrefabAssetPath.LevelParts["EnemyPrefabDodge"]);
		_enemyRenderer = _changingObject.GetComponentInChildren<SkinnedMeshRenderer>();
		_newMaterials = _enemyRenderer.sharedMaterials;
		for (int i = 1; i < _newMaterials.Length; i++)
		{
			_newMaterials[i] = ArtPresets[_presetNum].EnemyMaterial;
		}
		_enemyRenderer.materials = _newMaterials;
	}
	private void ChangeBuildingsMaterial()
	{
		_changingObject = Resources.Load<GameObject>(PrefabAssetPath.LevelParts["PrepearedBuilding1"]);
		_buildingRenderer = _changingObject.GetComponentInChildren<MeshRenderer>();
		_newMaterials = _buildingRenderer.sharedMaterials;
		_newMaterials[0] = ArtPresets[_presetNum].BuildingMaterial;
		_newMaterials[1] = ArtPresets[_presetNum].WindowsMaterial;
		_buildingRenderer.materials = _newMaterials;

		_changingObject = Resources.Load<GameObject>(PrefabAssetPath.LevelParts["PrepearedBuilding2"]);
		_buildingRenderer = _changingObject.GetComponentInChildren<MeshRenderer>();
		_newMaterials = _buildingRenderer.sharedMaterials;
		_newMaterials[0] = ArtPresets[_presetNum].BuildingMaterial;
		_newMaterials[1] = ArtPresets[_presetNum].WindowsMaterial;
		_buildingRenderer.materials = _newMaterials;

		_changingObject = Resources.Load<GameObject>(PrefabAssetPath.LevelParts["PrepearedBuilding3"]);
		_buildingRenderer = _changingObject.GetComponentInChildren<MeshRenderer>();
		_newMaterials = _buildingRenderer.sharedMaterials;
		_newMaterials[0] = ArtPresets[_presetNum].BuildingMaterial;
		_newMaterials[1] = ArtPresets[_presetNum].WindowsMaterial;
		_buildingRenderer.materials = _newMaterials;

		_changingObject = Resources.Load<GameObject>(PrefabAssetPath.LevelParts["PrepearedBuilding4"]);
		_buildingRenderer = _changingObject.GetComponentInChildren<MeshRenderer>();
		_newMaterials = _buildingRenderer.sharedMaterials;
		_newMaterials[0] = ArtPresets[_presetNum].BuildingMaterial;
		_newMaterials[1] = ArtPresets[_presetNum].WindowsMaterial;
		_buildingRenderer.materials = _newMaterials;

		_changingObject = Resources.Load<GameObject>(PrefabAssetPath.LevelParts["PrepearedRoofBuilding"]);
		_buildingRenderer = _changingObject.GetComponentInChildren<MeshRenderer>();
		_newMaterials = _buildingRenderer.sharedMaterials;
		_newMaterials[0] = ArtPresets[_presetNum].BuildingMaterial;
		_newMaterials[1] = ArtPresets[_presetNum].WindowsMaterial;
		_buildingRenderer.materials = _newMaterials;

		_changingObject = Resources.Load<GameObject>(PrefabAssetPath.LevelParts["PrepearedBuildingConstr1"]);
		_buildingRenderer = _changingObject.GetComponentInChildren<MeshRenderer>();
		_newMaterials = _buildingRenderer.sharedMaterials;
		_newMaterials[0] = ArtPresets[_presetNum].BuildingMaterial;
		_buildingRenderer.materials = _newMaterials;

		_changingObject = Resources.Load<GameObject>(PrefabAssetPath.LevelParts["PrepearedBuildingConstr2"]);
		_buildingRenderer = _changingObject.GetComponentInChildren<MeshRenderer>();
		_newMaterials = _buildingRenderer.sharedMaterials;
		_newMaterials[0] = ArtPresets[_presetNum].BuildingMaterial;
		_buildingRenderer.materials = _newMaterials;

		_changingObject = Resources.Load<GameObject>(PrefabAssetPath.LevelParts["BuildingCube"]);
		_buildingRenderer = _changingObject.GetComponentInChildren<MeshRenderer>();
		_newMaterials = _buildingRenderer.sharedMaterials;
		_newMaterials[0] = ArtPresets[_presetNum].BuildingMaterial;
		_buildingRenderer.materials = _newMaterials;

		_changingObject = Resources.Load<GameObject>(PrefabAssetPath.LevelParts["BankBuilding"]);
		MeshRenderer[] tempRenderers = _changingObject.GetComponentsInChildren<MeshRenderer>();
		_newMaterials = new Material[tempRenderers.Length];
		Debug.Log("tempRenderers " + tempRenderers.Length);
		for (int i = 0; i < 5; i++)
		{
			tempRenderers[i].sharedMaterial = ArtPresets[_presetNum].BuildingMaterial;
		}
		//_newMaterials[0] = ArtPresets[_presetNum].BuildingMaterial;
		//_buildingRenderer.materials = _newMaterials;
	}
	private void ChangeDecorativesMaterial()
	{
		_changingObject = Resources.Load<GameObject>(PrefabAssetPath.LevelParts["PrepearedDecorativeBench"]);
		_buildingRenderer = _changingObject.GetComponentInChildren<MeshRenderer>();
		_newMaterials = _buildingRenderer.sharedMaterials;
		_newMaterials[0] = ArtPresets[_presetNum].BuildingMaterial;
		_newMaterials[1] = ArtPresets[_presetNum].WindowsMaterial;
		_buildingRenderer.materials = _newMaterials;

		_changingObject = Resources.Load<GameObject>(PrefabAssetPath.LevelParts["PrepearedDecorativeCrane"]);
		_buildingRenderer = _changingObject.GetComponentInChildren<MeshRenderer>();
		_newMaterials = _buildingRenderer.sharedMaterials;
		_newMaterials[0] = ArtPresets[_presetNum].BuildingMaterial;
		_buildingRenderer.materials = _newMaterials;

		_changingObject = Resources.Load<GameObject>(PrefabAssetPath.LevelParts["PrepearedDecorativeVoltageWires"]);
		_buildingRenderer = _changingObject.GetComponentInChildren<MeshRenderer>();
		_newMaterials = _buildingRenderer.sharedMaterials;
		_newMaterials[0] = ArtPresets[_presetNum].BuildingMaterial;
		_newMaterials[1] = ArtPresets[_presetNum].WindowsMaterial;
		_buildingRenderer.materials = _newMaterials;

		_changingObject = Resources.Load<GameObject>(PrefabAssetPath.LevelParts["PrepearedDecorativeWoodShield"]);
		_buildingRenderer = _changingObject.GetComponentInChildren<MeshRenderer>();
		_newMaterials = _buildingRenderer.sharedMaterials;
		_newMaterials[0] = ArtPresets[_presetNum].BuildingMaterial;
		_newMaterials[1] = ArtPresets[_presetNum].WindowsMaterial;
		_buildingRenderer.materials = _newMaterials;
	}
	private void ChangeWebMaterial()
	{
		_changingObject = Resources.Load<GameObject>(PrefabAssetPath.LevelParts["WebSphere"]);
		_buildingRenderer = _changingObject.GetComponentInChildren<MeshRenderer>();
		_newMaterials = _buildingRenderer.sharedMaterials;
		_newMaterials[0] = ArtPresets[_presetNum].WebMaterial;
		_buildingRenderer.materials = _newMaterials;


		_changingObject = Resources.Load<GameObject>(PrefabAssetPath.LevelParts["WebModel"]);
		_buildingRenderer = _changingObject.GetComponentInChildren<MeshRenderer>();
		_newMaterials = _buildingRenderer.sharedMaterials;
		_newMaterials[0] = ArtPresets[_presetNum].WebMaterial;
		_buildingRenderer.materials = _newMaterials;
	}
}
