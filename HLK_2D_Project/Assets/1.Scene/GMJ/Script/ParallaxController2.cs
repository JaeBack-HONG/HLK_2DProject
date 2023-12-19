using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController2 : MonoBehaviour
{
    Transform cam; // 메인 카메라
    Vector3 camStartPos;
    float distanceX; // 카메라 시작위치, 현재 위치 사이의 거리(X 축)
    float distanceY; // 카메라 시작위치, 현재 위치 사이의 거리(Y 축)

    GameObject[] backgrounds;
    Material[] mat;
    float[] backSpeed;

    float farthestBack;

    [Range(0.01f, 0.05f)]
    public float parallaxSpeed;

    void Start()
    {
        cam = Camera.main.transform;
        camStartPos = cam.position;

        int backCount = transform.childCount;
        mat = new Material[backCount];
        backSpeed = new float[backCount];
        backgrounds = new GameObject[backCount];

        for (int i = 0; i < backCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
            mat[i] = backgrounds[i].GetComponent<Renderer>().material;
        }
        BackSpeedCalculate(backCount);
    }

    void BackSpeedCalculate(int backCount)
    {
        for (int i = 0; i < backCount; i++) // 가장 먼 거리를 찾음
        {
            if ((backgrounds[i].transform.position.z - cam.position.z) > farthestBack)
            {
                farthestBack = backgrounds[i].transform.position.z - cam.position.z;
            }
        }

        for (int i = 0; i < backCount; i++) // 배경 속도 설정
        {
            backSpeed[i] = 1 - (backgrounds[i].transform.position.z - cam.position.z) / farthestBack;
        }
    }

    private void LateUpdate()
    {
        distanceX = cam.position.x - camStartPos.x;
        distanceY = cam.position.y - camStartPos.y;

        transform.position = new Vector3(cam.position.x, 0, 0);

        //for (int i = 0; i < backgrounds.Length; i++)
        //{
        //    float speedX = backSpeed[i] * parallaxSpeed;
        //    float speedY = backSpeed[i] * parallaxSpeed;
        //
        //    // 아래 줄에서 new Vector2(0, distanceY)로 변경하여 Y 축에 대한 이동 값을 추가합니다.
        //    mat[i].SetTextureOffset("_MainTex", new Vector2(distanceX, 0) * speedX + new Vector2(0, distanceY) * speedY);
        //}
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float speedX = backSpeed[i] * parallaxSpeed;
            float speedY = backSpeed[i] * parallaxSpeed;

            // 아래 줄에서 new Vector2(0, distanceY)로 변경하여 Y 축에 대한 이동 값을 추가합니다.
            mat[i].SetTextureOffset("_MainTex", new Vector2(distanceX, 0) * speedX + new Vector2(0, distanceY) * speedY);
        }
    }
}