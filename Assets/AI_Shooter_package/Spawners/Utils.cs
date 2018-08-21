using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Utils {

	public static GameObject CreateEmpty (Transform parent) {
		GameObject go = new GameObject ();

		if (parent) {
			go.transform.parent = parent;
			go.transform.position = parent.position;
			go.name = parent.name + "_t";
		}

		return go;
	}

	public static Vector3 RandomVector3 (float range) {
		return new Vector3 (Random.Range(-range, range), Random.Range(-range, range), Random.Range(-range, range));
	}

	public static bool IsInside ( Collider test, Vector3 point)
	{
		Vector3    center;
		Vector3    direction;
		Ray        ray;
		RaycastHit hitInfo;
		bool       hit;
		
		// Use collider bounds to get the center of the collider. May be inaccurate
		// for some colliders (i.e. MeshCollider with a 'plane' mesh)
		center = test.bounds.center;
		
		// Cast a ray from point to center
		direction = center - point;
		ray = new Ray(point, direction);
		hit = test.Raycast(ray, out hitInfo, direction.magnitude);
		
		// If we hit the collider, point is outside. So we return !hit
		return !hit;
	}
	
	public static Vector3 GetRandomPositionInsideCollider (Collider col) {

		Vector3 point = col.bounds.center;
		point += new Vector3 (Random.Range(-col.bounds.size.x/2, col.bounds.size.x/2), 
		                      Random.Range(-col.bounds.size.y/2, col.bounds.size.y/2), 
		                      Random.Range(-col.bounds.size.z/2, col.bounds.size.z/2));
		if (IsInside(col, point))
//		if (col.bounds.Contains(point))
			return point;
		else 
			return GetRandomPositionInsideCollider(col);
	}
	
	
	
	
	public static Vector3 CatmullRom(Vector3 _P0, Vector3 _P1, Vector3 _P2, Vector3 _P3, float _i)
	{
		// comments are no use here... it's the catmull-rom equation.
		// Un-magic this, lord vector!
		return 0.5f * ( (2 * _P1) + (-_P0 + _P2) * _i + (2*_P0 - 5*_P1 + 4*_P2 - _P3) * _i*_i + (-_P0 + 3*_P1 - 3*_P2 + _P3) * _i*_i*_i );
	}
	
	/// <summary>
	/// Сортировка по дистанции относительно точки
	/// </summary>
	public class TransformDistanceCompararer : Comparer <Transform> { //IComparer {// System.Collections.Generic.Comparer<Transform> {

		public Vector3 position;
		
		public TransformDistanceCompararer (Vector3 pos) {
			position = pos;
		}
		
		public override int Compare (Transform x, Transform y)
		{
			float dist1 = (position - x.position).sqrMagnitude;
			float dist2 = (position - y.position).sqrMagnitude;
			
			return Mathf.CeilToInt(dist1 - dist2);
		}
		
	}
}
