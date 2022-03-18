using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GAP_LaserSystem;

[CreateAssetMenu]
public class LaserAbility : Ability
{
    public override void TriggerAbility()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().laser.SetActive(true);

        GameObject.Find("GameManager").GetComponent<GameManager>().laser.GetComponent<LaserScript>().ShootLaser(activeTime);
    }
}
