using UnityEngine;
using System;
using System.IO;

public class FileInput : MonoBehaviour
{
    public TMPro.TMP_InputField fileInputField; // Reference to the input field in the Unity Inspector
    public TMPro.TMP_InputField keyCodeInputField;

    public string filePath;

    public float[] nums = new float[5];

    public float keyCode;

    public void CheckFolderPathValidity()
    {
        string inputPath = fileInputField.text;

        if (Directory.Exists(inputPath))
        {
            // The folder path is valid; you can proceed with your game logic here.
            Debug.Log("Valid folder path: " + inputPath);
            filePath = inputPath;
        }
        else
        {
            // Display an error message to the player or handle the invalid path.
            Debug.LogWarning("Invalid folder path: " + inputPath);
        }
    }

    public void CreateLevelFiles()
    {
        try
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                string folderPath = Path.Combine(filePath, "Level1");

                Directory.CreateDirectory(folderPath); // Create the directory

                for (int i = 0; i < nums.Length; i++)
                {
                    float randNum = UnityEngine.Random.Range(0, 10.0f);
                    string filename = $"{i}.txt";
                    // Create a new empty text file inside the directory
                    string createdFilePath = Path.Combine(folderPath, filename);
                    using (StreamWriter sw = File.CreateText(createdFilePath))
                    {
                        sw.WriteLine(filename);
                        sw.WriteLine("Hello");
                        sw.WriteLine("Hello again");
                        sw.WriteLine(randNum);
                    }

                    nums[i] = randNum;
                }

                SetKeyCode();
            }
            else
            {
                Debug.LogWarning("Set a filepath first!");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error creating the folder: " + e.Message);
        }
    }

    private void SetKeyCode()
    {
        int randIndex = UnityEngine.Random.Range(0, nums.Length);
        keyCode = nums[randIndex];
        Debug.Log(keyCode);
    }

    public void GuessKeyCode()
    {
        string guess = keyCodeInputField.text;
        if (guess == keyCode.ToString())
        {
            Debug.Log("You guessed correctly! Great job!");
        }
        else
        {
            Debug.Log("Nope, sorry. Try again!");
        }
    }
}