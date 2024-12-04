using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using System;

public class Gameservice : MonoBehaviour
{
    public string environment = "production";
 
    async void Start() {
        try {
            var options = new InitializationOptions()
                .SetEnvironmentName(environment);
 
            await UnityServices.InitializeAsync(options);
            Debug.Log("ゲームサービス初期化できた");
        }
        catch (Exception exception) {
            // An error occurred during initialization.
            Debug.Log("ゲームサービス初期化できなかった");
        }
    }
}
