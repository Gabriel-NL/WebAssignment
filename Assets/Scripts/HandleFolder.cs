using System;
using System.IO;
using UnityEngine;

public class HandleFolder : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        const string folder_name = "Saves_storage";
        string full_path = Path.Combine(Application.dataPath, folder_name);
        CreateFolder(full_path);
        CreateTxtFiles(full_path, 10);
        PrintFolderContent(full_path);

    }

    public static void CreateFolder(string full_path)
    {


        if (Directory.Exists(full_path) == false)
        {
            Directory.CreateDirectory(full_path);
        }
    }

    public static void PrintFolderContent(string full_path)
    {

        if (Directory.Exists(full_path))
        {
            // Get all the subdirectories
            string[] directories = Directory.GetDirectories(full_path);
            foreach (string directory in directories)
            {
                Debug.Log("Directory: " + Path.GetFileName(directory));
            }

            // Get all the files
            string[] files = Directory.GetFiles(full_path);
            foreach (string file in files)
            {
                Debug.Log("File: " + Path.GetFileName(file));
            }
        }
        else
        {
            Debug.LogError("Directory does not exist: " + full_path);
        }
    }

    public static void CreateTxtFiles(string path, int counter)
    {
        // Ensure the directory exists
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        // Create a random number generator
        System.Random random = new System.Random();

        // Loop to create the file until a unique name is found
        int times_iterating = counter;
        for (int i = 0; i < times_iterating; i++)
        {

            while (true)
            {
                string fileName = $"txt_file_{counter}.txt";
                string fullPath = Path.Combine(path, fileName);

                // Check if the file exists
                if (!File.Exists(fullPath))
                {
                    // Generate a random string of 10 characters (letters only)
                    string randomString = GenerateRandomString(10, random);

                    // Write the random string to the file
                    File.WriteAllText(fullPath, randomString);
                    Console.WriteLine($"File created: {fullPath}");
                    break;
                }
                else
                {
                    // Increment the counter and try again
                    counter++;
                }
            }
        }
    }

    private static string GenerateRandomString(int length, System.Random random)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        char[] stringChars = new char[length];

        for (int i = 0; i < length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        return new string(stringChars);
    }
}
