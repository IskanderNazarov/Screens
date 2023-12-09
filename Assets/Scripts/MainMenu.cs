using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour, IResourceChangeListener {

    [SerializeField] private Map map;
    [SerializeField] private TextMeshProUGUI ticketsCountText;


    public void Show() {
        gameObject.SetActive(true);
    }
    
    public void Hide() {
        gameObject.SetActive(true);
    }

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