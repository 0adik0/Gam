using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BuildAssetBundlesScript : Editor
{
    [MenuItem("Assets/Build Asset Bundle")]
    static void BuildAllAssetBundles() {
        BuildPipeline.BuildAssetBundles(Application.persistentDataPath, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.iOS);
    }
}
