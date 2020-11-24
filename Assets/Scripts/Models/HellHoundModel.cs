﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BeastHunter
{
    public sealed class HellHoundModel : EnemyModel
    {
        #region Fields

        private HellHoundData hellHoundData;
        private InteractableObjectBehavior[] InteractableObjects;
        private InteractableObjectBehavior detectionSphereIO;
        private SphereCollider detectionSphere;
        private HellHoundAttackStateBehaviour[] attackStates;

        public HellHoundData.BehaviourState BehaviourState;
        public Vector3 SpawnPoint;
        public Collider ChasingTarget;
        public bool IsAttacking;
        public float Timer;

        public float RotatePosition1;
        public float RotatePosition2;

        public List<Collider> Enemies;

        #endregion


        #region Properties

        public Animator Animator { get; }
        public GameObject HellHound { get; }
        public NavMeshAgent NavMeshAgent { get; }
        public Rigidbody Rigidbody { get; }
        public Transform Transform { get; }
        public Collider AttackCollider { get; }
        public InteractableObjectBehavior WeaponIO { get; }

        #endregion


        #region ClassLifeCycle

        public HellHoundModel(GameObject gameObject, HellHoundData hellHoundData)
        {
            this.hellHoundData = hellHoundData;
            HellHound = gameObject;

            Transform = HellHound.transform;
            BehaviourState = HellHoundData.BehaviourState.None;

            Rigidbody = HellHound.GetComponent<Rigidbody>();
            SpawnPoint = Rigidbody.position;

            Animator = HellHound.GetComponent<Animator>();
            Animator.SetFloat("JumpSpeedRate", hellHoundData.Stats.JumpingSpeedRate);
            Animator.SetFloat("JumpBackSpeedRate", hellHoundData.Stats.BackJumpAnimationSpeedRate);
            Animator.SetFloat("JumpBackIntensity", hellHoundData.Stats.BackJumpAnimationIntensity);

            InteractableObjects = HellHound.GetComponentsInChildren<InteractableObjectBehavior>();

            detectionSphereIO = GetInteractableObject(InteractableObjectType.Sphere);
            detectionSphereIO.OnFilterHandler = DetectionFilter;
            detectionSphereIO.OnTriggerEnterHandler = OnDetectionEnemy;
            detectionSphereIO.OnTriggerExitHandler = OnLostSightEnemy;

            detectionSphere = detectionSphereIO.GetComponent<SphereCollider>();
            if (detectionSphere == null) Debug.LogError(this + " not found SphereCollider in DetectionSphere gameobject");
            else detectionSphere.radius = hellHoundData.Stats.DetectionRadius;

            WeaponIO = HellHound.GetComponentInChildren<WeaponHitBoxBehavior>();
            WeaponIO.OnFilterHandler = OnHitEnemyFilter;
            WeaponIO.OnTriggerEnterHandler = OnHitEnemy;

            NavMeshAgent = HellHound.GetComponent<NavMeshAgent>();
            NavMeshAgent.angularSpeed = hellHoundData.Stats.AngularSpeed;
            NavMeshAgent.acceleration = hellHoundData.Stats.Acceleration;
            NavMeshAgent.stoppingDistance = hellHoundData.Stats.StoppingDistance;

            AttackCollider = WeaponIO.GetComponent<BoxCollider>();
            AttackCollider.enabled = false;

            attackStates = Animator.GetBehaviours<HellHoundAttackStateBehaviour>();
            for (int i = 0; i < attackStates.Length; i++)
            {
                attackStates[i].OnStateEnterHandler += OnAttackStateEnter;
                attackStates[i].OnStateExitHandler += OnAttackStateExit;
            }

            Enemies = new List<Collider>();

            CurrentHealth = this.hellHoundData.BaseStats.MainStats.MaxHealth;
            IsDead = false;
        }

        #endregion


        #region Methods
        private bool DetectionFilter(Collider collider) => hellHoundData.DetectionFilter(collider);
        private void OnDetectionEnemy(ITrigger trigger, Collider collider) => hellHoundData.OnDetectionEnemy(collider, this);
        private void OnLostSightEnemy(ITrigger trigger, Collider collider) => hellHoundData.OnLostSightEnemy(collider, this);
        private bool OnHitEnemyFilter(Collider collider) => hellHoundData.OnHitEnemyFilter(collider);
        private void OnHitEnemy(ITrigger trigger, Collider collider) => hellHoundData.OnHitEnemy(collider, this);
        private void OnAttackStateEnter() => hellHoundData.OnAttackStateEnter(this);
        private void OnAttackStateExit() => hellHoundData.OnAttackStateExit(this);

        private InteractableObjectBehavior GetInteractableObject(InteractableObjectType type)
        {
            for (int i =0; i< InteractableObjects.Length; i++)
            {
                if (InteractableObjects[i].Type == type) return InteractableObjects[i];
            }
            Debug.LogWarning(this + "  not found InteractableObject of type " + type);
            return null;
        }

        private List<InteractableObjectBehavior> GetInteractableObjects(InteractableObjectType type)
        {
            List<InteractableObjectBehavior> result = new List<InteractableObjectBehavior>();
            for (int i = 0; i < InteractableObjects.Length; i++)
            {
                if (InteractableObjects[i].Type == type) result.Add(InteractableObjects[i]);
            }
            if (result.Count == 0) Debug.LogWarning(this + " not found InteractableObjects of type " + type);
            return result;
        }

        #endregion


        #region EnemyModel

        public override void DoSmth(string how)
        {
            hellHoundData.Do(how);
        }

        public override void Execute()
        {
            if (!IsDead)
            {
                hellHoundData.Act(this);
            }
        }

        public override EnemyStats GetStats()
        {
            return hellHoundData.BaseStats;
        }

        public override void OnAwake()
        {
            
        }

        public override void OnTearDown()
        {
            
        }

        public override void TakeDamage(Damage damage)
        {
            if (!IsDead)
            {
                hellHoundData.TakeDamage(this, damage);
            }
        }

        #endregion

    }
}
