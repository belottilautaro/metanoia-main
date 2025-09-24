using UnityEngine;
using UnityEngine.UI;

public class SanityBar : MonoBehaviour
{
    [Header("Componentes")]
    public Image relleno;
    public Image overlayGrietas;

    [Header("Valores")]
    public float corduraMax = 100f;
    public float corduraActual;

    private void Start()
    {
        corduraActual = corduraMax;
        ActualizarBarra();
    }

    private void Update()
    {
        // Controles de prueba
        if (Input.GetKeyDown(KeyCode.K))
        {
            CambiarCordura(-10f);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            CambiarCordura(10f);
        }
    }

    public void CambiarCordura(float cantidad)
    {
        corduraActual = Mathf.Clamp(corduraActual + cantidad, 0, corduraMax);
        ActualizarBarra();
    }

    private void ActualizarBarra()
    {
        float porcentaje = corduraActual / corduraMax;

        if (relleno != null)
        {
            relleno.fillAmount = porcentaje;

            // Colores según nivel
            if (porcentaje > 0.6f)
                relleno.color = Color.green;
            else if (porcentaje > 0.3f)
                relleno.color = Color.yellow;
            else
                relleno.color = Color.red;
        }

        if (overlayGrietas != null)
        {
            // Mostrar más grietas cuanto menor sea la cordura
            overlayGrietas.color = new Color(1f, 1f, 1f, 1f - porcentaje);
        }
    }
}

