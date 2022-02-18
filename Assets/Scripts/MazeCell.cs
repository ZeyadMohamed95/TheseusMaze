using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : MonoBehaviour
{
    [SerializeField]
    private MazeCellType mazeCellType;

    public MazeCellType MazeCellType => this.mazeCellType;
}
