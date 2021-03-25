using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public static Queue<Platform> activePlatforms = new Queue<Platform>();

    public Transform platformEndingPoint;
    
    public int platformLevel;
}