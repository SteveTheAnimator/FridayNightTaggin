using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace FridayNightTaggin.Scripts
{
    public class Notes : MonoBehaviour // https://github.com/Team-Determination/Unity-Party/blob/master/Assets/Scripts/Notes.cs
    {
        public float speed;
        public RectTransform target;
        public bool isActive;

        void Start()
        {

        }

        void Update()
        {
            if (isActive)
                target.Translate(Vector3.up * (speed * Time.deltaTime));
        }
    }
}
