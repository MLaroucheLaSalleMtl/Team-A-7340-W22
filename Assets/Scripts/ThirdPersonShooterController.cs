using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float baseSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private LayerMask aimColliderMask = new LayerMask();
    [SerializeField] private Transform bullet;
    [SerializeField] private Transform spawnBullet;

    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;
    private Animator animator;

    [SerializeField] private GameObject crosshair;

    private void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        SetAim();

    }

    private void SetAim()
    {
        Vector3 worldPosition = Vector3.zero;

        Vector2 centerPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(centerPoint);
        if(Physics.Raycast(ray, out RaycastHit hit, 999f, aimColliderMask))
        {
            worldPosition = hit.point;
        }
        
        
        //if the character aims
        if (starterAssetsInputs.aim)
        {
            animator.SetBool("Aiming", true);
            //Set the virtual aim camera
            aimVirtualCamera.gameObject.SetActive(true);
            //Set the sensitivity
            thirdPersonController.SetSensitivity(aimSensitivity);
            //Set the rotation when you move
            thirdPersonController.SetRotateOnMove(false);
            //Make the crosshair visible
            crosshair.SetActive(true);
            //Start aiming animation
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

            Vector3 aimTarget = worldPosition;
            aimTarget.y = transform.position.y;
            Vector3 aimDirection = (aimTarget - transform.position).normalized;

            //character rotation
            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
            
            if (starterAssetsInputs.shoot)
            {
                animator.SetTrigger("Shoot");
                Vector3 aimDir = (worldPosition - spawnBullet.position).normalized;
                Instantiate(bullet, spawnBullet.position, Quaternion.LookRotation(aimDir, Vector3.up));
                //Set it back to false so it doesn't shoot constantly
                starterAssetsInputs.shoot = false;
            }
        }
        else
        {
            animator.SetBool("Aiming", false);
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(baseSensitivity);
            thirdPersonController.SetRotateOnMove(true);
            crosshair.SetActive(false);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 0.2f));
        }
    }
}
