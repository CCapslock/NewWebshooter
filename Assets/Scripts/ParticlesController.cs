using System.Collections.Generic;
using UnityEngine;

public class ParticlesController : MonoBehaviour
{
	public static ParticlesController Current;
	public ParticleSystem SmallExplosinParticles;
	public ParticleSystem MagicExplosinParticles;
	public ParticleSystem SpeedyWindParticles;
	public ParticleSystem StarsExplosionParticles;
	public ParticleSystem EvilLaugh;
	public ParticleSystem Glow;
	public ParticleSystem ShieldBroke;
	private ParticleSystem _evilLaughObject;
	private ParticleSystem _glowObject;
	private ParticleSystem[] _starsExplosion;
	private ParticleSystem _shieldBroke;
	private ParticleSystem[] _smallExplosinObject;
	private ParticleSystem[] _magicExplosion;
	private ParticleSystem _speedyWindObject;

	private GameObject main;

	private void Awake()
	{
		main = FindObjectOfType<MainGameController>().gameObject;
		Current = this;
		_glowObject = Instantiate(Glow, new Vector3(0, 0, -20f), Quaternion.identity);
		_shieldBroke = Instantiate(ShieldBroke, new Vector3(0, 0, -20f), Quaternion.identity);
		_evilLaughObject = Instantiate(EvilLaugh, new Vector3(0, 0, -20f), Quaternion.identity);
		_evilLaughObject.Stop();

		_starsExplosion = new ParticleSystem[10];
		for (int i = 0; i < _starsExplosion.Length; i++)
        {
			_starsExplosion[i] = Instantiate(StarsExplosionParticles, new Vector3(0, 0, -20f), Quaternion.identity, main.transform);
		}
		
		_smallExplosinObject = new ParticleSystem[6];
		for (int i = 0; i < _smallExplosinObject.Length; i++)
		{
			_smallExplosinObject[i] = Instantiate(SmallExplosinParticles, new Vector3(0, 0, -20f), Quaternion.identity, main.transform);
		}
		
		_magicExplosion = new ParticleSystem[6];
		for (int i = 0; i < _magicExplosion.Length; i++)
		{
			_magicExplosion[i] = Instantiate(MagicExplosinParticles, new Vector3(0, 0, -20f), Quaternion.identity, main.transform);
		}
	}

	public void MakeSmallExplosion(Vector3 position)
	{
		for (int i = 0; i < _smallExplosinObject.Length; i++)
		{
			if (!_smallExplosinObject[i].isPlaying || i == _smallExplosinObject.Length - 1)
			{
				_smallExplosinObject[i].transform.position = position;
				_smallExplosinObject[i].Play();
				break;
			}
		}
	}
	public void MakeMagicExplosion(Vector3 position)
	{
		for (int i = 0; i < _magicExplosion.Length; i++)
		{
			if (!_magicExplosion[i].isPlaying || i == _magicExplosion.Length - 1)
			{
				_magicExplosion[i].transform.position = position;
				_magicExplosion[i].Play();
				break;
			}
		}
	}
	public void MakeStarsExplosion(Vector3 position)
	{
		for (int i = 0; i < _starsExplosion.Length; i++)
        {
			if (!_starsExplosion[i].isPlaying || i == _starsExplosion.Length - 1)
			{
				_starsExplosion[i].transform.position = position;
				_starsExplosion[i].Play();
				break;
			}
        }
	}
	public void StartWindParticle(Vector3 position, Vector3 rotation)
	{
		_speedyWindObject = Instantiate(SpeedyWindParticles, position, Quaternion.Euler(rotation));
		_speedyWindObject.Play();
	}
	public void MakeGlow(Vector3 position)
	{
		_glowObject.transform.position = position;
		_glowObject.Play();
	}
	public void MakeEvilLaughParticles(Vector3 position)
	{
		_evilLaughObject.transform.position = position;
		_evilLaughObject.Play();
	}
	public void MakeShieldBrokenParticles(Vector3 position)
	{
		_shieldBroke.transform.position = position;
		_shieldBroke.Play();
	}
}
