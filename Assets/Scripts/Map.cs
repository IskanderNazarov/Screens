using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour {
    private const string CUR_LEVEL_KEY = "cur_level_key";
    
    [SerializeField]private Image[] levelIcons;

    public void Show() {
        var cur = PlayerPrefs.GetInt(CUR_LEVEL_KEY, 0);
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}