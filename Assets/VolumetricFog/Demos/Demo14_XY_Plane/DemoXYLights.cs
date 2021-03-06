using UnityEngine;
using System.Collections;

namespace VolumetricFogAndMist {
    public class DemoXYLights : MonoBehaviour {

        public Vector3 attractionPos = new Vector3(0, 0, -1f);
        Vector3[] fairyDirections = new Vector3[VolumetricFog.MAX_POINT_LIGHTS];

        void Update() {
            const float accel = 0.01f;

            for (int k = 0; k < VolumetricFog.MAX_POINT_LIGHTS; k++) {
                Light fairy = VolumetricFog.instance.GetPointLight(k);
                if (fairy != null) {
                    fairy.transform.position += fairyDirections[k];
                    Vector3 fairyPos = fairy.transform.position;
                    if (fairyPos.x > attractionPos.x)
                        fairyDirections[k].x -= accel;
                    else
                        fairyDirections[k].x += accel;
                    if (fairyPos.y > attractionPos.y)
                        fairyDirections[k].y -= accel;
                    else
                        fairyDirections[k].y += accel;
                    if (fairyPos.z > attractionPos.z - 1f)
                        fairyDirections[k].z -= accel * 0.01f;
                    else
                        fairyDirections[k].z += accel * 0.01f;
                }
            }

        }
    }
}