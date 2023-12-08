using System;
using System.Collections.Generic;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

//using AppsFlyerSDK;

public class Purchaser : MonoBehaviour, IStoreExtension, IDetailedStoreListener {
    public const int FREE_HINTS_COUNT = 1;
    public static Purchaser shared { get; private set; }

    public const  string chest_1_ID = "buy_chest_id_1";
    public const  string chest_2_ID = "buy_chest_id_2";

    private static IStoreController storeController; // The Unity Purchasing system.
    private static IExtensionProvider storeExtensionProvider;
    private HashSet<IIAPListener> listeners;

    private void Awake() {
        if (shared == null) {
            shared = this;
            init();
        }
        else if (shared != this) {
            Destroy(this);
        }


        DontDestroyOnLoad(shared);
    }

    private void init() {
        print("PURCHASER INIT");
        listeners = new HashSet<IIAPListener>();
        //InitPurchasing();
    }

    //------------------------------------------------------------------

    public void RegisterListener(IIAPListener listener) {
        listeners.Add(listener);
        if (isInitialized()) {
            listener.OnIAPInitialized(true);
        }
    }

    public void RemoveListener(IIAPListener listener) {
        if (listeners.Contains(listener)) {
            listeners.Remove(listener);
        }
    }

    //------------------------------------------------------------------

    private async void InitPurchasing() {
        if (isInitialized()) {
            OnInitialized(true);
            return;
        }


        try {
            const string environment = "production";
            var options = new InitializationOptions().SetEnvironmentName(environment);
            await UnityServices.InitializeAsync(options);
        }
        catch (Exception exception) {
            // An error occurred during services initialization.
            print("UGS init error: " + exception.Message);
        }


        //show loading effect
        OnInitialized(false);

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(chest_1_ID, ProductType.Consumable);
        builder.AddProduct(chest_2_ID, ProductType.Consumable);

        //UnityPurchasing.Initialize(this, builder);
        UnityPurchasing.Initialize(this, builder);
    }

    //---------------------------------------------------------------------------

    public bool isInitialized() {
        return storeController != null && storeExtensionProvider != null;
    }

    //---------------------------------------------------------------------------

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions) {
        storeController = controller;
        storeExtensionProvider = extensions;
        OnInitialized(true);
    }

    //---------------------------------------------------------------------------

    public void OnInitializeFailed(InitializationFailureReason error) {
        OnInitialized(false);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message) {
        OnInitialized(false);
    }

    //---------------------------------------------------------------------------

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent) {
        var product = purchaseEvent.purchasedProduct;

        OnPurchased(product);
        return PurchaseProcessingResult.Complete;
    }
    
    public Product GetProductByID(string id) {
        return !isInitialized() ? null : storeController.products.WithID(id);
    }

    //---------------------------------------------------------------------------

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason) {
        OnPurchaseProcessFailed(product, failureReason);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription) {
    }

    //---------------------------------------------------------------------------

    public void BuyProduct(string id) {
        /*if (!isInitialized()) return;

        var product = storeController.products.WithID(id);
        if (product != null && product.availableToPurchase) {
            storeController.InitiatePurchase(product);
        }*/
        
        
        var tempListeners = new HashSet<IIAPListener>(listeners);
        foreach (var iapListener in tempListeners) {
            iapListener.OnProductPurchased(id);
        }
    }

    //---------------------------------------------------------------------------

    private void OnInitialized(bool isSuccess) {
        foreach (var iapListener in listeners) {
            iapListener.OnIAPInitialized(isSuccess);
        }
    }

    private void OnPurchased(Product product) {
        //protection of modifying the 'listeners' collection because
        //listeners can be removed after 'onProductPurchased' called
        var tempListeners = new HashSet<IIAPListener>(listeners);


        print("RESTORED_- P: " + product.definition.id);
        foreach (var iapListener in tempListeners) {
            iapListener.OnProductPurchased(product);
        }
    }

    private void OnPurchaseProcessFailed(Product product, PurchaseFailureReason failureReason) {
        foreach (var iapListener in listeners) {
            iapListener.OnPurchaseFailed(product, failureReason);
        }
    }

    //-----------------------------------------------------------------

    public void restorePurchasesBtn() {
        if (!isInitialized()) return;

        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer) {
            var ext = storeExtensionProvider.GetExtension<IAppleExtensions>();
            ext?.RestoreTransactions(delegate(bool b, string s) { });
        }
    }
}