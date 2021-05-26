using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LVL_number_1", menuName = "Create LVL", order = 0)]
public class LevelCreator : ScriptableObject
{
	[InfoBox(" нопка 'BuildScene' чисто гл€нуть все ли правильно спавнитс€ (не забудь почистить сцену)", EInfoBoxType.Warning)]
	public CustomTransform[] Buildings1Transforms;
	public CustomTransform[] Buildings2Transforms;
	public CustomTransform[] Buildings3Transforms;
	public CustomTransform[] Buildings4Transforms;
	public CustomTransform[] RoofBuildingTransforms;
	public CustomTransform[] BankBuildingsTransforms;
	public CustomTransform[] BuildingCubeTransforms;
	public CustomTransform[] SimpleEnemyTransforms;
	public CustomTransform[] ThrowingEnemyTransforms;
	public CustomTransform[] EnemyWithShieldTransforms;
	public CustomBossSetParametrs BossSetParametrs;
	public MovementPointForBuilder[] MovementPoints;
	public bool IsMinion;
	[ShowIf("IsMinion")]
	public bool IsJoker;
	[ShowIf("IsMinion")]
	public bool IsMisterio;
	[ShowIf("IsMinion")]
	public bool IsGoblin;

	[Button]
	public void PrepareArrays()
	{
		Buildings1Transforms = new CustomTransform[GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.Buildings1)).Length];
		Buildings2Transforms = new CustomTransform[GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.Buildings2)).Length];
		Buildings3Transforms = new CustomTransform[GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.Buildings3)).Length];
		Buildings4Transforms = new CustomTransform[GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.Buildings4)).Length];
		RoofBuildingTransforms = new CustomTransform[GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.RoofBuilding)).Length];
		BankBuildingsTransforms = new CustomTransform[GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.BankBuilding)).Length];
		BuildingCubeTransforms = new CustomTransform[GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.BuildingCube)).Length];
		SimpleEnemyTransforms = new CustomTransform[GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.SimpleEnemy)).Length];
		ThrowingEnemyTransforms = new CustomTransform[GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.ThrowingEnemy)).Length];
		EnemyWithShieldTransforms = new CustomTransform[GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.ShieldEnemy)).Length];
		MovementPoints = new MovementPointForBuilder[GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.MovingPoint)).Length];
		BossSetParametrs = new CustomBossSetParametrs();
	}
	[Button]
	public void ScanScene()
	{
		FindBuildings1(GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.Buildings1)));
		FindBuildings2(GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.Buildings2)));
		FindBuildings3(GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.Buildings3)));
		FindBuildings4(GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.Buildings4)));
		FindRoofBuildings(GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.RoofBuilding)));
		FindBankBuildings(GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.BankBuilding)));
		FindBuildingCubes(GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.BuildingCube)));
		FindSimpleEnemyes(GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.SimpleEnemy)));
		FindThrowingEnemyes(GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.ThrowingEnemy)));
		FindShieldEnemyes(GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.ShieldEnemy)));
		FindBossSet(GameObject.FindGameObjectWithTag(TagManager.GetTag(TagType.BossSet)));
		ScanMovingPoints(GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.MovingPoint)));
	}
	private void FindBossSet(GameObject gameObject)
	{
		if (gameObject != null)
		{
			BossSetParametrs.IsActive = true;
			BossSetParametrs.Transform.Position = gameObject.transform.position;
			BossSetParametrs.Transform.Rotation = gameObject.transform.rotation;
			BossSetParametrs.Transform.Scale = gameObject.transform.localScale;
		}
	}
	private void FindBuildings1(GameObject[] gameObjects)
	{
		for (int i = 0; i < gameObjects.Length; i++)
		{
			Buildings1Transforms[i].Position = gameObjects[i].transform.position;
			Buildings1Transforms[i].Rotation = gameObjects[i].transform.rotation;
			Buildings1Transforms[i].Scale = gameObjects[i].transform.localScale;
		}
	}
	private void FindBuildings2(GameObject[] gameObjects)
	{
		for (int i = 0; i < gameObjects.Length; i++)
		{
			Buildings2Transforms[i].Position = gameObjects[i].transform.position;
			Buildings2Transforms[i].Rotation = gameObjects[i].transform.rotation;
			Buildings2Transforms[i].Scale = gameObjects[i].transform.localScale;
		}
	}
	private void FindBuildings3(GameObject[] gameObjects)
	{
		for (int i = 0; i < gameObjects.Length; i++)
		{
			Buildings3Transforms[i].Position = gameObjects[i].transform.position;
			Buildings3Transforms[i].Rotation = gameObjects[i].transform.rotation;
			Buildings3Transforms[i].Scale = gameObjects[i].transform.localScale;
		}
	}
	private void FindBuildings4(GameObject[] gameObjects)
	{
		for (int i = 0; i < gameObjects.Length; i++)
		{
			Buildings4Transforms[i].Position = gameObjects[i].transform.position;
			Buildings4Transforms[i].Rotation = gameObjects[i].transform.rotation;
			Buildings4Transforms[i].Scale = gameObjects[i].transform.localScale;
		}
	}
	private void FindRoofBuildings(GameObject[] gameObjects)
	{
		for (int i = 0; i < gameObjects.Length; i++)
		{
			RoofBuildingTransforms[i].Position = gameObjects[i].transform.position;
			RoofBuildingTransforms[i].Rotation = gameObjects[i].transform.rotation;
			RoofBuildingTransforms[i].Scale = gameObjects[i].transform.localScale;
		}
	}
	private void FindBankBuildings(GameObject[] gameObjects)
	{
		for (int i = 0; i < gameObjects.Length; i++)
		{
			BankBuildingsTransforms[i].Position = gameObjects[i].transform.position;
			BankBuildingsTransforms[i].Rotation = gameObjects[i].transform.rotation;
			BankBuildingsTransforms[i].Scale = gameObjects[i].transform.localScale;
		}
	}
	private void FindBuildingCubes(GameObject[] gameObjects)
	{
		for (int i = 0; i < gameObjects.Length; i++)
		{
			BuildingCubeTransforms[i].Position = gameObjects[i].transform.position;
			BuildingCubeTransforms[i].Rotation = gameObjects[i].transform.rotation;
			BuildingCubeTransforms[i].Scale = gameObjects[i].transform.localScale;
		}
	}
	private void FindSimpleEnemyes(GameObject[] gameObjects)
	{
		for (int i = 0; i < gameObjects.Length; i++)
		{
			SimpleEnemyTransforms[i].Position = gameObjects[i].transform.position;
			SimpleEnemyTransforms[i].Rotation = gameObjects[i].transform.rotation;
			SimpleEnemyTransforms[i].Scale = gameObjects[i].transform.localScale;
		}
	}
	private void FindThrowingEnemyes(GameObject[] gameObjects)
	{
		for (int i = 0; i < gameObjects.Length; i++)
		{
			ThrowingEnemyTransforms[i].Position = gameObjects[i].transform.position;
			ThrowingEnemyTransforms[i].Rotation = gameObjects[i].transform.rotation;
			ThrowingEnemyTransforms[i].Scale = gameObjects[i].transform.localScale;
		}
	}
	private void FindShieldEnemyes(GameObject[] gameObjects)
	{
		for (int i = 0; i < gameObjects.Length; i++)
		{
			EnemyWithShieldTransforms[i].Position = gameObjects[i].transform.position;
			EnemyWithShieldTransforms[i].Rotation = gameObjects[i].transform.rotation;
			EnemyWithShieldTransforms[i].Scale = gameObjects[i].transform.localScale;
		}
	}
	private void ScanMovingPoints(GameObject[] gameObjects)
	{
		for (int i = 0; i < gameObjects.Length; i++)
		{
			MovementPoints[i].Transform.Position = gameObjects[i].transform.position;
			MovementPoints[i].Transform.Rotation = gameObjects[i].transform.rotation;
			MovementPoints[i].Transform.Scale = gameObjects[i].transform.localScale;
			MovementPoints TempMovementPoint = gameObjects[i].GetComponent<MovementPoints>();
			MovementPoints[i].NeedToRotate = TempMovementPoint.NeedToRotate;
			MovementPoints[i].NeedToFly = TempMovementPoint.NeedToFly;
			MovementPoints[i].NeedToCountEnemy = TempMovementPoint.NeedToCountEnemy;
			MovementPoints[i].NeedToChangeSpeed = TempMovementPoint.NeedToChangeSpeed;
			MovementPoints[i].NewSpeed = TempMovementPoint.NewSpeed;
			MovementPoints[i].RotationVector = TempMovementPoint.RotationVector;
			MovementPoints[i].PointNum = TempMovementPoint.PointNum;
			MovementPoints[i].IsFinalPoint = TempMovementPoint.IsFinalPoint;
			MovementPoints[i].IsBossBattle = TempMovementPoint.IsBossBattle;
			MovementPoints[i].EnemyTransforms = new CustomTransform[TempMovementPoint.Enemyes.Length];

		}
	}
	[Button]
	public void ScanMovementPoints()
	{
		GameObject[] PointsGameObjects = GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.MovingPoint));
		for (int i = 0; i < MovementPoints.Length; i++)
		{
			for (int j = 0; j < MovementPoints[i].EnemyTransforms.Length; j++)
			{
				MovementPoints TempMovementPoint = PointsGameObjects[i].GetComponent<MovementPoints>();
				MovementPoints[i].EnemyTransforms[j].Position = TempMovementPoint.Enemyes[j].transform.position;
				MovementPoints[i].EnemyTransforms[j].Rotation = TempMovementPoint.Enemyes[j].transform.rotation;
				MovementPoints[i].EnemyTransforms[j].Scale = TempMovementPoint.Enemyes[j].transform.localScale;
			}
		}
	}

	[Button]
	public void BuildScene()
	{
		for (int i = 0; i < Buildings1Transforms.Length; i++)
		{
			Instantiate(Resources.Load<GameObject>(PrefabAssetPath.LevelParts["PrepearedBuilding1"]), Buildings1Transforms[i].Position, Buildings1Transforms[i].Rotation).transform.localScale = Buildings1Transforms[i].Scale;
		}
		for (int i = 0; i < Buildings2Transforms.Length; i++)
		{
			Instantiate(Resources.Load<GameObject>(PrefabAssetPath.LevelParts["PrepearedBuilding2"]), Buildings2Transforms[i].Position, Buildings2Transforms[i].Rotation).transform.localScale = Buildings2Transforms[i].Scale;
		}
		
		for (int i = 0; i < BankBuildingsTransforms.Length; i++)
		{
			Instantiate(Resources.Load<GameObject>(PrefabAssetPath.LevelParts["BankBuilding"]), BankBuildingsTransforms[i].Position, BankBuildingsTransforms[i].Rotation).transform.localScale = BankBuildingsTransforms[i].Scale;
		}
		for (int i = 0; i < BuildingCubeTransforms.Length; i++)
		{
			Instantiate(Resources.Load<GameObject>(PrefabAssetPath.LevelParts["BuildingCube"]), BuildingCubeTransforms[i].Position, BuildingCubeTransforms[i].Rotation).transform.localScale = BuildingCubeTransforms[i].Scale;
		}
		MovementPoints[] TempMovementPoints = new MovementPoints[MovementPoints.Length];
		for (int i = 0; i < MovementPoints.Length; i++)
		{
			GameObject TempGameObject = Instantiate(Resources.Load<GameObject>(PrefabAssetPath.LevelParts["MovingPoint"]), MovementPoints[i].Transform.Position, MovementPoints[i].Transform.Rotation);
			TempGameObject.transform.localScale = MovementPoints[i].Transform.Scale;
			TempMovementPoints[i] = TempGameObject.GetComponent<MovementPoints>();
			TempMovementPoints[i].RotationVector = MovementPoints[i].RotationVector;
			TempMovementPoints[i].NewSpeed = MovementPoints[i].NewSpeed;
			TempMovementPoints[i].PointNum = MovementPoints[i].PointNum;
			TempMovementPoints[i].NeedToRotate = MovementPoints[i].NeedToRotate;
			TempMovementPoints[i].NeedToFly = MovementPoints[i].NeedToFly;
			TempMovementPoints[i].NeedToCountEnemy = MovementPoints[i].NeedToCountEnemy;
			TempMovementPoints[i].NeedToChangeSpeed = MovementPoints[i].NeedToChangeSpeed;
			TempMovementPoints[i].IsFinalPoint = MovementPoints[i].IsFinalPoint;
			TempMovementPoints[i].IsBossBattle = MovementPoints[i].IsBossBattle;
			TempMovementPoints[i].Enemyes = new GameObject[MovementPoints[i].EnemyTransforms.Length];
		}


		for (int i = 0; i < SimpleEnemyTransforms.Length; i++)
		{
			for (int j = 0; j < MovementPoints.Length; j++)
			{
				for (int k = 0; k < MovementPoints[j].EnemyTransforms.Length; k++)
				{
					if (MovementPoints[j].EnemyTransforms[k].Position == SimpleEnemyTransforms[i].Position)
					{
						TempMovementPoints[j].Enemyes[k] = Instantiate(Resources.Load<GameObject>(PrefabAssetPath.LevelParts["EnemyPrefabWithRagdoll"]), SimpleEnemyTransforms[i].Position, SimpleEnemyTransforms[i].Rotation);
						TempMovementPoints[j].Enemyes[k].transform.localScale = SimpleEnemyTransforms[i].Scale;
						if (IsMinion)
						{
							try
							{
								TagType maskTag = new TagType();
								if (IsJoker)
								{
									maskTag = TagType.JokerMask;
								}
								if (IsMisterio)
								{
									maskTag = TagType.MisterioMask;
								}
								if (IsGoblin)
								{
									maskTag = TagType.GoblinMask;
								}
								List<GameObject> maskGameObjects = new List<GameObject>();
								Transform[] tempGameObjects = TempMovementPoints[j].Enemyes[k].GetComponentsInChildren<Transform>();
								for (int l = 0; l < tempGameObjects.Length; l++)
								{
									Debug.Log("tempGameObjects tag = " + tempGameObjects[l].gameObject.tag);
									if (tempGameObjects[l].CompareTag(TagManager.GetTag(maskTag)))
									{
										maskGameObjects.Add(tempGameObjects[l].gameObject);
									}
								}
								int randomNum = UnityEngine.Random.Range(0, maskGameObjects.Count);
								maskGameObjects[randomNum].GetComponent<Renderer>().enabled = true;
							}
							catch { }
						}
					}
				}
			}
		}
		for (int i = 0; i < ThrowingEnemyTransforms.Length; i++)
		{
			for (int j = 0; j < MovementPoints.Length; j++)
			{
				for (int k = 0; k < MovementPoints[j].EnemyTransforms.Length; k++)
				{
					if (MovementPoints[j].EnemyTransforms[k].Position == ThrowingEnemyTransforms[i].Position)
					{
						TempMovementPoints[j].Enemyes[k] = Instantiate(Resources.Load<GameObject>(PrefabAssetPath.LevelParts["EnemyThrowingBombs"]), ThrowingEnemyTransforms[i].Position, ThrowingEnemyTransforms[i].Rotation);
						TempMovementPoints[j].Enemyes[k].transform.localScale = ThrowingEnemyTransforms[i].Scale;
						if (IsMinion)
						{
							try
							{
								TagType maskTag = new TagType();
								if (IsJoker)
								{
									maskTag = TagType.JokerMask;
								}
								if (IsMisterio)
								{
									maskTag = TagType.MisterioMask;
								}
								if (IsGoblin)
								{
									maskTag = TagType.GoblinMask;
								}
								List<GameObject> maskGameObjects = new List<GameObject>();
								Transform[] tempGameObjects = TempMovementPoints[j].Enemyes[k].GetComponentsInChildren<Transform>();
								for (int l = 0; l < tempGameObjects.Length; l++)
								{
									Debug.Log("tempGameObjects tag = " + tempGameObjects[l].gameObject.tag);
									if (tempGameObjects[l].CompareTag(TagManager.GetTag(maskTag)))
									{
										maskGameObjects.Add(tempGameObjects[l].gameObject);
									}
								}
								int randomNum = UnityEngine.Random.Range(0, maskGameObjects.Count);
								maskGameObjects[randomNum].GetComponent<Renderer>().enabled = true;
							}
							catch { }
						}
					}
				}
			}
		}

		for (int i = 0; i < EnemyWithShieldTransforms.Length; i++)
		{
			for (int j = 0; j < MovementPoints.Length; j++)
			{
				for (int k = 0; k < MovementPoints[j].EnemyTransforms.Length; k++)
				{
					if (MovementPoints[j].EnemyTransforms[k].Position == EnemyWithShieldTransforms[i].Position)
					{
						TempMovementPoints[j].Enemyes[k] = Instantiate(Resources.Load<GameObject>(PrefabAssetPath.LevelParts["EnemyPrefabWithShield"]), EnemyWithShieldTransforms[i].Position, EnemyWithShieldTransforms[i].Rotation);
						TempMovementPoints[j].Enemyes[k].transform.localScale = EnemyWithShieldTransforms[i].Scale;

						if (IsMinion)
						{
							try
							{
								TagType maskTag = new TagType();
								if (IsJoker)
								{
									maskTag = TagType.JokerMask;
								}
								if (IsMisterio)
								{
									maskTag = TagType.MisterioMask;
								}
								if (IsGoblin)
								{
									maskTag = TagType.GoblinMask;
								}
								List<GameObject> maskGameObjects = new List<GameObject>();
								Transform[] tempGameObjects = TempMovementPoints[j].Enemyes[k].GetComponentsInChildren<Transform>();
								for (int l = 0; l < tempGameObjects.Length; l++)
								{
									Debug.Log("tempGameObjects tag = " + tempGameObjects[l].gameObject.tag);
									if (tempGameObjects[l].CompareTag(TagManager.GetTag(maskTag)))
									{
										maskGameObjects.Add(tempGameObjects[l].gameObject);
									}
								}
								int randomNum = UnityEngine.Random.Range(0, maskGameObjects.Count);
								maskGameObjects[randomNum].GetComponent<Renderer>().enabled = true;
							}
							catch { }
						}
					}
				}
			}
		}

		//spawn boss set


		//spawn boss set
		if (BossSetParametrs.IsActive)
		{
			/*
			switch (BossSetParametrs.SetNum)
			{
				case 0:
					Instantiate(Resources.Load<GameObject>(PrefabAssetPath.LevelParts["BossSet0"]), BossSetParametrs.Transform.Position, BossSetParametrs.Transform.Rotation);
					break;
				case 1:
					Instantiate(Resources.Load<GameObject>(PrefabAssetPath.LevelParts["BossSet1"]), BossSetParametrs.Transform.Position, BossSetParametrs.Transform.Rotation);
					break;
				case 2:
					Instantiate(Resources.Load<GameObject>(PrefabAssetPath.LevelParts["BossSet2"]), BossSetParametrs.Transform.Position, BossSetParametrs.Transform.Rotation);
					break;
			}*/
			Instantiate(Resources.Load<GameObject>($"Prefabs/Bosses/{BossSetParametrs.SetName}"), BossSetParametrs.Transform.Position, BossSetParametrs.Transform.Rotation);
		}



		for (int i = 0; i < Buildings3Transforms.Length; i++)
		{
			Instantiate(Resources.Load<GameObject>(PrefabAssetPath.LevelParts["PrepearedBuilding3"]), Buildings3Transforms[i].Position, Buildings3Transforms[i].Rotation).transform.localScale = Buildings3Transforms[i].Scale;
		}
		for (int i = 0; i < Buildings4Transforms.Length; i++)
		{
			Instantiate(Resources.Load<GameObject>(PrefabAssetPath.LevelParts["PrepearedBuilding4"]), Buildings4Transforms[i].Position, Buildings4Transforms[i].Rotation).transform.localScale = Buildings4Transforms[i].Scale;
		}
		for (int i = 0; i < RoofBuildingTransforms.Length; i++)
		{
			Instantiate(Resources.Load<GameObject>(PrefabAssetPath.LevelParts["PrepearedRoofBuilding"]), RoofBuildingTransforms[i].Position, RoofBuildingTransforms[i].Rotation).transform.localScale = RoofBuildingTransforms[i].Scale;
		}
	}
}

[Serializable]
public class CustomTransform
{
	public Vector3 Position;
	public Quaternion Rotation;
	public Vector3 Scale;
}

[Serializable]
public class CustomBossSetParametrs
{
	public CustomTransform Transform;
	public string SetName;
	public bool IsActive;
}
[Serializable]
public class MovementPointForBuilder
{
	public CustomTransform Transform;
	public CustomTransform[] EnemyTransforms;
	public Vector3 RotationVector;
	public float NewSpeed;
	public int PointNum = 0;
	public bool NeedToRotate;
	public bool NeedToFly;
	public bool NeedToCountEnemy = true;
	public bool NeedToChangeSpeed = false;
	public bool IsFinalPoint = false;
	public bool IsBossBattle = false;
}
