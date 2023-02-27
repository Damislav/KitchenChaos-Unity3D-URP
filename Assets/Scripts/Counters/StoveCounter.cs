using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
  public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
  public EventHandler<OnStateChangedEventArgs> OnStateChanged;

  public class OnStateChangedEventArgs : EventArgs
  {
    public State state;
  }
  public enum State
  {
    Idle,
    Frying,
    Fried,
    Burned
  }

  [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
  [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

  private State state;
  private float fryingTimer;
  private FryingRecipeSO fryingRecipeSO;
  private float burningTimer;
  private BurningRecipeSO burningRecipeSO;



  private void Start()
  {
    state = State.Idle;
  }
  private void Update()
  {

    if (HasKitchenObject())
    {
      switch (state)
      {
        case State.Idle:
          break;
        case State.Frying:
          fryingTimer += Time.deltaTime;

          OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
          {
            progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
          });

          Debug.Log("Cooking");

          if (fryingTimer > fryingRecipeSO.fryingTimerMax)
          {
            //Fried
            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);

            Debug.Log("Object fried");
            state = State.Fried;
            // reset burning timer
            burningTimer = 0f;
            burningRecipeSO = GetBurningecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
            {
              state = state
            });
          }
          break;

        case State.Fried:
          burningTimer += Time.deltaTime;

          OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
          {
            progressNormalized = burningTimer / burningRecipeSO.burninTimerMax
          });
          if (burningTimer > burningRecipeSO.burninTimerMax)
          {
            //Fried
            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);

            Debug.Log("Object burned");

            state = State.Burned;

            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
            {
              state = state
            });

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
              progressNormalized = 0f
            });
          }
          break;

        case State.Burned:
          break;
      }
    }
  }

  public override void Interact(Player player)
  {
    // pick up and drop items
    if (!HasKitchenObject())
    {//there is no kitchenobject here}
      if (player.HasKitchenObject())
      {
        // Player is carrying something
        if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
        {
          // player carrying something can be fried
          player.GetKitchenObject().SetKitchenObjectParent(this);

          fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

          state = State.Frying;
          fryingTimer = 0f;

          OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
          {
            state = state
          });
          OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
          {
            progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
          });
        }
      }
      else
      {
        // player not carrying anything
      }
    }
    else
    {
      // there is kitchen object
      if (player.HasKitchenObject())
      {
        //player cayyring something
        if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) { }
        //Player is holding palte
        if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
        {
          GetKitchenObject().DestroySelf();
        }
        state = State.Idle;
        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
        {
          state = state
        });
        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
        {
          progressNormalized = 0f
        });
      }
      else
      {
        // player is not carrying anything
        GetKitchenObject().SetKitchenObjectParent(player);

        state = State.Idle;
        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
        {
          state = state
        });
        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
        {
          progressNormalized = 0f
        });
      }
    }
  }

  private bool HasRecipeWithInput(KitchenObjectSO inputkitchenObjectSO)
  {
    FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputkitchenObjectSO);
    return fryingRecipeSO != null;
  }

  private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputkitchenObjectSO)
  {
    FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputkitchenObjectSO
    );
    if (fryingRecipeSO != null)
    {
      return fryingRecipeSO.output;
    }
    else
    {
      return null;
    }
  }

  private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
  {
    foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
    {
      if (fryingRecipeSO.input == inputKitchenObjectSO)
      {
        return fryingRecipeSO;
      }
    }
    return null;
  }

  private BurningRecipeSO GetBurningecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
  {
    foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
    {
      if (burningRecipeSO.input == inputKitchenObjectSO)
      {
        return burningRecipeSO;
      }
    }
    return null;
  }

}
