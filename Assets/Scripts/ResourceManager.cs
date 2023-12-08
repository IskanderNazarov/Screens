using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ResourceManager : MonoBehaviour {
    public static ResourceManager shared { private set; get; }
    public static string TICKETS_COUNT_KEY = "tickets_count_key";


    private int ticketsCount;
    private HashSet<IResourceChangeListener> listeners;

    private void Awake() {
        if (shared == null) {
            shared = this;
        }
        else if (shared != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        Init();
    }

    private void Init() {
        listeners = new HashSet<IResourceChangeListener>();
        ticketsCount = PlayerPrefs.GetInt(TICKETS_COUNT_KEY, 0);
    }

    public void AddTickets(int tickets) {
        ticketsCount += tickets;
        NotifyTicketsChanged();
    }
    
    public void UseTickets(int tickets) {
        ticketsCount -= tickets;
        NotifyTicketsChanged();
    }

    public bool CanUseTickets(int tickets) {
        return tickets <= ticketsCount;
    }

    public void RegisterListener(IResourceChangeListener listener) {
        listeners.Add(listener);
        
        listener.OnTicketsCountChanged(ticketsCount);
    }
    public void RemoveListener(IResourceChangeListener listener) {
        listeners.Remove(listener);
    }

    private void NotifyTicketsChanged() {
        foreach (var l in listeners) {
            l.OnTicketsCountChanged(ticketsCount);
        }
    }
}