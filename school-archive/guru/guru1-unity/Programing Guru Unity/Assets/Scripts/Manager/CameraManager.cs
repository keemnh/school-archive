using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    static public CameraManager instance;

    public GameObject target;   //카메라가 따라갈 대상
    public float moveSpeed;
    private Vector3 targetPosition; //대상의 현재 위치 값

    public BoxCollider2D bound;

    //박스 콜라이더 영역의 최소 최대 xyz값을 지님
    private Vector3 minBound;
    private Vector3 maxBound;

    //카메라의 반너비, 반높이 값을 지닐 변수
    private float halfWidth;
    private float halfHeight;

    //카메라의 반높이 값을 구할 속성을 이용하기 위한 변수
    private Camera theCamera;


    //private void Awake()
    //{
    //    if (instance != null)
    //    {
    //        Destroy(this.gameObject);
    //    }
    //    else
    //    {
    //        DontDestroyOnLoad(this.gameObject);
    //        instance = this;
    //    }
    //}

    // Start is called before the first frame update
    void Start()
    {
        theCamera = GetComponent<Camera>();
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;

    }

    // Update is called once per frame
    void Update()
    {
        if(target.gameObject != null)
        {
            targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);    //this는 이 스크립트가 적용된 개체, 생략도 가능
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);

            float clampedX = Mathf.Clamp(this.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
            float clampedY = Mathf.Clamp(this.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

            this.transform.position = new Vector3(clampedX, clampedY, this.transform.position.z);

        }
    }

    public void SetBound(BoxCollider2D newBound)
    {
        bound = newBound;
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
    }

}
