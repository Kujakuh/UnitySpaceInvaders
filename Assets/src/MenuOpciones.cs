using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuOpciones : MonoBehaviour
{

    [SerializeField] private Slider _brightnessSlider;
    [SerializeField] private Image _overLay;    
    [SerializeField] private AudioMixer audioMixer;

     void Update()
    {
        DarkOverlay();
    }
    public void PantallaCompleta(bool pantallaCompleta){
        Screen.fullScreen = pantallaCompleta;
    }

    public void CambiarVolumen(float volumen){
        audioMixer.SetFloat("Volumen",volumen);
    }

    public void CambiarCalidad(int index){
        QualitySettings.SetQualityLevel(index);
    }
    private void DarkOverlay()
    {
        var tempColor = _overLay.color;
        tempColor.a = _brightnessSlider.value;
        _overLay.color = tempColor;
    }
}
