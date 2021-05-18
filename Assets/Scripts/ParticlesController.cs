using System.Collections.Generic;
using UnityEngine;

public class ParticlesController : MonoBehaviour
{
	public static ParticlesController Current;
	public ParticleSystem SmallExplosinParticles;
	public ParticleSystem MagicExplosinParticles;
	public ParticleSystem SpeedyWindParticles;
	public ParticleSystem StarsExplosionParticles;
	public ParticleSystem Glow;
	private ParticleSystem _glowObject;
	private ParticleSystem _starsExplosion;
	private ParticleSystem[] _smallExplosinObject;
	private ParticleSystem[] _magicExplosion;
	private ParticleSystem _speedyWindObject;


	private void Awake()
	{
		Current = this;
		_glowObject = Instantiate(Glow, new Vector3(0, 0, -20f), Quaternion.identity);
		_starsExplosion = Instantiate(StarsExplosionParticles, new Vector3(0, 0, -20f), Quaternion.identity);
		_smallExplosinObject = new ParticleSystem[6];
		for (int i = 0; i < _smallExplosinObject.Length; i++)
		{
			_smallExplosinObject[i] = Instantiate(SmallExplosinParticles, new Vector3(0, 0, -20f), Quaternion.identity);
		}
		_magicExplosion = new ParticleSystem[6];
		for (int i = 0; i < _magicExplosion.Length; i++)
		{
			_magicExplosion[i] = Instantiate(MagicExplosinParticles, new Vector3(0, 0, -20f), Quaternion.identity);
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
		_starsExplosion.transform.position = position;
		_starsExplosion.Play();
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
}
