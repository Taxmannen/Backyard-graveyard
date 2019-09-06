using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <author>Simon Ektjärn</author>
/// </summary>

public enum TaskObjectType { Head, Body, Ornament };
public class TaskObject : MonoBehaviour
{
    public TaskObjectType taskObjectType = TaskObjectType.Head;
    public Heads head = Heads.None;
    public Bodies body = Bodies.None;
    public Ornaments ornament = Ornaments.None;
}
