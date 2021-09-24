using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; //UI를 사용하려면 UnityEngine.UI 라이브러리 적용 필수

public class GameManagerLogic : MonoBehaviour
{
    public int totalItemCount; //아이템 총개수
    public int stage;
    public Text stageCountText;
    public Text playerCountText;

    void Awake()
    {
        stageCountText.text = "/ " + totalItemCount;
    }

    public void GetItem(int count)
    {
        playerCountText.text = count.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            SceneManager.LoadScene(stage); 
        //LoadScene의 매개변수는 장면 순서(int, Build Settings - scene index(오른쪽에 써져있는 숫자)도 가능
    }
}
//다음 스테이지 : 기존 스테이지의 모든 오브젝트를 복사해서 붙여넣기로 진행