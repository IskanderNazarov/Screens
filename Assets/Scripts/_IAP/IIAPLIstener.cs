using UnityEngine.Purchasing;

public interface IIAPListener {

    void OnIAPInitialized(bool isSuccess);
    void OnProductPurchased(Product product);
    void OnProductPurchased(string productId);
    void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason);

}