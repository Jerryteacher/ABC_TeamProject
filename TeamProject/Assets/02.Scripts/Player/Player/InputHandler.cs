using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class InputHandler : MonoBehaviour
    {
        // Input 관리 스크립트
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;

        public bool b_Input; //구르기 input
        public bool rb_Input;
        public bool rt_Input;

        public bool block_Input;
        public bool blockFlag;

        public bool rollFlag;
        public bool sprintFlag;
        public bool comboFlag;
        public float rollInputTimer;

        // PlayerControls 유니티 자체 패키지 프로그램
        PlayerControls inputActions;
        PlayerAttacker playerAttacker;
        PlayerInventory playerInventory;
        PlayerManager playerManager;
        AnimatorHandler animatorHandler;
    
    Animator ani;
    bool isBlock;

        // 플레이어 움직임
        Vector2 movementInput;

        // 카메라 움직임
        Vector2 cameraInput;

        private void Awake()
        {
            playerAttacker = GetComponent<PlayerAttacker>();
            playerInventory = GetComponent<PlayerInventory>();
            playerManager = GetComponent<PlayerManager>();
        animatorHandler = GetComponentInChildren<AnimatorHandler>();
        }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //isBlock = true;
            //ani.Play("blocking");
            animatorHandler.PlayTargetAnimation("blocking", true);
            GetComponent<CapsuleCollider>().enabled = false;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            animatorHandler.PlayTargetAnimation("Empty", true);
            GetComponent<CapsuleCollider>().enabled = true;
        }
    }


    public void OnEnable()
        {
            // 오브젝트가 활성화 될 때마다 호출
            if (inputActions == null)
            {
                // 선언
                inputActions = new PlayerControls();
                // PlayerControls접근, => 람다 오퍼레이터, movementInput에 인풋 값 저장
                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                //cameraInput에 인풋 값 저장
                inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

                inputActions.Enable();
            }

        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        public void TickInput(float delta)
        {
            //time.delta로 보정
            MoveInput(delta);
            HandleRollInput(delta);
            HandleAttackInput(delta);
        }

        private void MoveInput(float delta) //input system 1.0v 설치해야함.
        {
            //입력 받은 값들 세팅함수
            horizontal = movementInput.x;
            vertical = movementInput.y;

            //얼마나 움직일지, Clamp01: 0과 1 리턴 값 범위, 리턴값 (절대갑 수평 + 절대값 수직)
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            
            //마우스 인풋 저장
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }

        private void HandleRollInput(float delta) //구르기 input
        {
            b_Input = inputActions.PlayerAction.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Started;

            if (b_Input)
            {
                rollInputTimer += delta;
                sprintFlag = true;
            }
            else
            {
                if(rollInputTimer > 0 && rollInputTimer < 0.2f)
                {
                    sprintFlag = false;
                    rollFlag = true;
                }
                rollInputTimer = 0;
            }

        }

        private void HandleAttackInput(float delta)
        {
            inputActions.PlayerAction.RB.performed += i => rb_Input = true;
            inputActions.PlayerAction.RT.performed += i => rt_Input = true;

            inputActions.PlayerAction.Block.performed += i => block_Input = true;

        //RB input handles the RIGHT hand weapon's light attack
        if (rb_Input)
            {
                if (playerManager.canDoCombo)
                {
                    comboFlag = true;
                    playerAttacker.HandleWeaponCombo(playerInventory.rightWeapon);
                    comboFlag = false;
                }
                else
                {
                    if (playerManager.isInteracting)
                        return;

                    if (playerManager.canDoCombo)
                        return;
                    playerAttacker.HandleLightAttack(playerInventory.rightWeapon);

                }
            }
            if (rt_Input)
            {
                playerAttacker.HandleHeavyAttack(playerInventory.rightWeapon);
            }

        }
    }

