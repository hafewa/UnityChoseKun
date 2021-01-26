using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;


namespace Utj.UnityChoseKun
{
    [System.Serializable]
    public class SystemInfoView
    {
        static class Styles
        {
            public static readonly GUIContent BatteryLevel = new GUIContent("batteryLevel", "The current battery level ");
            public static readonly GUIContent BatteryStatus = new GUIContent("batteryStatus", "Returns the current status of the device's battery ");
            public static readonly GUIContent CopyTextureSupport = new GUIContent("copyTextureSupport", "���܂��܂� Graphics.CopyTexture �P�[�X���T�|�[�g���܂�");
            public static readonly GUIContent DeviceModel = new GUIContent("deviceModel", "�f�o�C�X�̃��f��");
            public static readonly GUIContent DeviceName = new GUIContent("deviceName", "�f�o�C�X��");
            public static readonly GUIContent DeviceType = new GUIContent("deviceType", "�f�o�C�X�^�C�v");
            public static readonly GUIContent DeviceUniqueIdentifier = new GUIContent("deviceUniqueIdentifier", "��ӂ̃f�o�C�X���ʎq�B���ׂẴf�o�C�X�ň�ӂł��邱�Ƃ��ۏ؂���Ă��܂�");
            public static readonly GUIContent GraphicsDeviceID = new GUIContent("graphicsDeviceID", "�O���t�B�b�N�f�o�C�X�̎��ʃR�[�h");
            public static readonly GUIContent GraphicsDeviceName = new GUIContent("graphicsDeviceName", "�O���t�B�b�N�f�o�C�X��");
            public static readonly GUIContent GraphicsDeviceType = new GUIContent("graphicsDeviceType", "�f�o�C�X�̃^�C�v��Ԃ��܂�");
            public static readonly GUIContent GraphicsDeviceVendor = new GUIContent("graphicsDeviceVendor", "�O���t�B�b�N�f�o�C�X�̃x���_�[");
            public static readonly GUIContent GraphicsDeviceVendorID = new GUIContent("graphicsDeviceVendorID", "�O���t�B�b�N�f�o�C�X�̃x���_�[�̎��ʃR�[�h");
            public static readonly GUIContent GraphicsDeviceVersion = new GUIContent("graphicsDeviceVersion", "�O���t�B�b�N�f�o�C�X���T�|�[�g���Ă���A�O���t�B�b�N�X API �^�C�v�ƃh���C�o�[�̃o�[�W����");
            public static readonly GUIContent GraphicsMemorySize = new GUIContent("graphicsMemorySize", "�r�f�I�������̗�");
            public static readonly GUIContent GraphicsMultiThreaded = new GUIContent("graphicsMultiThreaded", "�O���t�B�b�N�f�o�C�X���}���`�X���b�h�����_�����O���s�����ǂ���");
            public static readonly GUIContent GraphicsShaderLevel = new GUIContent("graphicsShaderLevel", "�O���t�B�b�N�f�o�C�X�̃V�F�[�_�[�̐��\���x��");
            public static readonly GUIContent GraphicsUVStartsAtTop = new GUIContent("graphicsUVStartsAtTop", "Returns true if the texture UV coordinate convention for this platform has Y starting at the top of the image.");
            public static readonly GUIContent HasDynamicUniformArrayIndexingInFragmentShaders = new GUIContent("hasDynamicUniformArrayIndexingInFragmentShaders", "Returns true when the GPU has native support for indexing uniform arrays in fragment shaders without restrictions.");
            public static readonly GUIContent HasHiddenSurfaceRemovalOnGPU = new GUIContent("hasHiddenSurfaceRemovalOnGPU", "True if the GPU supports hidden surface removal.");
            public static readonly GUIContent HasMipMaxLevel = new GUIContent("hasMipMaxLevel", "Returns true if the GPU supports partial mipmap chains");
            public static readonly GUIContent MaxComputeBufferInputsCompute = new GUIContent("maxComputeBufferInputsCompute", "Determines how many compute buffers Unity supports simultaneously in a compute shader for reading.");
            public static readonly GUIContent MaxComputeBufferInputsDomain = new GUIContent("maxComputeBufferInputsDomain", "Determines how many compute buffers Unity supports simultaneously in a domain shader for reading.");
            public static readonly GUIContent MaxComputeBufferInputsFragment = new GUIContent("maxComputeBufferInputsFragment", "Determines how many compute buffers Unity supports simultaneously in a fragment shader for reading. ");
            public static readonly GUIContent MaxComputeBufferInputsGeometry = new GUIContent("maxComputeBufferInputsGeometry", "Determines how many compute buffers Unity supports simultaneously in a geometry shader for reading. ");
            public static readonly GUIContent MaxComputeBufferInputsHull = new GUIContent("maxComputeBufferInputsHull", "Determines how many compute buffers Unity supports simultaneously in a hull shader for reading. ");
            public static readonly GUIContent MaxComputeBufferInputsVertex = new GUIContent("maxComputeBufferInputsVertex", "Determines how many compute buffers Unity supports simultaneously in a vertex shader for reading. ");
            public static readonly GUIContent MaxComputeWorkGroupSize = new GUIContent("maxComputeWorkGroupSize", "The largest total number of invocations in a single local work group that can be dispatched to a compute shader ");
            public static readonly GUIContent MaxComputeWorkGroupSizeX = new GUIContent("maxComputeWorkGroupSizeX", "The maximum number of work groups that a compute shader can use in X dimension ");
            public static readonly GUIContent MaxComputeWorkGroupSizeY = new GUIContent("maxComputeWorkGroupSizeY", "The maximum number of work groups that a compute shader can use in Y dimension ");
            public static readonly GUIContent MaxComputeWorkGroupSizeZ = new GUIContent("maxComputeWorkGroupSizeZ", "The maximum number of work groups that a compute shader can use in Z dimension ");
            public static readonly GUIContent MaxCubemapSize = new GUIContent("maxCubemapSize", "Maximum Cubemap texture size ");
            public static readonly GUIContent MaxTextureSize = new GUIContent("maxTextureSize", "�e�N�X�`���̍ő�T�C�Y");
            public static readonly GUIContent MinConstantBufferOffsetAlignment = new GUIContent("minConstantBufferOffsetAlignment", "Minimum buffer offset (in bytes) when binding a constant buffer using Shader.SetConstantBuffer or Material.SetConstantBuffer.");
            public static readonly GUIContent NpotSupport = new GUIContent("npotSupport", "GPU �͂ǂ̂悤�� NPOT (2��2��łȂ�) �e�N�X�`���̃T�|�[�g��񋟂��邩�B");
            public static readonly GUIContent OperatingSystem = new GUIContent("operatingSystem", "OS ���ƃo�[�W����");
            public static readonly GUIContent OperatingSystemFamily = new GUIContent("OperatingSystemFamily", "Returns the operating system family the game is running on ");
            public static readonly GUIContent ProcessorCount = new GUIContent("processorCount", "���݂̃v���Z�b�T�[�̐�");
            public static readonly GUIContent ProcessorFrequency = new GUIContent("processorFrequency", "MHz�P�ʂ̃v���Z�b�T�[���g��");
            public static readonly GUIContent ProcessorType = new GUIContent("processorType", "�v���Z�b�T�[��");
            public static readonly GUIContent RenderingThreadingMode = new GUIContent("renderingThreadingMode", "Application's actual rendering threading mode ");
            public static readonly GUIContent SupportedRandomWriteTargetCount = new GUIContent("supportedRandomWriteTargetCount", "The maximum number of random write targets (UAV) that Unity supports simultaneously. ");
            public static readonly GUIContent SupportedRenderTargetCount = new GUIContent("supportedRenderTargetCount", "�T�|�[�g���Ă��郌���_�����O�^�[�Q�b�g�̐�");
            public static readonly GUIContent Supports2DArrayTextures = new GUIContent("supports2DArrayTextures", "2D �z��e�N�X�`�����T�|�[�g����Ă��邩�ǂ���");
            public static readonly GUIContent Supports32bitsIndexBuffer = new GUIContent("supports32bitsIndexBuffer", "Are 32-bit index buffers supported? ");
            public static readonly GUIContent Supports3DRenderTextures = new GUIContent("supports3DRenderTextures", "Are 3D (volume) RenderTextures supported?");
            public static readonly GUIContent Supports3DTextures = new GUIContent("supports3DTextures", "3D (volume) �e�N�X�`�����T�|�[�g����Ă��邩�ǂ���");
            public static readonly GUIContent SupportsAccelerometer = new GUIContent("supportsAccelerometer", "�����x�Z���T�[�𗘗p�ł��邩�ǂ���");
            public static readonly GUIContent SupportsAsyncCompute = new GUIContent("supportsAsyncCompute", "Returns true when the platform supports asynchronous compute queues and false if otherwise.");
            public static readonly GUIContent SupportsAsyncGPUReadback = new GUIContent("supportsAsyncGPUReadback", "Returns true if asynchronous readback of GPU data is available for this device and false otherwise.");
            public static readonly GUIContent SupportsAudio = new GUIContent("supportsAudio", "Is there an Audio device available for playback?");
            public static readonly GUIContent SupportsComputeShaders = new GUIContent("supportsComputeShaders", "Compute �V�F�[�_�[���T�|�[�g����Ă��邩�ǂ���");
            public static readonly GUIContent SupportsCubemapArrayTextures = new GUIContent("supportsCubemapArrayTextures", "Are Cubemap Array textures supported?");
            public static readonly GUIContent SupportsGeometryShaders = new GUIContent("supportsGeometryShaders", "Are geometry shaders supported? ");
            public static readonly GUIContent SupportsGraphicsFence = new GUIContent("supportsGraphicsFence", "Returns true when the platform supports GraphicsFences, and false if otherwise.");
            public static readonly GUIContent SupportsGyroscope = new GUIContent("supportsGyroscope", "Returns true when the platform supports GraphicsFences, and false if otherwise.");
            public static readonly GUIContent SupportsHardwareQuadTopology = new GUIContent("supportsHardwareQuadTopology", "Does the hardware support quad topology? (Read Only)");
            public static readonly GUIContent SupportsInstancing = new GUIContent("supportsInstancing", "GPU �h���[�R�[���̃C���X�^���X�����T�|�[�g����Ă��邩�ǂ���");
            public static readonly GUIContent SupportsLocationService = new GUIContent("supportsLocationService", "���P�[�V�����T�[�r�X�i GPS �j�����p�ł��邩�ǂ���");
            public static readonly GUIContent SupportsMipStreaming = new GUIContent("supportsMipStreaming", "streaming of texture mip maps supported?");
            public static readonly GUIContent SupportsMotionVectors = new GUIContent("supportsMotionVectors", "Whether motion vectors are supported on this platform.");
            public static readonly GUIContent SupportsMultisampleAutoResolve = new GUIContent("supportsMultisampleAutoResolve", "Returns true if multisampled textures are resolved automatically");
            public static readonly GUIContent SupportsMultisampledTextures = new GUIContent("supportsMultisampledTextures", "Are multisampled textures supported?");
            public static readonly GUIContent SupportsRawShadowDepthSampling = new GUIContent("supportsRawShadowDepthSampling", "�T�|�[�g����Ă���V���h�E�}�b�v����̃T���v�����O�͐��̃f�v�X��?");
            public static readonly GUIContent SupportsRayTracing = new GUIContent("supportsRayTracing", "Checks if ray tracing is supported by the current configuration.");
            public static readonly GUIContent SupportsSeparatedRenderTargetsBlend = new GUIContent("supportsSeparatedRenderTargetsBlend", "supportsSeparatedRenderTargetsBlend");
            public static readonly GUIContent SupportsSetConstantBuffer = new GUIContent("supportsSetConstantBuffer", "Does the current renderer support binding constant buffers directly?");
            public static readonly GUIContent SupportsShadows = new GUIContent("SupportsShadows", "�O���t�B�b�N�X�J�[�h���e���T�|�[�g���Ă��邩�ǂ���");
            public static readonly GUIContent SupportsSparseTextures = new GUIContent("supportsSparseTextures", "�X�p�[�X�e�N�X�`���̓T�|�[�g����܂����B");
            public static readonly GUIContent SupportsTessellationShaders = new GUIContent("supportsTessellationShaders", "Are tessellation shaders supported? ");
            public static readonly GUIContent SupportsTextureWrapMirrorOnce = new GUIContent("supportsTextureWrapMirrorOnce", "Returns true if the 'Mirror Once' texture wrap mode is supported. ");
            public static readonly GUIContent SupportsVibration = new GUIContent("supportsVibration", "���̃f�o�C�X�͐U���ɂ�郆�[�U�[�̐G�o�t�B�[�h�o�b�N��񋟂��邱�Ƃ��ł��邩�B");
            public static readonly GUIContent SystemMemorySize = new GUIContent("systemMemorySize", "�V�X�e���������̗�");
            public static readonly GUIContent UnsupportedIdentifier = new GUIContent("unsupportedIdentifier", "���݂̃v���b�g�t�H�[���ŃT�|�[�g����Ă��Ȃ� SystemInfo ������v���p�e�B�[����̖߂�l");
            public static readonly GUIContent UsesLoadStoreActions = new GUIContent("usesLoadStoreActions", "True if the Graphics API takes RenderBufferLoadAction and RenderBufferStoreAction into account, false if otherwise.");
            public static readonly GUIContent UsesReversedZBuffer = new GUIContent("usesReversedZBuffer", "his property is true if the current platform uses a reversed depth buffer (where values range from 1 at the near plane and 0 at far plane), and false if the depth buffer is normal (0 is near, 1 is far). ");

        }



