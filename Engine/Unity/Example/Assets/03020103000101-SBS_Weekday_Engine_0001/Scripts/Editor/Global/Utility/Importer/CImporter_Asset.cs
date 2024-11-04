using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

/*
 * AssetPostprocessor 클래스는 Unity 에디터 상에서 특정 에셋들을 로드 될 때 발생하는
 * 이벤트를 처리하는 역할을 수행한다. (즉, 해당 클래스를 활용하면 에셋이 추가 되는 과정에서
 * 특정 옵션을 자동으로 설정하도록 구문을 작성하는 것이 가능하다.)
 */
/** 에셋 추가자 */
public partial class CImporter_Asset : AssetPostprocessor
{
	/** 텍스처가 추가 되었을 경우 */
	private void OnPostprocessTexture(Texture2D a_oTex2D)
	{
		// 텍스처 추가자가 존재 할 경우
		if(this.assetImporter is TextureImporter)
		{
			var oTexImporter = this.assetImporter as TextureImporter;
			oTexImporter.alphaIsTransparency = true;
			oTexImporter.spritePixelsPerUnit = 1.0f;

			oTexImporter.alphaSource = TextureImporterAlphaSource.FromInput;
			oTexImporter.mipmapEnabled = oTexImporter.textureType != TextureImporterType.Sprite;
		}
	}
}
#endif // #if UNITY_EDITOR
