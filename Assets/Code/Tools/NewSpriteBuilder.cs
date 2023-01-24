using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSpriteBuilder : MonoBehaviour
{
    [SerializeField] private Sprite center;
    [SerializeField] private Sprite right;
    [SerializeField] private Sprite upRight;
    [SerializeField] private Sprite upLeft;
    [SerializeField] private Sprite left;
    [SerializeField] private Sprite downLeft;
    [SerializeField] private Sprite downRight;
    [SerializeField] private string folderName;
    [SerializeField] private string filePrefix;

    /// <summary>
    /// A special method for creating the possible subcombinations for piece connections
    /// </summary>
    [ContextMenu("Run")]
    public void CreateAll()
    {
        if (string.IsNullOrEmpty(folderName))
        {
            Debug.LogError("Specify folder name!");
            return;
        }
        if (string.IsNullOrEmpty(filePrefix))
        {
            Debug.LogError("Specify a file prefix!");
            return;
        }
        string folderPath = Application.dataPath + "/" + folderName;
        System.IO.Directory.CreateDirectory(folderPath);

        Sprite[] useableOrder = new Sprite[] { downRight, downLeft, left, upLeft, upRight, right };

        for (int i = 0; i < 64; i++)
        {
            bool[] flags = new bool[6];
            List<Sprite> usedSprites = new List<Sprite>();
            usedSprites.Add(center);
            for (int h = 0; h < 6; h++)
            {
                int mask = Mathf.RoundToInt(Mathf.Pow(2, h));
                if ((i & mask) > 0)
                {
                    usedSprites.Add(useableOrder[h]);
                    flags[h] = true;
                }
            }
            Texture2D createdTexture = CombineSpritesToTexture(usedSprites);
            byte[] ssPngByte = createdTexture.EncodeToPNG();
            string UID = "";
            for (int f = 0; f < flags.Length; f++)
            {
                UID += flags[f] ? "1" : "0";
            }
            var writeFile = System.IO.File.Create(string.Format("{0}/{1}_{2}.png", folderPath, filePrefix, UID));
            writeFile.Write(ssPngByte);
            writeFile.Flush();
            writeFile.Close();
        }
        Debug.Log("Done creating files. Please refresh Asset window. Remember to update sprite metadata.");
    }

    /// <summary>
    /// Combines the set of provided textures into one texture
    /// </summary>
    /// <param name="spritesToCombine"></param>
    /// <returns></returns>
    Texture2D CombineSpritesToTexture(List<Sprite> spritesToCombine)
    {
        Texture2D texture = new Texture2D(center.texture.width, center.texture.height, TextureFormat.ARGB32, false);
        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                bool draw = false;
                foreach (var s in spritesToCombine)
                {
                    if (s.texture.GetPixel(x, y).a > 0)
                    {
                        draw = true;
                        break;
                    }
                }
                texture.SetPixel(x, y, new Color(1, 1, 1, draw ? 1 : 0));
            }
        }
        texture.Apply();
        return texture;
    }



}
