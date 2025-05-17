using UnityEngine;
using System;
using System.Text; // Encodingのために必要

public class ObfuscationUtil
{//難読化するためのスクリプト
    private static string xorKey = "MySecretKey123"; // 任意の鍵（アプリ内で一貫）

    public static string Obfuscate(string plainText)
    {
        var bytes = Encoding.UTF8.GetBytes(plainText);
        for (int i = 0; i < bytes.Length; i++)
        {
            bytes[i] ^= (byte)xorKey[i % xorKey.Length];
        }
        return Convert.ToBase64String(bytes);
    }

    public static string Deobfuscate(string obfuscatedText)
    {
        var bytes = Convert.FromBase64String(obfuscatedText);
        for (int i = 0; i < bytes.Length; i++)
        {
            bytes[i] ^= (byte)xorKey[i % xorKey.Length];
        }
        return Encoding.UTF8.GetString(bytes);
    }
}
