using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;
using Common;
using UnityEngine.InputSystem.Interactions;
using GameFramework.Event;
using UnityGameFramework.Runtime;

namespace StarForce
{
	///<summary>
	/// 输入系统
	///<summary>

	public class PlayerController : MonoBehaviour
	{
		private CharacterMotor m_CharacterMotor;               
		private PlayerInputControl m_InputControl;
		private PlayerData m_PlayerData;
		private Animator m_Animator;
		private ItemInteractionController m_ItemInteractionController;
		
		public bool isAttacking;

		public void SetPlayerData(PlayerData playerData)
		{
			if (playerData == null)
			{
				Log.Error("PlayerData is null");
				return;
			}
			m_PlayerData = playerData;
			Log.Info($"PlayerData set successfully: {playerData.Name}");
		}

		private void Awake()
		{
			m_CharacterMotor = GetComponent<CharacterMotor>();
			m_InputControl = new PlayerInputControl();
			m_Animator = GetComponentInChildren<Animator>();
			
			m_ItemInteractionController = GetComponent<ItemInteractionController>();
			if (m_ItemInteractionController == null)
			{
				m_ItemInteractionController = gameObject.AddComponent<ItemInteractionController>();
			}
		}

		private void Start()
		{
			StartCoroutine(InitializeController());
		}

		private IEnumerator InitializeController()
		{
			// 等待CharacterMotor完全初始化
			while (m_CharacterMotor == null || !m_CharacterMotor.IsInitialized)
			{
				yield return null;
			}

			// 绑定输入事件
			m_InputControl.Gameplay.Jump.started += Jump;  
			m_InputControl.Gameplay.Move.canceled += MoveCanceled;    
			
			m_InputControl.Gameplay.Attack.started += AttackStart;
			m_InputControl.Gameplay.Attack.performed += AttackPerform;
			m_InputControl.Gameplay.Attack.canceled += AttackCanceled;

			m_InputControl.Gameplay.Shoot.started += ctx => m_CharacterMotor.shoot();
			m_InputControl.Gameplay.Roll.performed += ctx => m_CharacterMotor.Roll();
			m_InputControl.Gameplay.Interaction.performed += OnInteractionPerformed;
		}

		private void AttackStart(InputAction.CallbackContext context)
		{
			if (context.interaction is SlowTapInteraction)
			{              
				m_CharacterMotor.charging();
			}
		}
			
		private void AttackPerform(InputAction.CallbackContext context)
		{
			if (context.interaction is TapInteraction)
			{
				m_CharacterMotor.attack();
			}
			else if (context.interaction is SlowTapInteraction)
			{
				m_CharacterMotor.attackChargeRelease();
			}
		}

		private void AttackCanceled(InputAction.CallbackContext context)
		{
			if (context.interaction is SlowTapInteraction)
			{
				m_Animator.SetBool(m_PlayerData.attackCharging, false);
				m_CharacterMotor.isCharging = false;
			}
		}

		private void FixedUpdate()
		{
			if (m_CharacterMotor == null || !m_CharacterMotor.IsInitialized || m_Animator == null || m_PlayerData == null)
				return;
			
			isAttacking = IsAttacking();
			m_CharacterMotor.Move(m_InputControl.Gameplay.Move.ReadValue<Vector2>());
			m_CharacterMotor.Jump();           
			m_Animator.SetBool(m_PlayerData.isGround, m_CharacterMotor.isGround);
		}

		private bool IsAttacking()
		{
			return m_Animator.GetBool(m_PlayerData.attack01) || 
				   m_Animator.GetBool(m_PlayerData.attack02) || 
				   m_Animator.GetBool(m_PlayerData.attack03);
		}

		private void MoveCanceled(InputAction.CallbackContext context)
		{
			m_Animator.SetBool(m_PlayerData.run, false);
		}

		private void Jump(InputAction.CallbackContext context)
		{
			m_CharacterMotor.prepareJump();            
		}

		private void OnInteractionPerformed(InputAction.CallbackContext context)
		{
			m_ItemInteractionController.InteractWithNearestItem();
		}

		private void OnEnable() => m_InputControl?.Enable();
		private void OnDisable() => m_InputControl?.Disable();

		public PlayerData GetPlayerData() => m_PlayerData;
	}
}
