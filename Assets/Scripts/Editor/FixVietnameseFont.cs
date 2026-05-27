
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using TMPro;
using System.IO;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// Editor script to fix Vietnamese font rendering in the About panel.
/// Run from menu: Tools > Fix Vietnamese Font
/// 
/// This script will:
/// 1. Find or create a Vietnamese-compatible TMP SDF font asset
/// 2. Replace the FreckleFace font with the new font in the Setting menu prefab
/// 3. Also fix font references in the GameEnd prefab
/// </summary>
public class FixVietnameseFont : EditorWindow
{
    // Vietnamese character set including all diacritics
    private const string VIETNAMESE_CHARS =
        // Basic Latin
        "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789 " +
        "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~" +
        // Vietnamese specific characters
        "\u00C0\u00C1\u00C2\u00C3\u00C8\u00C9\u00CA\u00CC\u00CD\u00D2\u00D3\u00D4\u00D5\u00D9\u00DA\u00DD" + // ÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚÝ
        "\u00E0\u00E1\u00E2\u00E3\u00E8\u00E9\u00EA\u00EC\u00ED\u00F2\u00F3\u00F4\u00F5\u00F9\u00FA\u00FD" + // àáâãèéêìíòóôõùúý
        "\u0102\u0103\u0110\u0111\u0128\u0129\u0168\u0169\u01A0\u01A1\u01AF\u01B0" + // ĂăĐđĨĩŨũƠơƯư
        "\u1EA0\u1EA1\u1EA2\u1EA3\u1EA4\u1EA5\u1EA6\u1EA7\u1EA8\u1EA9\u1EAA\u1EAB\u1EAC\u1EAD\u1EAE\u1EAF" + // ẠạẢảẤấẦầẨẩẪẫẬậẮắ
        "\u1EB0\u1EB1\u1EB2\u1EB3\u1EB4\u1EB5\u1EB6\u1EB7\u1EB8\u1EB9\u1EBA\u1EBB\u1EBC\u1EBD\u1EBE\u1EBF" + // ẰằẲẳẴẵẶặẸẹẺẻẼẽẾế
        "\u1EC0\u1EC1\u1EC2\u1EC3\u1EC4\u1EC5\u1EC6\u1EC7\u1EC8\u1EC9\u1ECA\u1ECB\u1ECC\u1ECD\u1ECE\u1ECF" + // ỀềỂểỄễỆệỈỉỊịỌọỎỏ
        "\u1ED0\u1ED1\u1ED2\u1ED3\u1ED4\u1ED5\u1ED6\u1ED7\u1ED8\u1ED9\u1EDA\u1EDB\u1EDC\u1EDD\u1EDE\u1EDF" + // ỐốỒồỔổỖỗỘộỚớỜờỞở
        "\u1EE0\u1EE1\u1EE2\u1EE3\u1EE4\u1EE5\u1EE6\u1EE7\u1EE8\u1EE9\u1EEA\u1EEB\u1EEC\u1EED\u1EEE\u1EEF" + // ỠỡỢợỤụỦủỨứỪừỬửỮữ
        "\u1EF0\u1EF1\u1EF2\u1EF3\u1EF4\u1EF5\u1EF6\u1EF7\u1EF8\u1EF9"; // ỰựỲỳỴỵỶỷỸỹ

    [MenuItem("Tools/Fix Vietnamese Font")]
    public static void ShowWindow()
    {
        GetWindow<FixVietnameseFont>("Fix Vietnamese Font");
    }

    private TMP_FontAsset vietnameseFont;
    private Font sourceTTF;

    private void OnGUI()
    {
        GUILayout.Label("Fix Vietnamese Font for About Panel", EditorStyles.boldLabel);
        GUILayout.Space(10);

        EditorGUILayout.HelpBox(
            "This tool will replace the FreckleFace font (which doesn't support Vietnamese) " +
            "with a Vietnamese-compatible font in the Setting menu and GameEnd prefabs.\n\n" +
            "Option 1: Drag a Vietnamese-compatible TMP SDF Font Asset below.\n" +
            "Option 2: Drag a .ttf font file and click 'Create SDF Font' to generate one.",
            MessageType.Info);

        GUILayout.Space(10);

        GUILayout.Label("Option 1: Use existing TMP SDF Font Asset", EditorStyles.boldLabel);
        vietnameseFont = (TMP_FontAsset)EditorGUILayout.ObjectField("TMP Font Asset:", vietnameseFont, typeof(TMP_FontAsset), false);

        GUILayout.Space(10);

        GUILayout.Label("Option 2: Create from TTF file", EditorStyles.boldLabel);
        sourceTTF = (Font)EditorGUILayout.ObjectField("Source TTF Font:", sourceTTF, typeof(Font), false);

        if (sourceTTF != null && GUILayout.Button("Create SDF Font Asset"))
        {
            CreateSDFFontAsset();
        }

        GUILayout.Space(20);

        GUI.enabled = vietnameseFont != null;
        if (GUILayout.Button("Apply Font to All Prefabs", GUILayout.Height(40)))
        {
            ApplyFontToPrefabs();
        }
        GUI.enabled = true;
    }

