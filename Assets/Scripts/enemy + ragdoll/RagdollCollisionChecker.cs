using UnityEngine;

public class RagdollCollisionChecker : MonoBehaviour
{
	private EnemyController _stickmanscript;
	private ThrowingEnemyController _throwingStickmanScript;
	private FallingEnemy _fallingStickmanScript;
	[SerializeField] private BossRagdollController _bossRagdollController;
	private bool _isThrowingStickman;
	private bool _isFallingStickman;
	[SerializeField] private bool _isBossStickman;
	public void SetParametrs(EnemyController stickmanscript)
	{
		_isThrowingStickman = false;
		_stickmanscript = stickmanscript;
	}
	public void SetParametrs(ThrowingEnemyController stickmanscript)
	{
		_isThrowingStickman = true;
		_throwingStickmanScript = stickmanscript;
	}
	public void SetParametrs(FallingEnemy stickmanscript)
	{
		_isFallingStickman = true;
		_fallingStickmanScript = stickmanscript;
	}
	public void SetParametrs(BossRagdollController stickmanscript)
	{
		_isBossStickman = true;
		_bossRagdollController = stickmanscript;
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag(TagManager.GetTag(TagType.Wall)))
		{
			if (!_isBossStickman)
			{
				if (_isThrowingStickman)
				{
					_throwingStickmanScript.GetStickmanStucked(collision, transform.position, gameObject.name);
				}
				else if (_isFallingStickman)
				{
					_fallingStickmanScript.GetStickmanStucked(collision, transform.position, gameObject.name);
				}
				else
				{
					_stickmanscript.GetStickmanStucked(collision, transform.position, gameObject.name);
				}
			}
			else
			{
				_bossRagdollController.GetStickmanStucked(collision, transform.position, gameObject.name);

			}
		}
	}
}
