using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControll_Warrior : MonoBehaviour
{
    public static CameraControll_Warrior Instance = null;

    [SerializeField] private CinemachineVirtualCamera cinemachinevir_Intro;
    [SerializeField] private CinemachineVirtualCamera cinemachinevir_Boss;

    [SerializeField] private Player_State player_state;
    [SerializeField] private GameObject WarriorBoss_obj;
    [SerializeField] private GameObject DoorOpen_obj;
    [SerializeField] private GameObject DoorClose_obj;
    [SerializeField] private int priority;

    private CinemachineBasicMultiChannelPerlin noise;
    private Warrior_Boss warrior;
    private bool firstEnter = false;
    public bool start = false;
    public bool warriorIsDie = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        warrior = WarriorBoss_obj.GetComponent<Warrior_Boss>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&&!firstEnter)
        {
            player_state.actState = Unit_state.Default;
            firstEnter = true;
            Debug.Log("인트로 시작");
            cinemachinevir_Intro.Priority = 20;
            DoorController();
            StartCoroutine(WaitCamera());
            StartCoroutine(warrior.WarriorIntroAni());
        }
    }
    IEnumerator WaitCamera()
    {
        player_state.GetComponent<Rigidbody>().velocity = Vector2.zero;
        yield return new WaitForSeconds(5f);
        player_state.actState = Unit_state.Idle;        
        cinemachinevir_Boss.Priority = 30;
    }
    public void WarrriorDieCamera()
    {
        cinemachinevir_Boss.Priority = 5;
    }
    public void WarrriorDieCameraReturn()
    {
        cinemachinevir_Intro.Priority = 5;
    }
    private void DoorController()
    {
        DoorOpen_obj.SetActive(!DoorOpen_obj.activeSelf);
        DoorClose_obj.SetActive(!DoorClose_obj.activeSelf);
    }
}
