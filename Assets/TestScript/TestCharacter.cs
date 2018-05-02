using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TestCharacter : MonoBehaviour {
	[SerializeField]
	protected float speed;

	[HideInInspector]
	public bool isFacingRight;

	[SerializeField]
	public int health;

	[SerializeField]
	protected List<string> damageSources;

	[HideInInspector]
	public Animator Animator;

	public abstract bool IsDead {get;}

	public bool TakeDamage {get;set;}

	public bool Attack {get; set;}

	public Rigidbody2D Rigidbody {get; set;}

	protected virtual void Awake()
	{
		isFacingRight = true;
		Animator = GetComponent<Animator>();
		Rigidbody = GetComponent<Rigidbody2D>();
	}

	protected void Flip()
	{
		isFacingRight = !isFacingRight;
		transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
	}

	public abstract IEnumerator TakingDamage();

	protected void OnTriggerEnter2D(Collider2D other)
	{
		if (IsDead)
			return;
		if (damageSources.Contains(other.tag))
		{
			Destroy(other.gameObject);
			StartCoroutine(TakingDamage());
		}
	}

	public void SelfDestroy()
	{
		gameObject.SetActive(false);
	}
}