using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LVL_number_1", menuName = "Create LVL", order = 0)]
public class LevelCreator : ScriptableObject
{
	[InfoBox("������ 'BuildScene' ����� ������� ��� �� ��������� ��������� (�� ������ ��������� �����)", EInfoBoxType.Warning)]
	public CustomTransform[] Buildings1Transforms;
	public CustomTransform[] Buildings2Transforms;
	public CustomTransform[] Buildings3Transforms;
	public CustomTransform[] Buildings4Transforms;
	public CustomTransform[] BuildingConstr1Transforms;
	public CustomTransform[] BuildingConstr2Transforms;
	public CustomTransform[] RoofBuildingTransforms;
	public CustomTransform[] BankBuildingsTransforms;
	public CustomTransform[] BuildingCubeTransforms;

	public CustomTransform[] SimpleEnemyTransforms;
	public CustomTransform[] ThrowingEnemyTransforms;
	public CustomTransform[] EnemyWithShieldTransforms;
	public CustomTransform[] DodgeEnemyTransforms;
	public MovementPointForBuilder[] MovementPoints;
	public CustomBossSetParametrs BossSetParametrs;

	public CustomTransform[] DecorativeBenchTransforms;
	public CustomTransform[] DecorativeCraneTransforms;
	public CustomTransform[] DecorativeVoltageWiresTransforms;
	public CustomTransform[] DecorativeWoodShieldTransforms;

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

