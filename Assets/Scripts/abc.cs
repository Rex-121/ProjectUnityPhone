using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

using UnityEngine.Networking;

public class abc : MonoBehaviour
{

    public Text Title;
    public InputField ToDoInput;
    public Button AddButton;

    // Start is called before the first frame update
    void Start()
    {

        var submit = Observable.Merge(
                AddButton.OnClickAsObservable().Select(_ => ToDoInput.text),
                ToDoInput.OnEndEditAsObservable().Where(_ => Input.GetKeyDown(KeyCode.Return)));

        // add to reactive collection
        submit.Where(x => x != "")
              .Subscribe(x =>
              {
                  Title.text = x; // clear input field
              });

        ToDoInput.OnValueChangedAsObservable().Subscribe(x => Title.text = x);

        StartCoroutine(Get());
    }

    IEnumerator Get()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get("http://appcourse.roobo.com.cn/pudding/teacher/v1/course/list");
        webRequest.SetRequestHeader("Authorization", "GoRoobo eyJhcHBQYWNrYWdlSUQiOiJvUXdiOEhLQWgiLCJhcHBJRCI6Ik9HSTFaV0l5TkdJNE0yVTMiLCJ0cyI6MjAsImF1dGgiOnsiYXBwVXNlcklEIjoib1E6MjE2MWNjZTdkMDIyN2Q1N2EwYWY0NmQ5NjYwZTI5MWUiLCJhY2Nlc3NUb2tlbiI6ImRlZjgyMjk3OGZjZjdjZTBiMmI4YmY3ZTc0ZWRjOTkyIiwiYWNjZXNzVG9rZW5FeHBpcmVzIjpudWxsLCJyZWZyZXNoVG9rZW4iOiI1OTc4ZjU1MWY5N2Q0NGJhN2FjMmE4YjViODc4YTM3ZiIsInJlZnJlc2hUb2tlbkV4cGlyZXMiOiIyMDIxLTA5LTI5VDE3OjE1OjQ4KzA4OjAwIn0sImFwcCI6eyJ2aWEiOiJhbmRyb2lkIiwiYXBwIjoiTXkgQXBwbGljYXRpb24iLCJhdmVyIjoiMi4wLjEtMTYzMDMxMTY1MDEwNC1vbmxpbmUtd2FuZ3poaWNoZW4iLCJjdmVyIjoiMjM4IiwibW9kZWwiOiIiLCJsb2NhbCI6InpoX0NOIiwiY2giOiIxMDAwMCIsIm5ldCI6IiJ9fQ==.f52ac4de1c5fd7f256f8915e90d867c2");
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(webRequest.error);
        }
        //异常处理，很多博文用了error!=null这是错误的，请看下文其他属性部分
        //if (webRequest.isHttpError || webRequest.isNetworkError)
        //    Debug.Log(webRequest.error);
        else
        {
            Debug.Log(webRequest.downloadHandler.text);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
