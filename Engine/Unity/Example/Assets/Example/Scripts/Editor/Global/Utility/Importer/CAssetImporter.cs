using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

/** 에셋 추가자 */
public class CAssetImporter : AssetPostprocessor {
	/** 텍스처가 추가 되었을 경우 */
	public void OnPreprocessTexture() {
		var oTexImporter = this.assetImporter as TextureImporter;
		oTexImporter.spritePixelsPerUnit = 1.0f;
	}
}
#endif // #if UNITY_EDITOR
