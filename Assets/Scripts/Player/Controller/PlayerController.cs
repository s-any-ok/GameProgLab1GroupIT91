using System;
using UnityEngine;

namespace Player.Controller
{
	public class PlayerController : MonoBehaviour
	{
		[SerializeField] private Vector2 _force = new(10, 10);
		[SerializeField] private float _speed = 10f;
		[SerializeField] private Rigidbody2D _rb;

		private bool _isInAir;
		private Vector3 _initialPosition;
	
		private void Start()
		{
			_initialPosition = transform.position;
		}
		private void Update()
		{
			if (Input.GetButton("Horizontal"))
			{
				MoveHorizontal();
			}

			if (Input.GetKey(KeyCode.Space) && !_isInAir)
			{
				Jump();
			}
			CheckOutOfMap();
		}
		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
			{
				_isInAir = false;
			}
		}
		private void MoveHorizontal()
		{
			Vector3 horizontalMovement = transform.right * Input.GetAxis("Horizontal");
			transform.position =
				Vector3.MoveTowards(transform.position, transform.position + horizontalMovement, _speed * Time.deltaTime);
		}

		private void Jump()
		{
			_isInAir = true;
			_rb.AddForce(transform.up * _force, ForceMode2D.Impulse);
		}

		private void CheckOutOfMap()
		{
			if (Math.Abs(transform.position.x) > 9 || transform.position.y < -4)
			{
				transform.position = _initialPosition;
			} 
		}

	}
}
