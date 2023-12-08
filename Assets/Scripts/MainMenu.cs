using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour, IResourceChangeListener {

    [SerializeField] private TextMeshProUGUI ticketsCountText;


    private void OnEnable() {
        ResourceManager.shared.RegisterListener(this);
    }

    private void OnDisable() {
        ResourceManager.shared.RemoveListener(this);
    }

    public void OnTicketsCountChanged(int ticketsCount) {
        ticketsCountText.text = $"{ticketsCount}";
    }
}