        [SerializeField] SystemInfoKun m_systemInfoKun;
        [SerializeField] Vector2 mScrollPos;


        SystemInfoKun systemInfoKun
        {
            get { if (m_systemInfoKun == null) { m_systemInfoKun = new SystemInfoKun(); } return m_systemInfoKun; }
            set { m_systemInfoKun = value; }
        }



        public void OnGUI()
        {
            mScrollPos = EditorGUILayout.BeginScrollView(mScrollPos);
            EditorGUILayout.FloatField(Styles.BatteryLevel,systemInfoKun.batteryLevel);
            EditorGUILayout.EnumPopup(Styles.BatteryStatus, systemInfoKun.batteryStatus);
            EditorGUILayout.EnumPopup(Styles.CopyTextureSupport, systemInfoKun.copyTextureSupport);
            EditorGUILayout.TextField(Styles.DeviceModel, systemInfoKun.deviceModel);
            EditorGUILayout.TextField(Styles.DeviceName, systemInfoKun.deviceName);
            EditorGUILayout.EnumPopup(Styles.DeviceType, systemInfoKun.deviceType);
            EditorGUILayout.TextField(Styles.DeviceUniqueIdentifier, systemInfoKun.deviceUniqueIdentifier);
            EditorGUILayout.IntField(Styles.GraphicsDeviceID,systemInfoKun.graphicsDeviceID);
            EditorGUILayout.TextField(Styles.DeviceName,systemInfoKun.deviceName);
            EditorGUILayout.EnumPopup(Styles.GraphicsDeviceType,systemInfoKun.graphicsDeviceType);
            EditorGUILayout.TextField(Styles.GraphicsDeviceVendor,systemInfoKun.graphicsDeviceVendor);
            EditorGUILayout.IntField(Styles.GraphicsDeviceVendorID,systemInfoKun.graphicsDeviceVendorID);
            EditorGUILayout.TextField(Styles.GraphicsDeviceVersion, systemInfoKun.graphicsDeviceVersion);
            EditorGUILayout.IntField(Styles.GraphicsMemorySize, systemInfoKun.graphicsMemorySize);
            EditorGUILayout.Toggle(Styles.GraphicsMultiThreaded,systemInfoKun.graphicsMultiThreaded);
            EditorGUILayout.IntField(Styles.GraphicsShaderLevel, systemInfoKun.graphicsShaderLevel);
            EditorGUILayout.Toggle(Styles.GraphicsUVStartsAtTop,systemInfoKun.graphicsUVStartsAtTop);
            EditorGUILayout.Toggle(Styles.HasDynamicUniformArrayIndexingInFragmentShaders,systemInfoKun.hasDynamicUniformArrayIndexingInFragmentShaders);
            EditorGUILayout.Toggle(Styles.HasHiddenSurfaceRemovalOnGPU, systemInfoKun.hasHiddenSurfaceRemovalOnGPU);
            EditorGUILayout.Toggle(Styles.HasMipMaxLevel, systemInfoKun.hasMipMaxLevel);
            EditorGUILayout.IntField(Styles.MaxComputeBufferInputsCompute, systemInfoKun.maxComputeBufferInputsCompute);
            EditorGUILayout.IntField(Styles.MaxComputeBufferInputsDomain, systemInfoKun.maxComputeBufferInputsDomain);
            EditorGUILayout.IntField(Styles.MaxComputeBufferInputsFragment, systemInfoKun.maxComputeBufferInputsFragment);
            EditorGUILayout.IntField(Styles.MaxComputeBufferInputsGeometry, systemInfoKun.maxComputeBufferInputsGeometry);
            EditorGUILayout.IntField(Styles.MaxComputeBufferInputsHull, systemInfoKun.maxComputeBufferInputsHull);
            EditorGUILayout.IntField(Styles.MaxComputeBufferInputsVertex, systemInfoKun.maxComputeBufferInputsVertex);
            EditorGUILayout.IntField(Styles.MaxComputeWorkGroupSize, systemInfoKun.maxComputeWorkGroupSize);
            EditorGUILayout.IntField(Styles.MaxComputeWorkGroupSizeX, systemInfoKun.maxComputeWorkGroupSizeX);
            EditorGUILayout.IntField(Styles.MaxComputeWorkGroupSizeY, systemInfoKun.maxComputeWorkGroupSizeY);
            EditorGUILayout.IntField(Styles.MaxComputeWorkGroupSizeZ, systemInfoKun.maxComputeWorkGroupSizeZ);
            EditorGUILayout.IntField(Styles.MaxCubemapSize, systemInfoKun.maxCubemapSize);
            EditorGUILayout.IntField(Styles.MaxTextureSize, systemInfoKun.maxTextureSize);
            EditorGUILayout.Toggle(Styles.MinConstantBufferOffsetAlignment, systemInfoKun.minConstantBufferOffsetAlignment);
            EditorGUILayout.EnumPopup(Styles.NpotSupport, systemInfoKun.npotSupport);
            EditorGUILayout.TextField(Styles.OperatingSystem, systemInfoKun.operatingSystem);
            EditorGUILayout.EnumPopup(Styles.OperatingSystemFamily, systemInfoKun.operatingSystemFamily);
            EditorGUILayout.IntField(Styles.ProcessorCount,systemInfoKun.processorCount);
            EditorGUILayout.IntField(Styles.ProcessorFrequency, systemInfoKun.processorFrequency);
            EditorGUILayout.TextField(Styles.ProcessorType, systemInfoKun.processorType);
            EditorGUILayout.EnumPopup(Styles.RenderingThreadingMode,systemInfoKun.renderingThreadingMode);
            EditorGUILayout.IntField(Styles.SupportedRandomWriteTargetCount,systemInfoKun.supportedRandomWriteTargetCount);
            EditorGUILayout.IntField(Styles.SupportedRenderTargetCount,systemInfoKun.supportedRenderTargetCount);
            EditorGUILayout.Toggle(Styles.Supports2DArrayTextures,systemInfoKun.supports2DArrayTextures);
            EditorGUILayout.Toggle(Styles.Supports32bitsIndexBuffer, systemInfoKun.supports32bitsIndexBuffer);
            EditorGUILayout.Toggle(Styles.Supports3DRenderTextures,systemInfoKun.supports3DRenderTextures);
            EditorGUILayout.Toggle(Styles.Supports3DTextures, systemInfoKun.supports3DTextures);
            EditorGUILayout.Toggle(Styles.SupportsAccelerometer,systemInfoKun.supportsAccelerometer);
            EditorGUILayout.Toggle(Styles.SupportsAsyncCompute,systemInfoKun.supportsAsyncCompute);
            EditorGUILayout.Toggle(Styles.SupportsAsyncGPUReadback,systemInfoKun.supportsAsyncGPUReadback);
            EditorGUILayout.Toggle(Styles.SupportsAudio,systemInfoKun.supportsAudio);
            EditorGUILayout.Toggle(Styles.SupportsComputeShaders, systemInfoKun.supportsComputeShaders);
            EditorGUILayout.Toggle(Styles.SupportsCubemapArrayTextures, systemInfoKun.supportsCubemapArrayTextures);
            EditorGUILayout.Toggle(Styles.SupportsGeometryShaders,systemInfoKun.supportsGeometryShaders);
            EditorGUILayout.Toggle(Styles.SupportsGraphicsFence,systemInfoKun.supportsGraphicsFence);
            EditorGUILayout.Toggle(Styles.SupportsGyroscope, systemInfoKun.supportsGyroscope);
            EditorGUILayout.Toggle(Styles.SupportsHardwareQuadTopology,systemInfoKun.supportsHardwareQuadTopology);
            EditorGUILayout.Toggle(Styles.SupportsInstancing, systemInfoKun.supportsInstancing);
            EditorGUILayout.Toggle(Styles.SupportsLocationService, systemInfoKun.supportsLocationService);
            EditorGUILayout.Toggle(Styles.SupportsMipStreaming,systemInfoKun.supportsMipStreaming);
            EditorGUILayout.Toggle(Styles.SupportsMotionVectors, systemInfoKun.supportsMotionVectors);
            EditorGUILayout.Toggle(Styles.SupportsMultisampleAutoResolve, systemInfoKun.supportsMultisampleAutoResolve);
            EditorGUILayout.IntField(Styles.SupportsMultisampledTextures, systemInfoKun.supportsMultisampledTextures);
            EditorGUILayout.Toggle(Styles.SupportsRawShadowDepthSampling, systemInfoKun.supportsRawShadowDepthSampling);
            EditorGUILayout.Toggle(Styles.SupportsRayTracing,systemInfoKun.supportsRayTracing);
            EditorGUILayout.Toggle(Styles.SupportsSeparatedRenderTargetsBlend,systemInfoKun.supportsSeparatedRenderTargetsBlend);
            EditorGUILayout.Toggle(Styles.SupportsSetConstantBuffer,systemInfoKun.supportsSetConstantBuffer);
            EditorGUILayout.Toggle(Styles.SupportsSparseTextures,systemInfoKun.supportsSparseTextures);
            EditorGUILayout.Toggle(Styles.SupportsTessellationShaders,systemInfoKun.supportsTessellationShaders);
            EditorGUILayout.IntField(Styles.SupportsTextureWrapMirrorOnce, systemInfoKun.supportsTextureWrapMirrorOnce);
            EditorGUILayout.IntField(Styles.SystemMemorySize, systemInfoKun.systemMemorySize);
            EditorGUILayout.TextField(Styles.UnsupportedIdentifier,systemInfoKun.unsupportedIdentifier);
            EditorGUILayout.Toggle(Styles.UsesLoadStoreActions,systemInfoKun.usesLoadStoreActions);
            EditorGUILayout.Toggle(Styles.UsesReversedZBuffer,systemInfoKun.usesReversedZBuffer);
            EditorGUILayout.EndScrollView();


            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Pull"))
            {
                UnityChoseKunEditor.SendMessage<SystemInfoKun>(UnityChoseKun.MessageID.SystemInfoPull, null);
            }
            EditorGUILayout.EndHorizontal();
        }



        public void OnMessageEvent(BinaryReader binaryReader)
        {
            systemInfoKun.Deserialize(binaryReader);
        }


    }
}
