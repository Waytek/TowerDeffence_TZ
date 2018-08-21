using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseComponentController : MonoBehaviour {
    public Action onDead = delegate { };
    Animator _animator;
    public Animator animator
    {
        get
        {
            if (_animator != null) { return _animator; }
            else
            {
                if ((_animator = GetComponent<Animator>()) != null)
                {
                    return _animator;
                }
                else
                {
                    Debug.LogError("Animator NotFound");
                    return null;
                }
            }
        }
    }
    NavMeshAgent _navAgent;
    public NavMeshAgent navAgent
    {
        get
        {
            if (_navAgent != null) { return _navAgent; }
            else
            {
                if ((_navAgent = GetComponent<NavMeshAgent>()) != null)
                {
                    return _navAgent;
                }
                else
                {
                    Debug.LogError("NavMeshAgent Created");
                    return _navAgent = gameObject.AddComponent<NavMeshAgent>();
                }
            }
        }
    }
    Collider _collider;
    public Collider coll
    {
        get
        {
            if (_collider != null) { return _collider; }
            else
            {
                if ((_collider = GetComponent<Collider>()) != null)
                {
                    return _collider;
                }
                else
                {
                    Debug.LogError("Collider NotFound");
                    return null;
                }
            }
        }
    }
    Rigidbody _rigidbody;
    public Rigidbody rig
    {
        get
        {
            if (_rigidbody != null) { return _rigidbody; }
            else
            {
                if ((_rigidbody = GetComponent<Rigidbody>()) != null)
                {
                    return _rigidbody;
                }
                else
                {
                    Debug.LogError("Rigidbody NotFound");
                    return null;
                }
            }
        }
    }
    LifeController _lifeController;
    public LifeController lifeController
    {
        get
        {
            if (_lifeController != null) { return _lifeController; }
            else
            {
                if ((_lifeController = GetComponent<LifeController>()) != null)
                {
                    return _lifeController;
                }
                else
                {
                    Debug.LogError("LifeController Created");
                    return _lifeController = gameObject.AddComponent<LifeController>();
                }
            }
        }
    }
}
