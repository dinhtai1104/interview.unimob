using UnityEngine;
namespace Helper.CameraView
{
    [ExecuteInEditMode]
    public class ResolutionMatch : MonoBehaviour
    {
        [SerializeField] private Camera mCamera;

        private void Start()
        {
            Match();
        }

        public void Match()
        {
            //độ phân giải tiêu chuẩn
            //720x1560
            //1170x1560
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;
            float screenAspect = screenWidth * 1.0f / screenHeight;
            float milestoneAspect = 9f / 16f;

            if (screenAspect <= milestoneAspect)
            {
                mCamera.orthographicSize = (1080 / 100.0f) / (2 * screenAspect);
            }
            else
            {
                mCamera.orthographicSize = 1920 / 200f;
            }
        }
    }

#if UNITY_EDITOR

    [UnityEditor.CustomEditor(typeof(ResolutionMatch))]
    public class ResolutionMatchEditor : UnityEditor.Editor
    {
        private ResolutionMatch mScript;

        private void OnEnable()
        {
            mScript = (ResolutionMatch)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Match")) { mScript.Match(); }
        }
    }
#endif
}