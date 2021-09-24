using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyBall : MonoBehaviour
{
    public float jumpPower; //public 붙이면 초기화는 안해도 됨 실시간으로 테스트(특히 숫자의 경우)
    public int itemCount;
    public GameManagerLogic manager;
    bool isJump; 
    Rigidbody rigid;
    AudioSource audio;

    void Awake()
    {
        isJump = false;
        rigid = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && !isJump)
        {
            isJump = true;
            rigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal"); //왼쪽 오른쪽
        float v = Input.GetAxisRaw("Vertical"); //위 아래 (앞 뒤)
        Vector3 vec = new Vector3(h, 0, v);

        rigid.AddForce(vec, ForceMode.Impulse);     
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
            isJump = false;
    }

    /*
    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Cube")
            rigid.AddForce(Vector3.up * 2, ForceMode.Impulse);
    }*/

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item") //name으로 비교하면 동일한 다수의 오브젝트와 이벤트 발생 x
        {//Tag : 오브젝트를 구분하는 단순한 문자열
         //shift 누르면 Hierarchy에서 다중 선택 가능
            itemCount++;
            audio.Play();
            //사운드 재생, 비활성화 구간에는 컴포넌트 함수가 실행되지 않을 수 있음!
            other.gameObject.SetActive(false);
            //gameObject : 자기 자신, 처음부터 가지고 있음
            //SetActive(bool) : 오브젝트 활성화 함수(이름 옆에 체크 표시)
            manager.GetItem(itemCount);
        }

        else if (other.tag == "Finish")
        {
            if(itemCount == manager.totalItemCount) {
                //Game Clear! && Next Stage
                if (manager.stage == 2)
                    SceneManager.LoadScene(0);
                else 
                    SceneManager.LoadScene(manager.stage + 1);
            }
            else {
            
                //Restart Stage
                SceneManager.LoadScene(manager.stage); 
                //문자열에 숫자를 더하면 그 숫자도 문자열이 됨 ex)"ex-"+1 -> "ex-1"
                //.ToString()으로 문자열로 변환
                //->Scene index로 쓰는게 편함
                //SceneManager : 장면을 관리하는 기본 클래스
                //LoadScene(SceneName) : 주어진 장면을 불러오는 함수
                //Scene을 불러오려면 꼭 Build Setting에서 추가
            }

        }
    }
}
//AudioSource : 사운드 재생 컴포넌트, AudioClip 필요
//AudioClip : 사운드 파일 컴포넌트
//형태가 없고 전반적인 로직을 가진 오브젝트를 매니저로 지정
//GameObject.Find~ : Find계열 함수는 부하를 초래할 수 있으므로 피하는 것을 권장 
//-> 변수로 받아쓰기(ex.여기서 manager변수의 경우)
//UI로 친절한 게임을 만들자
