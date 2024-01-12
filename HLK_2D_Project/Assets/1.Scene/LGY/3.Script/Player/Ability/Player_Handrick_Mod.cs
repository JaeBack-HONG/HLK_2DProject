using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Handrick_Mod : Ability
{
    [SerializeField] private float gauge = 0f;
    [SerializeField] private float rushSpeed = 13f;
    [SerializeField] private CircleCollider2D spearcol;
    [SerializeField] private float gaugecor = 2f;
    [SerializeField] private Image gaugeUI;
    private bool isRush = false;

    public override void UseAbility()
    {
        if (!isRush) StartCoroutine(HandrickAttack_co());
    }

    private IEnumerator HandrickAttack_co()
    {
        isRush = true;
        P_state.actState = Unit_state.Idle;

        while (Input.GetKey(KeyCode.Z) && gauge <= 3f)
        {
            gauge += Time.fixedDeltaTime * gaugecor;
            gaugeUI.fillAmount = gauge / 3f;
            yield return new WaitForFixedUpdate();
        }

        while (Input.GetKey(KeyCode.Z))
        {
            yield return new WaitForFixedUpdate();
        }
        transform.gameObject.layer = (int)Layer_Index.Hit;
        PlayerManager.instance.UsedAb();
        DefaulutSet("HandrickMod");
        P_state.actState = Unit_state.Idle;
        spearcol.enabled = true;

        while (gauge >= 0 && !Input.GetKeyDown(KeyCode.Z))
        {
            gaugeUI.fillAmount = gauge / 3f;
            gauge -= Time.deltaTime;
            rigidbody.velocity = new Vector2(P_state.direction.x * rushSpeed, rigidbody.velocity.y);
            yield return null;
        }
        gaugeUI.fillAmount = 0;

        transform.gameObject.layer = (int)Layer_Index.Player;
        spearcol.enabled = false;
        EndSet();
        isRush = false;
        yield return null;
    }



}
