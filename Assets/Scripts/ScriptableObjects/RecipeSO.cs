using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RecipeSO : ScriptableObject {


    public List<KitchenObjectSO> kitchenObjectSOList;
    public string recipeName;

    public static implicit operator int(RecipeSO v)
    {
        throw new NotImplementedException();
    }
}