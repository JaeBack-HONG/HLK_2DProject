using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController2 : MonoBehaviour
{
    Transform cam; // ���� ī�޶�
    Vector3 camStartPos;
    float distanceX; // ī�޶� ������ġ, ���� ��ġ ������ �Ÿ�(X ��)
    float distanceY; // ī�޶� ������ġ, ���� ��ġ ������ �Ÿ�(Y ��)

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
        for (int i = 0; i < backCount; i++) // ���� �� �Ÿ��� ã��
        {
            if ((backgrounds[i].transform.position.z - cam.position.z) > farthestBack)
            {
                farthestBack = backgrounds[i].transform.position.z - cam.position.z;
            }
        }

        for (int i = 0; i < backCount; i++) // ��� �ӵ� ����
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
        //    // �Ʒ� �ٿ��� new Vector2(0, distanceY)�� �����Ͽ� Y �࿡ ���� �̵� ���� �߰��մϴ�.
        //    mat[i].SetTextureOffset("_MainTex", new Vector2(distanceX, 0) * speedX + new Vector2(0, distanceY) * speedY);
        //}
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float speedX = backSpeed[i] * parallaxSpeed;
            float speedY = backSpeed[i] * parallaxSpeed;

            // �Ʒ� �ٿ��� new Vector2(0, distanceY)�� �����Ͽ� Y �࿡ ���� �̵� ���� �߰��մϴ�.
            mat[i].SetTextureOffset("_MainTex", new Vector2(distanceX, 0) * speedX + new Vector2(0, distanceY) * speedY);
        }
    }
}