using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Canvas : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    GameObject TeksJudul;
    GameObject PlayButton;
    GameObject OptionButton;
    public GameObject canvasToLoad;
    public GameObject panelToAnimate;
    void Start()
    {
        TeksJudul = this.transform.Find("Panel").Find("Background").Find("JudulGame").gameObject;
        PlayButton = this.transform.Find("Panel").Find("Background").Find("btnPlay").gameObject;
        OptionButton = this.transform.Find("Panel").Find("Background").Find("btnOption").gameObject;

        iTween.MoveFrom(TeksJudul, iTween.Hash("y", 1000, "time",
        2f, "delay", 0f, "easeType", "easeOutBounce"));
        iTween.ScaleFrom(PlayButton, iTween.Hash("x", 0, "y", 0, "time", 2f, "delay", 1f,
        "easeType", "easeOutQuart"));
        iTween.MoveFrom(OptionButton, iTween.Hash("x", -1000, "time", 2f, "delay", 2f, "easeType",
        "easeOutExpo",
        "oncomplete", "selesaianim", "oncompletetarget", this.gameObject));
    }
    void selesaianim()
    {
        Debug.Log("selesai sudah animasinya");
        this.transform.Find("Panel").Find("Background").GetComponent<Image>().color = Color.green;
    }

    public void btnPlayClick()
    {
        Debug.Log("Tombol Play diklik");
        SceneManager.LoadScene("Week9_Health Bar");
    }

    public void btnOptionClick()
    {
        Debug.Log("Tombol Option diklik");
        canvasToLoad.SetActive(true);
        panelToAnimate.GetComponent<RectTransform>().localScale = Vector3.zero;

        iTween.ScaleTo(panelToAnimate, iTween.Hash(
            "scale", Vector3.one,
            "time", 0.5f,
            "easeType", "easeOutBack",
            "ignoretimescale", true
        ));

    }

    public void closeOptionClick()
    {

        panelToAnimate.GetComponent<RectTransform>().localScale = Vector3.one; 

        iTween.ScaleTo(panelToAnimate, iTween.Hash(
            "scale", Vector3.zero,
            "time", 0.4f,
            "easeType", "easeInBack",
            "ignoretimescale", true
        ));

    }
    public void DisableOptionCanvas()
    {
        canvasToLoad.SetActive(false);
    }
}
