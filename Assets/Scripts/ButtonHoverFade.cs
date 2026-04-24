using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverFade : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image background;
    public Image backgroundtip;

    public float fadeSpeed = 5f;

    public float bgMaxAlpha = 0.5f; 
    public float tipMaxAlpha = 1f;  

    private float targetAlpha = 0f;

    void Update()
    {
        if (background == null || backgroundtip == null) return;

        Color bgColor = background.color;
        Color tipColor = backgroundtip.color;

        bgColor.a = Mathf.Lerp(bgColor.a, targetAlpha * bgMaxAlpha, Time.unscaledDeltaTime * fadeSpeed);
        tipColor.a = Mathf.Lerp(tipColor.a, targetAlpha * tipMaxAlpha, Time.unscaledDeltaTime * fadeSpeed);

        background.color = bgColor;
        backgroundtip.color = tipColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetAlpha = 1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetAlpha = 0f;
    }
}