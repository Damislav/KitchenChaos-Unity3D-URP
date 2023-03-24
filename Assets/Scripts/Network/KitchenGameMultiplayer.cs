using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;


public class KitchenGameMultiplayer : NetworkBehaviour
{
    public static KitchenGameMultiplayer Instance { get; private set; }

    [SerializeField] private KitchenObjectListSo kitchenObjectListSo;
    private void Awake()
    {
        Instance = this;
    }


    public void SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        SpawnKitchenObjectsServerRpc(GetKitcheObjectSoIndex(kitchenObjectSO), kitchenObjectParent.GetNetworkObject());
    }

    [ServerRpc(RequireOwnership = false)]
    private void SpawnKitchenObjectsServerRpc(int kitchenObjectSoIndex, NetworkObjectReference kitchenObjectParentNetworkObjectReference)
    {
        KitchenObjectSO kitchenObjectSO = GetKitchenObjectSoFromIndex(kitchenObjectSoIndex);
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);

        NetworkObject kitchenObjectNetworkObject = kitchenObjectTransform.GetComponent<NetworkObject>();
        kitchenObjectNetworkObject.Spawn(true);

        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();

        kitchenObjectParentNetworkObjectReference.TryGet(out NetworkObject kitchenObjectParentNetworkObject);
    
        IKitchenObjectParent kitchenObjectParent = kitchenObjectParentNetworkObject.GetComponent<IKitchenObjectParent>();
     
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
    }

    private int GetKitcheObjectSoIndex(KitchenObjectSO kitchenObjectSO)
    {
        // convert to index
        return kitchenObjectListSo.kitchenObjectsSoList.IndexOf(kitchenObjectSO);
    }

    private KitchenObjectSO GetKitchenObjectSoFromIndex(int kitchenObjectSoIndex)
    {
        return kitchenObjectListSo.kitchenObjectsSoList[kitchenObjectSoIndex];
    }
}
