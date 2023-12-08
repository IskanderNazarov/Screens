using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;

public class Market : MonoBehaviour, IIAPListener, IResourceChangeListener {

    [SerializeField] private TextMeshProUGUI ticketsCountText;
    
    public void Show() {
        Purchaser.shared.RegisterListener(this);
        ResourceManager.shared.RegisterListener(this);
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
        Purchaser.shared.RemoveListener(this);
        ResourceManager.shared.RemoveListener(this);
    }

    public void OnIAPInitialized(bool isSuccess) {
    }

    public void OnProductPurchased(Product product) {
    }

    public void OnProductPurchased(string productId) {
        if (productId == Purchaser.chest_1_ID) {
            ResourceManager.shared.AddTickets(200);
        }
        else if (productId == Purchaser.chest_2_ID) {
            ResourceManager.shared.AddTickets(500);
        }
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason) {
    }

    public void BuyChest_1() {
        Purchaser.shared.BuyProduct(Purchaser.chest_1_ID);
    }
    
    public void BuyChest_2() {
        Purchaser.shared.BuyProduct(Purchaser.chest_2_ID);
    }

    public void BuyLocation_1() {
        if (ResourceManager.shared.CanUseTickets(500)){
            ResourceManager.shared.UseTickets(500);
        }
        
    }
    public void BuyLocation_2() {
        if (ResourceManager.shared.CanUseTickets(500)){
            ResourceManager.shared.UseTickets(500);
        }
    }
    public void BuyLocation_3() {
        if (ResourceManager.shared.CanUseTickets(500)){
            ResourceManager.shared.UseTickets(500);
        }
    }
    
    public void BuyCharacter_1() {
        if (ResourceManager.shared.CanUseTickets(300)){
            ResourceManager.shared.UseTickets(300);
        }
    }
    
    public void BuyCharacter_2() {
        if (ResourceManager.shared.CanUseTickets(300)){
            ResourceManager.shared.UseTickets(300);
        }
    }

    public void OnTicketsCountChanged(int ticketsCount) {
        ticketsCountText.text = $"{ticketsCount}";
    }
}