    private void CreateSDFFontAsset()
    {
        if (sourceTTF == null)
        {
            EditorUtility.DisplayDialog("Error", "Please assign a TTF font file first.", "OK");
            return;
        }

        // Create font asset using TMP's built-in method
        string fontPath = AssetDatabase.GetAssetPath(sourceTTF);
        string fontName = Path.GetFileNameWithoutExtension(fontPath);
        string outputPath = $"Assets/Font/{fontName} SDF.asset";

        // Create font asset
        TMP_FontAsset fontAsset = TMP_FontAsset.CreateFontAsset(sourceTTF);
        if (fontAsset == null)
        {
            EditorUtility.DisplayDialog("Error", "Failed to create SDF font asset.", "OK");
            return;
        }

        // Set atlas population mode to Dynamic so characters are generated on-the-fly
        fontAsset.atlasPopulationMode = AtlasPopulationMode.Dynamic;

        AssetDatabase.CreateAsset(fontAsset, outputPath);

        // Create material
        Material fontMaterial = fontAsset.material;
        if (fontMaterial != null)
        {
            string materialPath = $"Assets/Font/{fontName} SDF.mat";
            // Material is embedded in the font asset, no need to create separately
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        vietnameseFont = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(outputPath);

        EditorUtility.DisplayDialog("Success",
            $"SDF Font Asset created at:\n{outputPath}\n\n" +
            "The font is set to Dynamic mode so it will generate Vietnamese glyphs on-the-fly.\n\n" +
            "Now click 'Apply Font to All Prefabs' to update the prefabs.",
            "OK");
    }

    private void ApplyFontToPrefabs()
    {
        if (vietnameseFont == null)
        {
            EditorUtility.DisplayDialog("Error", "Please assign a Vietnamese-compatible TMP font asset first.", "OK");
            return;
        }

        int totalReplaced = 0;

        // Fix Setting menu prefab
        string settingMenuPath = "Assets/Prefabs/Setting menu.prefab";
        totalReplaced += FixPrefab(settingMenuPath);

        // Fix GameEnd prefab
        string gameEndPath = "Assets/Prefabs/GameEnd.prefab";
        totalReplaced += FixPrefab(gameEndPath);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EditorUtility.DisplayDialog("Done",
            $"Replaced {totalReplaced} font references across prefabs.\n" +
            "Vietnamese text should now render correctly in all scenes.",
            "OK");
    }

    private int FixPrefab(string prefabPath)
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
        if (prefab == null)
        {
            Debug.LogWarning($"Prefab not found at: {prefabPath}");
            return 0;
        }

        // Get all TMP_Text components in the prefab
        TMP_Text[] textComponents = prefab.GetComponentsInChildren<TMP_Text>(true);
        int replaced = 0;

        foreach (var text in textComponents)
        {
            if (text.font != vietnameseFont)
            {
                // Use SerializedObject for proper prefab editing
                SerializedObject so = new SerializedObject(text);
                SerializedProperty fontProp = so.FindProperty("m_fontAsset");
                SerializedProperty matProp = so.FindProperty("m_sharedMaterial");

                if (fontProp != null)
                {
                    fontProp.objectReferenceValue = vietnameseFont;
                }
                if (matProp != null)
                {
                    matProp.objectReferenceValue = vietnameseFont.material;
                }

                so.ApplyModifiedProperties();
                replaced++;
                Debug.Log($"Replaced font on '{text.gameObject.name}' in {prefabPath} | Text: {text.text}");
            }
        }

        if (replaced > 0)
        {
            EditorUtility.SetDirty(prefab);
            PrefabUtility.SavePrefabAsset(prefab);
            Debug.Log($"Fixed {replaced} TMP text components in {prefabPath}");
        }

        return replaced;
    }
}
#endif
