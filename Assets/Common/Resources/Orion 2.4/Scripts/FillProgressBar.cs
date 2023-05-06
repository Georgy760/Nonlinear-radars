using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Common.Resources.Orion_2._4.Scripts
{
    public class FillProgressBar : MonoBehaviour
    {
        Material objectMaterial;

        [SerializeField] Color color = Color.white;
       
        [SerializeField] private TypeSignal shkala_mode;
        private void Awake()
        {
            objectMaterial = new Material(Shader.Find("Shader Graphs/FillProgressBar"));
            gameObject.GetComponent<Renderer>().material = objectMaterial;
            objectMaterial.SetColor("_FillColor", color);
            objectMaterial.SetFloat("_FillRate", 0.079f);
            FindObjectd.OnNearObject += FindObject_OnNearObject;
        }
        private void FindObject_OnNearObject(float obj, TypeSignal type)
        {
            if (type == shkala_mode)
            {
                float _fillStep;
                if (shkala_mode != TypeSignal.None)
                    _fillStep = 0.0055f * (1 - obj) + 0.079f;
                else
                    _fillStep = 0.0055f *  obj + 0.079f;
                Debug.Log("Procent: " + _fillStep);
                if (obj == 0f) _fillStep = 0.079f;
                objectMaterial.SetFloat("_FillRate", _fillStep);
            }
        }
    }
}