		DecorativeBenchTransforms = new CustomTransform[GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.DecorativeBench)).Length];
		DecorativeCraneTransforms = new CustomTransform[GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.DecorativeCrane)).Length];
		DecorativeVoltageWiresTransforms = new CustomTransform[GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.DecorativeVoltageWires)).Length];
		DecorativeWoodShieldTransforms = new CustomTransform[GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.DecorativeWoodShield)).Length];

		RoofBuildingTransforms = new CustomTransform[GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.RoofBuilding)).Length];
		BankBuildingsTransforms = new CustomTransform[GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.BankBuilding)).Length];
		BuildingConstr1Transforms = new CustomTransform[GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.BuildingConstr1)).Length];
		BuildingConstr2Transforms = new CustomTransform[GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.BuildingConstr2)).Length];
		BuildingCubeTransforms = new CustomTransform[GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.BuildingCube)).Length];

		SimpleEnemyTransforms = new CustomTransform[GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.SimpleEnemy)).Length];
		ThrowingEnemyTransforms = new CustomTransform[GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.ThrowingEnemy)).Length];
		EnemyWithShieldTransforms = new CustomTransform[GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.ShieldEnemy)).Length];
		DodgeEnemyTransforms = new CustomTransform[GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.DodgeEnemy)).Length];

		MovementPoints = new MovementPointForBuilder[GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.MovingPoint)).Length];
		BossSetParametrs = new CustomBossSetParametrs();
	}
	[Button]
	public void ScanScene()
	{
		FindObjects(GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.Buildings1)), Buildings1Transforms);
		FindObjects(GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.Buildings2)), Buildings2Transforms);
		FindObjects(GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.Buildings3)), Buildings3Transforms);
		FindObjects(GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.Buildings4)), Buildings4Transforms);
		FindObjects(GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.BuildingConstr1)), BuildingConstr1Transforms);
		FindObjects(GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.BuildingConstr2)), BuildingConstr2Transforms);
		FindObjects(GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.RoofBuilding)), RoofBuildingTransforms);
		FindObjects(GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.BankBuilding)), BankBuildingsTransforms);
		FindObjects(GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.BuildingCube)), BuildingCubeTransforms);

		FindObjects(GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.DecorativeBench)), DecorativeBenchTransforms);
		FindObjects(GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.DecorativeCrane)), DecorativeCraneTransforms);
		FindObjects(GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.DecorativeVoltageWires)), DecorativeVoltageWiresTransforms);
		FindObjects(GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.DecorativeWoodShield)), DecorativeWoodShieldTransforms);

		FindObjects(GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.SimpleEnemy)), SimpleEnemyTransforms);
		FindObjects(GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.ThrowingEnemy)),ThrowingEnemyTransforms);
		FindObjects(GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.ShieldEnemy)), EnemyWithShieldTransforms);
		FindObjects(GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.DodgeEnemy)),DodgeEnemyTransforms);

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
	private void FindObjects(GameObject[] gameObjects, CustomTransform[] awdw)
	{
		CustomTransform[] tempObjectsTransforms = new CustomTransform[awdw.Length];
		for (int i = 0; i < tempObjectsTransforms.Length; i++)
		{
			awdw[i].Position = gameObjects[i].transform.position;
			awdw[i].Rotation = gameObjects[i].transform.rotation;
			awdw[i].Scale = gameObjects[i].transform.localScale;
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
									if (UnityEngine.Random.Range(0, 2) == 1)
									{
										maskTag = TagType.MisterioMask;
									}
									else
									{
										maskTag = TagType.MisterioMask;
									}
								}
								if (IsGoblin)
								{
									maskTag = TagType.GoblinMask;
								}
								List<GameObject> maskGameObjects = new List<GameObject>();
								Transform[] tempGameObjects = TempMovementPoints[j].Enemyes[k].GetComponentsInChildren<Transform>();
								for (int l = 0; l < tempGameObjects.Length; l++)
								{
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
									if (UnityEngine.Random.Range(0, 2) == 1)
									{
										maskTag = TagType.MisterioMask;
									}
									else
									{
										maskTag = TagType.MisterioMask;
									}
									
								}
								if (IsGoblin)
								{
									maskTag = TagType.GoblinMask;
								}
								List<GameObject> maskGameObjects = new List<GameObject>();
								Transform[] tempGameObjects = TempMovementPoints[j].Enemyes[k].GetComponentsInChildren<Transform>();
								for (int l = 0; l < tempGameObjects.Length; l++)
								{
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
		if (BossSetParametrs.IsActive)
		{
			Instantiate(Resources.Load<GameObject>($"Prefabs/Bosses/{BossSetParametrs.SetName}"), BossSetParametrs.Transform.Position, BossSetParametrs.Transform.Rotation);
		}

		//new Objects

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
		for (int i = 0; i < BuildingConstr1Transforms.Length; i++)
		{
			Instantiate(Resources.Load<GameObject>(PrefabAssetPath.LevelParts["PrepearedBuildingConstr1"]), BuildingConstr1Transforms[i].Position, BuildingConstr1Transforms[i].Rotation).transform.localScale = BuildingConstr1Transforms[i].Scale;
		}
		for (int i = 0; i < BuildingConstr2Transforms.Length; i++)
		{
			Instantiate(Resources.Load<GameObject>(PrefabAssetPath.LevelParts["PrepearedBuildingConstr2"]), BuildingConstr2Transforms[i].Position, BuildingConstr2Transforms[i].Rotation).transform.localScale = BuildingConstr2Transforms[i].Scale;
		}


		for (int i = 0; i < DecorativeBenchTransforms.Length; i++)
		{
			Instantiate(Resources.Load<GameObject>(PrefabAssetPath.LevelParts["PrepearedDecorativeBench"]), DecorativeBenchTransforms[i].Position, DecorativeBenchTransforms[i].Rotation).transform.localScale = DecorativeBenchTransforms[i].Scale;
		}
		for (int i = 0; i < DecorativeCraneTransforms.Length; i++)
		{
			Instantiate(Resources.Load<GameObject>(PrefabAssetPath.LevelParts["PrepearedDecorativeCrane"]), DecorativeCraneTransforms[i].Position, DecorativeCraneTransforms[i].Rotation).transform.localScale = DecorativeCraneTransforms[i].Scale;
		}
		for (int i = 0; i < DecorativeVoltageWiresTransforms.Length; i++)
		{
			Instantiate(Resources.Load<GameObject>(PrefabAssetPath.LevelParts["PrepearedDecorativeVoltageWires"]), DecorativeVoltageWiresTransforms[i].Position, DecorativeVoltageWiresTransforms[i].Rotation).transform.localScale = DecorativeVoltageWiresTransforms[i].Scale;
		}
		for (int i = 0; i < DecorativeWoodShieldTransforms.Length; i++)
		{
			Instantiate(Resources.Load<GameObject>(PrefabAssetPath.LevelParts["PrepearedDecorativeWoodShield"]), DecorativeWoodShieldTransforms[i].Position, DecorativeWoodShieldTransforms[i].Rotation).transform.localScale = DecorativeWoodShieldTransforms[i].Scale;
		}




		for (int i = 0; i < DodgeEnemyTransforms.Length; i++)
		{
			for (int j = 0; j < MovementPoints.Length; j++)
			{
				for (int k = 0; k < MovementPoints[j].EnemyTransforms.Length; k++)
				{
					if (MovementPoints[j].EnemyTransforms[k].Position == DodgeEnemyTransforms[i].Position)
					{
						TempMovementPoints[j].Enemyes[k] = Instantiate(Resources.Load<GameObject>(PrefabAssetPath.LevelParts["EnemyPrefabDodge"]), DodgeEnemyTransforms[i].Position, DodgeEnemyTransforms[i].Rotation);
						TempMovementPoints[j].Enemyes[k].transform.localScale = DodgeEnemyTransforms[i].Scale;

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

		for (int i = 0; i < BankBuildingsTransforms.Length; i++)
		{
			Instantiate(Resources.Load<GameObject>(PrefabAssetPath.LevelParts["BankBuilding"]), BankBuildingsTransforms[i].Position, BankBuildingsTransforms[i].Rotation).transform.localScale = BankBuildingsTransforms[i].Scale;
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
