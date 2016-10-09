using UnityEngine;
using System.Collections;

// interface for any object that can take damage
public interface IDamageable{
	void TakeDamage( int dmg );
}
