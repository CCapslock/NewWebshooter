using UnityEngine;

public class ParticlesController : MonoBehaviour
{
	public ParticleSystem SmallExplosinParticles;
	public ParticleSystem SpeedyWindParticles;
	private ParticleSystem _smallExplosinObject;
	private ParticleSystem _speedyWindObject;

	private void Awake()
	{
		_smallExplosinObject = Instantiate(SmallExplosinParticles, new Vector3(0, 0, -20f), Quaternion.identity);
	}

	public void MakeSmallExplosion(Vector3 position) 
	{
		_smallExplosinObject.transform.position = position;
		_smallExplosinObject.Play();
	}
	public void StartWindParticle(Vector3 position, Vector3 rotation)
	{
		_speedyWindObject = Instantiate(SpeedyWindParticles, position, Quaternion.Euler(rotation));
		_speedyWindObject.Play();
	}
}
