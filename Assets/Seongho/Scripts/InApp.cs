using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class InApp : MonoBehaviour, IStoreListener
{
    private static IStoreController m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

    public static string mProductID_test = "product_test";

    private bool mIsInitialized
    {
        get
        {
            return m_StoreController != null && m_StoreExtensionProvider != null;
        }
    }


    private void Start()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
               // enables saving game progress.
               //.EnableSavedGames()
               // registers a callback to handle game invitations received while the game is not running.
               //.WithInvitationDelegate(<callback method>)
               // registers a callback for turn based match notifications received while the
               // game is not running.
               //.WithMatchDelegate(<callback method>)
               // require access to a player's Google+ social graph (usually not needed)
               .RequireGooglePlus()
               .Build();

        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        Social.localUser.Authenticate((bool success) => {
            InitializePurchasing();
        });


    }
    public void InitializePurchasing()
    {
        if (mIsInitialized)
        {
            // ... we are done here.
            return;
        }

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(mProductID_test, ProductType.Consumable);
        UnityPurchasing.Initialize(this, builder);
    }

    #region IStoreListener
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("InApp Initialize Failed \n" + error);
    }
    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    {
        Debug.Log("Purchase Failed \n" + i.definition.id + "\n" + p);
    }
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (String.Equals(args.purchasedProduct.definition.id, mProductID_test, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
        }
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }

        // Return a flag indicating whether this product has completely been received, or if the application needs 
        // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
        // saving purchased products to the cloud, and when that save is delayed. 
        return PurchaseProcessingResult.Complete;
    }

    #endregion


    private void BuyProductID(string tProductID)
    {
        if(mIsInitialized)
        {
            Product tProduct = m_StoreController.products.WithID(tProductID);
            if(tProduct != null && tProduct.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", tProduct.definition.id));
                m_StoreController.InitiatePurchase(tProduct);
            }
            else
            {
                // ... report the product look-up failure situation  
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }


    #region Test
    public void BuyTestProduct()
    {
        BuyProductID(mProductID_test);
    }
    #endregion
}
