using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

//Photon을 쓰기 위해 MonoBebaviour 대신에 MonoBehaviourPunCallbacks을 사용
public class PhotonSetting : MonoBehaviourPunCallbacks
{
    public InputField email;
    public InputField password;
    public InputField username;
    public InputField region;
   
    public void LoginSuccess(LoginResult result)
    {
        PhotonNetwork.GameVersion = "1.0f";

        PhotonNetwork.NickName = username.text;

        PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = region.text;

        PhotonNetwork.LoadLevel("Photon Lobby");
    }

    public void LoginFailure(PlayFabError error)
    {
        PopUpManager.Show("Please enter a correct username and password");
    }

    public void SignUpSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("회원가입 성공");
    }

    public void SignUpFailure(PlayFabError error)
    {
        Debug.Log("회원 가입 실패");
    }


    public void SignUp()
    {
        //auto<-자료형 추론
        //var는 모든 자료형을 받을 수 있다.
        // RegisterPlayFabUserRequest : Playfab에 유저를 등록하기 위한 클래스
        var request = new RegisterPlayFabUserRequest
        {
            Email = email.text,
            Password = password.text,
            Username = username.text
        };

        PlayFabClientAPI.RegisterPlayFabUser
        (
           request,
           SignUpSuccess,
           SignUpFailure
        );
    }

    public void Login()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = email.text,
            Password = password.text
        };

        PlayFabClientAPI.LoginWithEmailAddress
            (
            request,
            LoginSuccess,
            LoginFailure
            );
    }
}
