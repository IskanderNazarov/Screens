using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour {
    public const string CUR_LEVEL_KEY = "cur_level_key";

    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private MapLvlItem[] levelIcons;
    [SerializeField] private ScrollRect scrollbar;
    [SerializeField] private Game game;
    [SerializeField] private TextMeshProUGUI ticketsCountText;

    private bool isInit;
    private int curIndex;


    private void Init() {
        isInit = true;
        for (var i = 0; i < levelIcons.Length; i++) {
            levelIcons[i].Init(i);
            levelIcons[i].OnItemClicked += OnItemClicked;
        }
    }

    private void OnItemClicked(int itemIndex) {
        game.StartGame(itemIndex);
    }

    public void Show() {
        if (!isInit) Init();

        curIndex = PlayerPrefs.GetInt(CUR_LEVEL_KEY, 0);
        UpdateItems();
        scrollbar.verticalNormalizedPosition = (float)curIndex / (levelIcons.Length - 1);
        gameObject.SetActive(true);

        ticketsCountText.text = "" + ResourceManager.shared.Tickets;
    }

    private void UpdateItems() {
        print("CurIndex: " + curIndex);
        foreach (var l in levelIcons) {
            l.UpdateItem(curIndex);
        }
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void OnHomeClicked() {
        Hide();
        mainMenu.Show();
    }
}