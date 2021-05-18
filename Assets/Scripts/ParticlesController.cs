using UnityEngine;

public class ParticlesController : MonoBehaviour
{
	public static ParticlesController Current;
	public ParticleSystem SmallExplosinParticles;
	public ParticleSystem MagicExplosinParticles;
	public ParticleSystem SpeedyWindParticles;
	public ParticleSystem StarsExplosionParticles;
	private ParticleSystem _smallExplosinObject;
	private ParticleSystem _starsExplosion;
	private ParticleSystem _magicExplosion;
	private ParticleSystem _speedyWindObject;

	private void Awake()
	{
		Current = this;
		_smallExplosinObject = Instantiate(SmallExplosinParticles, new Vector3(0, 0, -20f), Quaternion.identity);
		_starsExplosion = Instantiate(StarsExplosionParticles, new Vector3(0, 0, -20f), Quaternion.identity);
		_magicExplosion = Instantiate(MagicExplosinParticles, new Vector3(0, 0, -20f), Quaternion.identity);
	}

	public void MakeSmallExplosion(Vector3 position) 
	{
		_smallExplosinObject.transform.position = position;
		_smallExplosinObject.Play();
	}
	public void MakeMagicExplosion(Vector3 position) 
	{
		_magicExplosion.transform.position = position;
		_magicExplosion.Play();
	}
	public void MakeStarsExplosionExplosion(Vector3 position) 
	{
		_starsExplosion.transform.position = position;
		_starsExplosion.Play();
	}
	public void StartWindParticle(Vector3 position, Vector3 rotation)
	{
		_speedyWindObject = Instantiate(SpeedyWindParticles, position, Quaternion.Euler(rotation));
		_speedyWindObject.Play();
	}
}
