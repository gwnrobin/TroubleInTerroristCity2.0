﻿using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : Humanoid
{
    public Camera Camera { get => _playerCamera; }

    public readonly Activity Fire = new();

    // Movement
    public readonly Value<float> MoveCycle = new();
    public readonly Message MoveCycleEnded = new();

    public readonly Value<RaycastInfo> RaycastInfo = new(null);

    /// <summary>Is there any object close to the camera? Eg. A wall</summary>
    public readonly Value<Collider> ObjectInProximity = new Value<Collider>();

    public readonly Value<bool> ViewLocked = new Value<bool>();

    public readonly Value<float> Stamina = new(100f);

    public readonly Value<Vector2> MoveInput = new(Vector2.zero);
    public readonly Value<Vector2> LookInput = new(Vector2.zero);
    public readonly Value<int> ScrollValue = new(0);

    public readonly Activity DisabledMovement = new();

    public readonly Attempt DestroyEquippedItem = new();
    
    //public readonly Attempt ChangeUseMode = new Attempt();
    
    //public readonly Activity Sliding = new Activity();
    
    [Header("Camera")]
    [SerializeField] private Camera _playerCamera;

    public Dictionary<string, Activity> GetAllActivities()
    {
        Dictionary<string, Activity> activities = new Dictionary<string, Activity>();
        FieldInfo[] fields = GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (FieldInfo field in fields)
        {
            if (field.FieldType == typeof(Activity) && field.GetValue(this) != null)
            {
                activities.Add(field.Name, (Activity)field.GetValue(this));
            }
        }

        PropertyInfo[] properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (PropertyInfo property in properties)
        {
            if (property.PropertyType == typeof(Activity) && property.GetValue(this) != null)
            {
                activities.Add(property.Name, (Activity)property.GetValue(this));
            }
        }

        return activities;
    }
}