using System.Collections;
using System.Linq;
using UnityEngine;
using Zenject;
using System;
using Common.Resources.NR_900EMS.Scripts.Display;
using Unity.VisualScripting;
using Common.Resources.NR_900EMS.Scripts.OutPutPower;

namespace Common.Resources.NR_900EMS.Scripts
{
    public class FindObjectd : MonoBehaviour
    {
        // Start is called before the first frame update
        AudioSource _audioSource;
        public static event Action<float, TypeSignal> OnNearObject;
        public static event Action<TypeSignal> OnChangeTypeObject;
        bool _isOn = false;
        GameObject target = null;

        int minPower = 6,maxPower = 20;
        private void OnFinder(bool obj)
        {
            _isOn = obj;
            if (_isOn == false)
                OnNearObject.Invoke(0f, TypeSignal.None);
            else
            {
                float value = Mathf.Clamp01(GetComponent<SphereCollider>().radius / maxPower);
                OnNearObject.Invoke(value, TypeSignal.None);
            }
            Debug.Log("On Finder" + _isOn);
        }


        private void Awake()
        {
            DisplayManager.OnListen += OnFinder;
        }


        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();

            StartCoroutine(CheckObject());
        }

        public void VolumeUp() => _audioSource.volume += 0.1f;
        public void VolumeDown() => _audioSource.volume -= 0.1f;
        

        public void PowerRadiusUpDown(Power power)
        {
            if(power == Power.PMIN){ 
                GetComponent<SphereCollider>().radius = minPower - 1;
                GetComponent<SphereCollider>().center = new Vector3(0, minPower, 0);
            }
            else
            {
                GetComponent<SphereCollider>().radius = maxPower;
                GetComponent<SphereCollider>().center = new Vector3(0, maxPower - 1, 0);
            }

        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("ObjectFind"))
            {
                target = other.gameObject;
                StartCoroutine(CheckObject());
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.transform == target.transform)
            {
                OnNearObject?.Invoke(0f, target.GetComponent<TypeObject>().signal);
                OnChangeTypeObject?.Invoke(TypeSignal.None);
                target = null;
            }
        }
        IEnumerator CheckObject()
        {
            float time = 1f;
            while (true)
            {


                if (target != null && _isOn)
                {
                    var distance = Vector3.Distance(transform.position, target.transform.position);
                    Debug.Log("Find");
                    float maxDistance = GetComponent<SphereCollider>().radius * 2;
                    Debug.Log("Radius: " + maxDistance + "Distance: " + distance);
                    time = Mathf.Clamp01(distance / maxDistance);
                    OnNearObject?.Invoke(time, target.GetComponent<TypeObject>().signal);
                    Debug.Log(time);
                    if (time < 0.5f)
                        OnChangeTypeObject?.Invoke(target.GetComponent<TypeObject>().signal);
                    else
                    {
                        OnChangeTypeObject?.Invoke(TypeSignal.None);
                    }
                    _audioSource.Play();
                }
                else if (target == null)
                {
                    break;
                }
                else
                {
                    OnNearObject?.Invoke(0f, target.GetComponent<TypeObject>().signal);
                }

                yield return new WaitForSeconds(time);
                _audioSource.Stop();
            }
        }

    }
}