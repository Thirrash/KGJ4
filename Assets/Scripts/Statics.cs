using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Statics
{
    public static int MotherLayer = 8;
    public static int ONRLayer = 9;
    public static int JKMLayer = 10;
    public static int BuildingLayer = 11;
    public static int FloorLayer = 12;
    public static int HomelessLayer = 13;
    public static int AmericanLayer = 14;

    public static int DestroyableLayers = 1 << MotherLayer | 1 << ONRLayer | 1 << JKMLayer | 1 << BuildingLayer;
}
