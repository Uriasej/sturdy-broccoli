﻿using UObject = UnityEngine.Object;

namespace UnityEngine.ProBuilder
{
    internal partial class SelectionPickerRenderer
    {
        internal class SelectionPickerRendererHDRP : ISelectionPickerRenderer
        {

            public Texture2D RenderLookupTexture(
                Camera camera,
                Shader shader,
                string tag,
                int width = -1,
                int height = -1)
            {
#if HDRP_7_1_0_OR_NEWER
                bool autoSize = width < 0 || height < 0;

                int _width = autoSize ? (int)camera.pixelRect.width : width;
                int _height = autoSize ? (int)camera.pixelRect.height : height;

                GameObject go = new GameObject();
                Camera renderCam = go.AddComponent<Camera>();
                renderCam.CopyFrom(camera);

                renderCam.renderingPath = RenderingPath.Forward;
                renderCam.enabled = false;
                renderCam.clearFlags = CameraClearFlags.SolidColor;
                renderCam.backgroundColor = Color.white;
                renderCam.allowHDR = false;
                renderCam.allowMSAA = false;
                renderCam.forceIntoRenderTexture = true;

                Rendering.HighDefinition.HDAdditionalCameraData hdCamData = go.AddComponent<Rendering.HighDefinition.HDAdditionalCameraData>();
                hdCamData.flipYMode = Rendering.HighDefinition.HDAdditionalCameraData.FlipYMode.ForceFlipY;
                hdCamData.customRender += CustomRenderPass;

                RenderTextureDescriptor descriptor = new RenderTextureDescriptor()
                {
                    width = _width,
                    height = _height,
                    colorFormat = renderTextureFormat,
                    autoGenerateMips = false,
                    depthBufferBits = 16,
                    dimension = UnityEngine.Rendering.TextureDimension.Tex2D,
                    enableRandomWrite = false,
                    memoryless = RenderTextureMemoryless.None,
                    sRGB = false,
                    useMipMap = false,
                    volumeDepth = 1,
                    msaaSamples = 1
                };
                RenderTexture rt = RenderTexture.GetTemporary(descriptor);

                RenderTexture prev = RenderTexture.active;
                renderCam.targetTexture = rt;
                RenderTexture.active = rt;

                renderCam.Render();

                Texture2D img = new Texture2D(_width, _height, textureFormat, false, false);
                img.ReadPixels(new Rect(0, 0, _width, _height), 0, 0);
                img.Apply();

                RenderTexture.active = prev;
                RenderTexture.ReleaseTemporary(rt);

                UObject.DestroyImmediate(go);

                return img;
#else
                return null;
#endif
            }

#if HDRP_7_1_0_OR_NEWER
            static void CustomRenderPass(Rendering.ScriptableRenderContext ctx, Rendering.HighDefinition.HDCamera camera)
            {
                ctx.SetupCameraProperties(camera.camera);

                Rendering.CommandBuffer cb = new Rendering.CommandBuffer();
                cb.ClearRenderTarget(true, true, Color.white);
                ctx.ExecuteCommandBuffer(cb);
                ctx.Submit();

                Rendering.DrawingSettings drawSettings = new Rendering.DrawingSettings();
                drawSettings.SetShaderPassName(0, new Rendering.ShaderTagId("Always"));

                Rendering.FilteringSettings filterSettings = Rendering.FilteringSettings.defaultValue;

                if (camera.camera.TryGetCullingParameters(out Rendering.ScriptableCullingParameters cullParams))
                {
                    Rendering.CullingResults cullResuts = ctx.Cull(ref cullParams);
                    ctx.DrawRenderers(cullResuts, ref drawSettings, ref filterSettings);
                }
            }
#endif
            }
    }
}
