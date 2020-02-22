using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoRotation : MonoBehaviour
{
   private void LateUpdate()
   {
      transform.rotation = Quaternion.identity;
   }
}
