using System.Collections.Generic;

public static class TagManager
{
	private static readonly Dictionary<TagType, string> _tags;

	static TagManager()
	{
		_tags = new Dictionary<TagType, string>
			{
				{TagType.Player, "Player"},
				{TagType.EnemyPart, "EnemyPart"},
				{TagType.Wall, "Wall"},
				{TagType.Web, "Web"},
				{TagType.MovingPoint, "MovingPoint"},
				{TagType.Object, "Object"},
				{TagType.Buildings1, "Buildings1"},
				{TagType.Buildings2, "Buildings2"},
				{TagType.Buildings3, "Buildings3"},
				{TagType.Buildings4, "Buildings4"},
				{TagType.SimpleEnemy, "SimpleEnemy"},
				{TagType.ThrowingEnemy, "ThrowingEnemy"},
				{TagType.BuildingCube, "BuildingCube"},
				{TagType.Bottom, "Bottom"},
				{TagType.BossSet, "BossSet"},
				{TagType.JokerMask, "JokerMask"},
				{TagType.MisterioMask, "MisterioMask"},
				{TagType.GoblinMask, "GoblinMask"},
				{TagType.ShieldEnemy, "ShieldEnemy"},
				{TagType.DodgeEnemy, "DodgeEnemy"},
				{TagType.BankBuilding, "BankBuilding"},
				{TagType.BuildingConstr1, "BuildingConstr1"},
				{TagType.BuildingConstr2, "BuildingConstr2"},
				{TagType.RoofBuilding, "RoofBuilding"}
			};
	}

	public static string GetTag(TagType tagType)
	{
		return _tags[tagType];
	}
}
