using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class Equipment : MonoBehaviour
{
    public SpriteRenderer characterRenderer, equipmentRenderer;
    public Vector2 PointerPosition {  get; set; }
    private Animator animator;
    public float delay = 0.3f;
    private bool actionBlocked;
    public SpriteLibrary spriteLibrary;

    private ObjectHealth objHealth;
    private EnemyHealth health;
    public EquipmentData currentEquipment;
    public bool action => actionBlocked;

    public Transform equipmentPivot;
    public Transform circleOrigin;
    public float radius;
    public int facingLeft;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        if (actionBlocked || currentEquipment == null)
        {
            return;
        }
        Vector2 scale = transform.localScale;
        transform.localScale = scale;
    }
    public void Equip(EquipmentData equip)
    {
        currentEquipment = equip;

        equipmentRenderer.sprite = equip.equipSprite;
        spriteLibrary.spriteLibraryAsset = equip.spriteAsset;
        radius = equip.range;

        Debug.Log($"Using equipment {equip.equipType}");
    }
    public void UseEquipment()
    {
        if (actionBlocked || currentEquipment == null)
        {
            return;
        }
        actionBlocked = true;
        animator.SetTrigger("Attack");
    }
    
    
    public void ResetAction()
    {
        actionBlocked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3  position = circleOrigin == null ? Vector3.zero : circleOrigin.position;
        Gizmos.DrawWireSphere(position, radius);
    }

    public void DetectCollider()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position, radius))
        {
            EnemyHealth enemyHealth = collider.GetComponent<EnemyHealth>();
            ObjectHealth objHealth = collider.GetComponent<ObjectHealth>();

            if(enemyHealth !=  null)
            {
                if(currentEquipment.equipType == EquipmentData.EquipmentType.Sword)
                {
                    enemyHealth.GetHit(1, transform.parent.gameObject);
                }
            }

            if(objHealth != null)
            {
                if (currentEquipment.equipType == EquipmentData.EquipmentType.Axe || currentEquipment.equipType == EquipmentData.EquipmentType.Pickaxe)
                {
                    objHealth.GetHit(1, transform.parent.gameObject, currentEquipment);
                }
            }
        }
    }
}
