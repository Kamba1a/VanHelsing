﻿using UnityEngine;
using UniRx;
using System;
using RootMotion.FinalIK;

namespace BeastHunter
{
    public class BossMainState : BossBaseState
    {
        #region Constants

        public const float ANGLE_TARGET_RANGE = 20f;
        public const float DISTANCE_TO_START_ATTACK = 5f;
        private const float TRIGGER_VIEW_INCREASE = 70f;
        private const float SPEED_COUNT_FRAME = 0.15f;
        private const float TIME_TO_NORMILIZE_WEAK_POINT = 5f;

        private const float HUNGER_TIME = 5f;

        #endregion


        #region Fields

        public Quaternion TargetRotation;

        private Vector3 _lastPosition;
        private Vector3 _currentPosition;
        private Vector3 _targetDirection;

        private float _speedCountTime;
        private float _hungerCountTime = HUNGER_TIME;

        private float _timer = 7f;
        private int _hitPerTime = 0;
        private float _damagePerTime;

        #endregion

        #region Properties

        public bool IsHunger { get; private set; }

        #endregion


        #region ClassLifeCycle

        public BossMainState(BossStateMachine stateMachine) : base(stateMachine)
        {
        }

        #endregion


        #region Methods

        public override void OnAwake()
        {
            _stateMachine._model.BossBehavior.OnFilterHandler += OnFilterHandler;
            _stateMachine._model.BossBehavior.OnTriggerEnterHandler += OnTriggerEnterHandler;
            _stateMachine._model.BossBehavior.OnTriggerExitHandler += OnTriggerExitHandler;
            MessageBroker.Default.Receive<OnPlayerDieEventCLass>().Subscribe(OnPlayerDieHandler);
            MessageBroker.Default.Receive<OnBossStunnedEventClass>().Subscribe(OnBossStunnedHandler);
            MessageBroker.Default.Receive<OnBossHittedEventClass>().Subscribe(OnBossHittedHandler);
            MessageBroker.Default.Receive<OnBossWeakPointHittedEventClass>().Subscribe(MakeWeakPointBurst);
            MessageBroker.Default.Receive<OnPlayerSneakingEventClass>().Subscribe(OnPlayerSneakingHandler);
        }

        public override void Initialise()
        {
            CanExit = false;
            CanBeOverriden = false;
        }

        public override void Execute()
        {
            if (!_stateMachine._model.IsDead)
            {
                SpeedCheck();
                // HealthCheck();
                CheckDirection();
                HungerCheck();
                GetTargetCurrentPosition();
                CheckCurrentState();
                HitCounter();
                InteractionTriggerUpdate();
            }
        }

        public override void OnExit()
        {
        }

        public override void OnTearDown()
        {
            _stateMachine._model.BossBehavior.OnFilterHandler -= OnFilterHandler;
            _stateMachine._model.BossBehavior.OnTriggerEnterHandler -= OnTriggerEnterHandler;
            _stateMachine._model.BossBehavior.OnTriggerExitHandler -= OnTriggerExitHandler;
        }

        private void OnBossHittedHandler(OnBossHittedEventClass eventClass)
        {
            if (!_stateMachine._model.IsDead)
            {
                _hitPerTime++;
              //  _stateMachine.SetCurrentStateOverride(BossStatesEnum.Hitted);
            }
        }

        private void OnBossStunnedHandler(OnBossStunnedEventClass eventClass)
        {
            if (!_stateMachine._model.IsDead)
            {
             //   _stateMachine.SetCurrentStateOverride(BossStatesEnum.Stunned);
            }         
        }

        private void OnPlayerDieHandler(OnPlayerDieEventCLass eventClass)
        {
            if (!_stateMachine._model.IsDead)
            {
                _stateMachine.SetCurrentStateOverride(BossStatesEnum.Patroling);
            }
        }

        private void HealthCheck()
        {
            Debug.Log($"Health{_bossModel.CurrentHealth}");
        }

        private void HitCounter()
        {
            if (_hitPerTime > 0)
            {
                _timer -= Time.deltaTime;
            }

            if (_timer <= 0)
            {
                if (_hitPerTime >= 3)
                {
                    _stateMachine.BossSkills.HardBarkSkill.SwitchAllowed(true);
                    if (!_stateMachine.CurrentState.isAnimationPlay)
                    {
                      //  _stateMachine.BossSkills.ForceUseSkill(_stateMachine.BossSkills.MainSkillDictionary[SkillDictionaryEnum.DefenceStateSkillDictionary], 2);
                    }
                }

                DamageCounterReset();
            }
        }

        private void DamageCounterReset()
        {
            _timer = 7f;
            _hitPerTime = 0;
        }

        public void DamageCounter(Damage damage)
        {
            _damagePerTime += damage.PhysicalDamage;

            if (_damagePerTime >= 20)
            {
                if (_stateMachine.CurrentState.isAnySkillUsed && _stateMachine.CurrentState.CurrentSkill.CanInterrupt)
                {
                    CurrentSkillStop();
                }
                _damagePerTime = 0;
            }
        }

