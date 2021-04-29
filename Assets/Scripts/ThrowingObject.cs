﻿using UnityEngine;

public class ThrowingObject : MonoBehaviour
{
	public GameObject SpiderWeb;
	public bool IsStucked;
	public bool IsCoin;

	private Rigidbody _rigidbody;
	private GameObject Web;
	private Vector3 CustomWebPosition;
	private Vector3 ThrowingVector;
	private Vector3 RotationVector;
	private float _throwingForce = 3000f;
	private bool IsObjectStucked;
	private bool IsObjectedWebed;

	private float _magicNumber = 0.1f;

	public bool NeedToRotate = true;
	private void Awake()
	{
		IsStucked = false;
		_rigidbody = GetComponent<Rigidbody>();
		//_rigidbody.isKinematic = true;
		CustomWebPosition = new Vector3(0, 0, -0.3f);

		int num1 = Random.Range(0, 3);
		int num2 = Random.Range(0, 3 - num1);
		int num3 = Random.Range(0, 3 - num1 - num2);

		RotationVector = new Vector3(num1, num2, num3);
	}
	private void FixedUpdate()
	{
		if (NeedToRotate)
			RotateObject();
	}
	private void RotateObject()
	{
		_rigidbody.transform.Rotate(RotationVector);
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Wall") && !IsObjectStucked && IsObjectedWebed)
		{
			IsObjectStucked = true;
			GetObjectStucked(collision);
		}
		if (collision.gameObject.CompareTag("Web"))
		{
			SphereCollider collider = collision.gameObject.GetComponent<SphereCollider>();
			collider.isTrigger = true;
			IsObjectedWebed = true;
			ThrowingVector = transform.position;
			ThrowingVector.z = 3000f;
			ThrowingVector.x = (transform.position.x - collision.transform.position.x) * 1000;
			ThrowingVector.y = (transform.position.y - collision.transform.position.y) * 2500;
			_rigidbody.AddForce(ThrowingVector);
		}


	}
	private void GetObjectStucked(Collision collision)
	{
		NeedToRotate = false;
		gameObject.tag = "Wall";
		_rigidbody.isKinematic = true;
		IsStucked = true;
		Web = Instantiate(SpiderWeb, transform.position + CustomWebPosition, Quaternion.identity);
		Web.transform.Rotate(new Vector3(0, 180f, Random.Range(0, 360f)));
		 
		if (collision.transform.position.y - collision.GetContact(0).point.y <= _magicNumber)
		{
			if (collision.GetContact(0).point.z - collision.transform.position.z <= _magicNumber)
			{
				if (collision.GetContact(0).point.x > collision.transform.position.x)
				{
					Web.transform.rotation = Quaternion.Euler(Web.transform.rotation.eulerAngles + new Vector3(0, 90f, 0));
				}
				else
				{
					Web.transform.rotation = Quaternion.Euler(Web.transform.rotation.eulerAngles + new Vector3(0, -90f, 0));
				}
			}
		}
		else
		{
			//Web.transform.rotation = Quaternion.Euler(Web.transform.rotation.eulerAngles + new Vector3(-90f, 0, 0));
		}
	}
	public void TrowObjectUp()
	{
		_rigidbody.isKinematic = false;
		_rigidbody.AddForce(Vector3.up * _throwingForce);
	}
	public void SetForce(float force)
	{
		_throwingForce = force;
	}
}
