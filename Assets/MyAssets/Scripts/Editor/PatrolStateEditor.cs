using UnityEditor;
using UnityEngine;

namespace MyAssets
{
    [CustomEditor(typeof(PatrplPointContainer))]
    public class PatrolStateEditor : Editor
    {
        const string PatrolPointsPropertyName = "patrolPoints";
        SerializedProperty positionArray;
        GUIStyle labelStype;

        private void OnEnable()
        {
            positionArray = serializedObject.FindProperty(PatrolPointsPropertyName);
            labelStype = new GUIStyle
            {
                fontSize = 20,
                fontStyle = FontStyle.Bold,
                padding = new RectOffset
                {
                    top = 2,
                    bottom = 2,
                    left = 2,
                    right = 2,
                },
                normal = new GUIStyleState
                {
                    background = Texture2D.normalTexture,
                },
            };
        }

        private void OnSceneGUI()
        {
            serializedObject.Update();

            DrawPatrolPoints();

            serializedObject.ApplyModifiedProperties();
        }

        void DrawPatrolPoints()
        {
            if (positionArray.arraySize <= 0)
            {
                return;
            }

            Vector3[] points = new Vector3[positionArray.arraySize + 1];
            Color[] colors = new Color[positionArray.arraySize + 1];
            var patrol = target as PatrplPointContainer;
            for (int index = 0; index < positionArray.arraySize; index++)
            {
                var elemnt = positionArray.GetArrayElementAtIndex(index);
                var positionValue = elemnt.vector3Value;
                elemnt.vector3Value = Handles.PositionHandle(positionValue, Quaternion.identity);

                points[index] = positionValue;
                colors[index] = Color.white;

                if (EditorApplication.isPlaying && index == patrol.CurrentPoint)
                {
                    colors[index] = Color.red;
                }

                var size = HandleUtility.GetHandleSize(positionValue);
                Handles.Label(positionValue, $"{index}", labelStype);

            }
            points[positionArray.arraySize] = positionArray.GetArrayElementAtIndex(0).vector3Value;
            colors[positionArray.arraySize] = Color.white;

            const float lineWidth = 6.0f;
            Handles.DrawAAPolyLine(
                lineWidth,
                colors,
                points);
        }

        [DrawGizmo(GizmoType.Selected)]
        static void DrawGizmo(PatrplPointContainer container, GizmoType gizmoType)
        {
            var serializedObject = new SerializedObject(container);
            serializedObject.Update();

            var positionArray = serializedObject.FindProperty(PatrolPointsPropertyName);

            const float radius = 1.0f;
            for (int index = 0; index < positionArray.arraySize; index++)
            {
                var elemnt = positionArray.GetArrayElementAtIndex(index);
                if (EditorApplication.isPlaying && container.CurrentPoint == index)
                {
                    Gizmos.color = Color.red;
                }
                else
                {
                    Gizmos.color = Color.white;
                }
                Gizmos.DrawWireSphere(elemnt.vector3Value, radius);
            }
        }
    }
}