        private void SpeedCheck()
        {
            if (_speedCountTime > 0)
            {
                _speedCountTime -= Time.fixedDeltaTime;
            }
            else
            {
                _speedCountTime = SPEED_COUNT_FRAME;
                _currentPosition = _stateMachine._model.BossTransform.position;
                _stateMachine._model.CurrentSpeed = Mathf.Sqrt((_currentPosition - _lastPosition).sqrMagnitude) /
                    SPEED_COUNT_FRAME;
                _lastPosition = _currentPosition;
            }

            _stateMachine._model.BossAnimator.SetFloat("Speed", _stateMachine._model.CurrentSpeed);
        }

        private void CheckCurrentState()
        {
            if(_stateMachine.CurrentState.IsBattleState)
            {
                _stateMachine._model.BossSphereCollider.radius = TRIGGER_VIEW_INCREASE;
            }
            else
            {
                _stateMachine._model.BossSphereCollider.radius = _bossData._bossSettings.SphereColliderRadius;
            }
        }


        private void HungerCheck()
        {
            if (!IsHunger)
            {
                _hungerCountTime -= Time.deltaTime;
                if(_hungerCountTime<=0)
                {
                    IsHunger = true;
                    _hungerCountTime = HUNGER_TIME;
                }
            }
        }

        private bool OnFilterHandler(Collider tagObject)
        {
            bool isEnemyColliderHit = false;
            InteractableObjectBehavior interacterBehavior = tagObject.GetComponent<InteractableObjectBehavior>();

            if (interacterBehavior != null)
            {
                if( interacterBehavior.Type == InteractableObjectType.Player || interacterBehavior.Type == InteractableObjectType.Food)
                {
                    isEnemyColliderHit = true;
                }
            }
            return isEnemyColliderHit;
        }

        private void OnTriggerEnterHandler(ITrigger thisdObject, Collider enteredObject)
        {
            var interactableObject = enteredObject.GetComponent<InteractableObjectBehavior>().Type;

            if (interactableObject == InteractableObjectType.Player & !enteredObject.isTrigger)
            {
                _bossModel.BossCurrentTarget = enteredObject.gameObject;
                _stateMachine.SetCurrentStateOverride(BossStatesEnum.Chasing);
            }

            if (interactableObject == InteractableObjectType.Food)
            {
                if (!_stateMachine._model.FoodList.Contains(enteredObject.gameObject))
                {
                    _stateMachine._model.FoodList.Add(enteredObject.gameObject);
                }
            }
        }

        private void OnTriggerExitHandler(ITrigger thisdObject, Collider enteredObject)
        {
            var interactableObject = enteredObject.GetComponent<InteractableObjectBehavior>().Type;

            if (interactableObject == InteractableObjectType.Food)
            {
                if (_stateMachine._model.FoodList.Contains(enteredObject.gameObject))
                {
                    _stateMachine._model.FoodList.Remove(enteredObject.gameObject);
                }
            }
            if (interactableObject == InteractableObjectType.Player & !enteredObject.isTrigger)
            {
                _stateMachine.SetCurrentStateOverride(BossStatesEnum.Searching);
            }
        }

        private void MakeWeakPointBurst(OnBossWeakPointHittedEventClass eventClass)
        {
            eventClass.WeakPointCollider.gameObject.GetComponent<ParticleSystem>().Play();
            _stateMachine._model.TakeDamage(Services.SharedInstance.AttackService.CountDamage(
                eventClass.WeakPointCollider.GetComponent<HitBoxBehavior>().AdditionalDamage, 
                    _stateMachine._model.GetStats().MainStats));
            eventClass.WeakPointCollider.GetComponent<Light>().color = Color.red;
            eventClass.WeakPointCollider.enabled = false;

            Action makeWeakPointNormalAction = () => MakeWeakPointNormal(eventClass.WeakPointCollider);
            TimeRemaining makeWeakPointNormal = new TimeRemaining(makeWeakPointNormalAction, TIME_TO_NORMILIZE_WEAK_POINT);
            makeWeakPointNormal.AddTimeRemaining(TIME_TO_NORMILIZE_WEAK_POINT);
        }

        private void MakeWeakPointNormal(Collider collider)
        {
            collider.enabled = true;
            collider.GetComponent<Light>().color = Color.yellow;
        }

        private void CheckDirection()
        {
            _targetDirection = (_stateMachine._context.CharacterModel.CharacterTransform.position -
                _stateMachine._model.BossTransform.position).normalized;
            TargetRotation = Quaternion.LookRotation(_targetDirection);
        }
        
        public Vector3? GetTargetCurrentPosition()
        {     
           return _bossModel.BossCurrentTarget?.transform.position;
        }
    
        private void OnPlayerSneakingHandler(OnPlayerSneakingEventClass eventClass)
        {
            if (eventClass.IsSneaking)
            {
                _stateMachine._model.BossSphereCollider.radius /= 
                    _stateMachine._model.BossSettings.SphereColliderRadiusDecreace;
            }
            else
            {
                _stateMachine._model.BossSphereCollider.radius = _stateMachine._model.BossSettings.SphereColliderRadius;
            }
        }

        private void InteractionTriggerUpdate()
        {
          _bossModel.ClosestTriggerIndex = _bossModel.InteractionSystem.GetClosestTriggerIndex();
           if(_bossModel.BossCurrentTarget !=null)
            {
                _bossModel.InteractionObject = _bossModel.BossCurrentTarget.GetComponentInChildren<InteractionObject>();
            }
        }

        #endregion
    }
}

