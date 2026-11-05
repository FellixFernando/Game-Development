using UnityEngine;

public class AudioCamera_8 : MonoBehaviour
{
    [SerializeField] AudioSource sumberSuara;
    [SerializeField] AudioSource sumberSuaraJalan;
    [SerializeField] AudioClip clipMusik1;
    [SerializeField] AudioClip clipMusik2;
    [SerializeField] AudioClip clipSuaraJalan;
    void Start()
    {
        sumberSuara.clip = clipMusik1;
        sumberSuara.loop = true;
        sumberSuara.volume = 0.3f;
        sumberSuara.Play();

        sumberSuaraJalan.clip = clipSuaraJalan;
        sumberSuaraJalan.volume = 1f;
    }

    public void rubahMusik1()
    {
        sumberSuara.Stop();
        sumberSuara.clip = clipMusik2;
        sumberSuara.Play();
    }
    public void rubahMusik2()
    {
        sumberSuara.Stop();
        sumberSuara.clip = clipMusik1;
        sumberSuara.Play();
    }

    public void suaraBerjalan()
    {
        if (!sumberSuaraJalan.isPlaying)
        {
            sumberSuaraJalan.loop = true;
            sumberSuaraJalan.Play();
        }
    }

    public void berhentiJalan()
    {
        if (sumberSuaraJalan.isPlaying)
        {
            sumberSuaraJalan.Stop();
        }
    }
    // Update is called once per frame

}
