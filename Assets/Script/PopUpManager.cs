using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    //메시지를 표시하는 UI
    public Text errorMessage;

    private static GameObject prefab;

    //PopUpManager로 전역에서 접근할 수 있는 함수
    public static PopUpManager Show(string message)
    {
        if(prefab==null)
        {
            //프리팹이 없다면 프리팹을 불러옵니다.
            prefab = (GameObject)Resources.Load("Error PopUp");
        }

        GameObject obj = Instantiate(prefab);
        PopUpManager errorUi = obj.GetComponent<PopUpManager>();
        errorUi.UpdateContent(message);

        return errorUi;
    }
   
    //팝업의 내용을 갱신하는 함수
    public void UpdateContent(string message)
    {
        errorMessage.text = message;
    }


    //팝업을 닫는 함수
    public void CanclePopUp()
    {
        Destroy(gameObject);
    }
}
