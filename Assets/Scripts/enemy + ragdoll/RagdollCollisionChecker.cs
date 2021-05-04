using UnityEngine;

public class RagdollCollisionChecker : MonoBehaviour
{
	private EnemyController _stickmanscript;
	private ThrowingEnemyController _throwingStickmanScript;
	private FallingEnemy _fallingStickmanScript;
	private bool _isThrowingStickman;
	private bool _isFallingStickman;
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
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag(TagManager.GetTag(TagType.Wall)))
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
	}
}
