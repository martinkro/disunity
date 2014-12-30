using Ionic.Zlib;
using UnityEngine;
using System.Collections;
using System;
using System.IO;


public class BuildScript : MonoBehaviour {

	void Start () {

		FileStream objSourceEncryptFile =  new FileStream("~/dump.txt", 
		                                                  FileMode.Open, 
		                                                  FileAccess.ReadWrite);
		FileStream objDestUnEncryptFile = new FileStream("~/DestDecryptFile.dll", 
		                                                 FileMode.OpenOrCreate, 
		                                                 FileAccess.ReadWrite);
		string FileName_HashKey = "dump"; 
		long FileSize = objSourceEncryptFile.Length;
		byte [] ByReadData = new byte[FileSize];

		objSourceEncryptFile.Read(ByReadData, 0, (Int32)FileSize); 
		System.Random random = new System.Random(FileName_HashKey.GetHashCode());

		for (int i = 0; i < ByReadData.Length; i++)
		{
			ByReadData[i] ^= (byte)random.Next(256);
		}

		byte[] Array =  ZlibStream.UncompressBuffer(ByReadData);

		for (int i = 0; i < Array.Length; i++)
		{
			objDestUnEncryptFile.WriteByte(Array[i]);
		}

		
		Debug.Log ("Write finished! ");
	}
}
