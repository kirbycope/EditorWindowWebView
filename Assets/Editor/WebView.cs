using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using UnityEditor;
using UnityEngine;

public class WebView : EditorWindow
{
    static readonly HttpClient httpClient = new HttpClient();
    private string url = "https://getbootstrap.com/docs/5.3/examples/starter-template/";
    private string httpContent;
    Vector2 scrollPos = Vector2.zero;

    private async void GetHttp(string url)
    {
        Debug.Log(url);
        var response = await httpClient.GetAsync(url);
        Debug.Log(response.StatusCode);
        httpContent = response.Content.ReadAsStringAsync().Result;
    }

    private void ParseHttp(string response) { }

    [MenuItem("WebView/Open")]
    static void WebViewOpen()
    {
        WebView webView = (WebView)EditorWindow.GetWindow(typeof(WebView));
        webView.titleContent = new GUIContent("WebView");
        webView.minSize = new Vector2(640, 480);
        webView.Show();
    }

    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button(new GUIContent("←", "Click to go back"), GUILayout.Width(20)))
            Debug.Log("back");
        GUILayout.Button(new GUIContent("→", "Click to go forward"), GUILayout.Width(20));
        GUILayout.Button(new GUIContent("↻", "Reload this page"), GUILayout.Width(20));
        GUILayout.Button(new GUIContent("⌂", "Open the home page"), GUILayout.Width(20));
        url = EditorGUILayout.TextField(url);
        if (GUILayout.Button(new GUIContent("⏎", "Go"), GUILayout.Width(24)))
            GetHttp(url);
        GUILayout.Button(new GUIContent("☰", "Customize and control"), GUILayout.Width(20));
        EditorGUILayout.EndHorizontal();

        //GUI.backgroundColor = Color.yellow;
        //GUILayout.Button(new GUIContent("yellow"));
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.ExpandHeight(true));
        GUIStyle style = new GUIStyle() { alignment = TextAnchor.UpperLeft };
        EditorGUILayout.LabelField(httpContent, style, GUILayout.ExpandHeight(true));
        EditorGUILayout.EndScrollView();
    }
}
