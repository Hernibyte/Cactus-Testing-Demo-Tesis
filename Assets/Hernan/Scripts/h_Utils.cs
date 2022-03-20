﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class h_Utils
{
    /// <summary>
    /// Identifica si el layer concuerda con la mascara.
    /// </summary>
    /// <param name="mask">Mascara base contra la cual checkear</param>
    /// <param name="layer">Layer del objeto al cual identificar</param>
    /// <returns>True: concuerda. False: no concuerda.</returns>
    public static bool LayerMaskContains(LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }
}
