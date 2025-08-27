using UnityEngine;
using UnityEngine.Events;

using System.Linq;
using System.Collections;
using System.Collections.Generic;

public abstract class IMenu : MonoBehaviour
{
    [Header("PANELS")]
    public IPanel startPanel;
    [Space(5)]
    public IPanel currentPanel;
    public IPanel lastPanel;
    [Space(5)]
    public List<IPanel> allPanels = new List<IPanel>();

    [Header("STATES")]
    public bool returnable;
    public bool allowReturning;
    public UnityEvent OnShow;
    public UnityEvent OnHide;

    CanvasGroup cg;

    public virtual void Show()
    {
        if (cg == null)
            cg = gameObject.AddComponent<CanvasGroup>();

        OnShow?.Invoke();
        gameObject.SetActive(true);

        LeanTween.alphaCanvas(cg, 1, 0.2f);
    }
    public virtual void Hide()
    {
        if (cg == null)
            cg = gameObject.AddComponent<CanvasGroup>();

        OnHide?.Invoke();

        LeanTween.alphaCanvas(cg, 0, 0.2f).setOnComplete(Events);
    }

    void Events()
    {
        gameObject.SetActive(false);
    }

    public virtual void OpenPanel(IPanel panelToShow, bool saveLastPanel = true)
    {
        if (panelToShow == null)
            return;

        if (!allPanels.Contains(panelToShow))
            return;

        if (panelToShow == currentPanel)
            return;

        foreach (IPanel m in allPanels)
        {
            m.Hide();
        }

        if (currentPanel != null)
        {
            if (saveLastPanel)
                lastPanel = currentPanel;
            else
                lastPanel = null;

            currentPanel.Hide();
        }
        else
        {
            lastPanel = null;
        }

        currentPanel = panelToShow;
        panelToShow.Show();
    }
    public void OpenPanelButton(IPanel panelToShow)
    {
        OpenPanel(panelToShow);
    }

    void OnValidate()
    {
        allPanels = allPanels.Distinct().ToList();
    }
}