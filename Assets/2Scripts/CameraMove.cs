using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Transform playerTransform;
    Vector3 Offset; //조정값
    
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //FindGameObjectWithTag : 주어진 태그로 오브젝트 검색
        Offset = transform.position - playerTransform.position;
    }

    void LateUpdate() //카메라 이동, ui업데이트 업데이트 연산 이후 실행(뒤에 따라 붙는 것들은 여기다)
    {
        transform.position = playerTransform.position + Offset;
    }
}
//물리적으로 맞닿지 않는 오브젝트는 Coliider - isTrigger 체크