using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class SanityBarBuilderV2
{
    [MenuItem("Tools/Crear Barra de Cordura (Sprites)")]
    public static void CrearBarraCorduraSprites()
    {
        // Buscar o crear Canvas
        Canvas canvas = Object.FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            GameObject canvasGO = new GameObject("Canvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            canvas = canvasGO.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            CanvasScaler cs = canvasGO.GetComponent<CanvasScaler>();
            cs.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            cs.referenceResolution = new Vector2(1920, 1080);
        }

        // Cargar sprites desde Resources
        Sprite frameSprite = Resources.Load<Sprite>("Sanity_Frame");
        Sprite fillSprite = Resources.Load<Sprite>("Sanity_Fill");
        Sprite cracksSprite = Resources.Load<Sprite>("Sanity_Grietas");

        // Crear contenedor
        GameObject barraGO = new GameObject("BarraCordura", typeof(RectTransform));
        barraGO.transform.SetParent(canvas.transform, false);
        RectTransform rt = barraGO.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(520, 80);
        rt.anchoredPosition = new Vector2(0, -50);

        // Marco
        GameObject frameGO = new GameObject("Fondo", typeof(RectTransform), typeof(Image));
        frameGO.transform.SetParent(barraGO.transform, false);
        Image frameImg = frameGO.GetComponent<Image>();
        if (frameSprite != null) frameImg.sprite = frameSprite;
        frameImg.preserveAspect = true;
        frameGO.GetComponent<RectTransform>().sizeDelta = rt.sizeDelta;

        // Relleno
        GameObject fillGO = new GameObject("Relleno", typeof(RectTransform), typeof(Image));
        fillGO.transform.SetParent(barraGO.transform, false);
        Image fillImg = fillGO.GetComponent<Image>();
        if (fillSprite != null) fillImg.sprite = fillSprite;
        fillImg.type = Image.Type.Filled;
        fillImg.fillMethod = Image.FillMethod.Horizontal;
        fillImg.fillOrigin = (int)Image.OriginHorizontal.Left;
        fillImg.fillAmount = 1f;
        fillGO.GetComponent<RectTransform>().sizeDelta = rt.sizeDelta;

        // Grietas overlay
        GameObject cracksGO = new GameObject("Grietas", typeof(RectTransform), typeof(Image));
        cracksGO.transform.SetParent(barraGO.transform, false);
        Image cracksImg = cracksGO.GetComponent<Image>();
        if (cracksSprite != null) cracksImg.sprite = cracksSprite;
        cracksImg.color = new Color(1f, 1f, 1f, 0f); // invisible al inicio
        cracksGO.GetComponent<RectTransform>().sizeDelta = rt.sizeDelta;

        // Añadir controlador y asignar referencias
        SanityBar sb = barraGO.AddComponent<SanityBar>();
        sb.relleno = fillImg;
        sb.overlayGrietas = cracksImg;

        Selection.activeGameObject = barraGO;
    }
}
