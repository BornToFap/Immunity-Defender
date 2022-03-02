using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Identifier: MonoBehaviour
{
    public int IDNUM;
    public int status;
    public  int ID_UI
    {
        set { IDNUM = value; }
        get { return IDNUM; }
    }
}
