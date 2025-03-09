using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class Beginning : MonoBehaviour
{
    [SerializeField] private Image fadePanel;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(Begin());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Begin()
    {
        fadePanel.DOFade(0, 2f);
        yield return new WaitForSeconds(5f);
        StartCoroutine(Ending());
    }


    //CARGAR NUEVA ESCENA

    IEnumerator Ending()
    {
        yield return new WaitForSeconds(0f);
        fadePanel.DOFade(1, 2f);
        CargarNivel(1);
        
    }

    IEnumerator CargarAsync(int NumeroDeEscena)
    {
        yield return new WaitForSeconds(2); //esperar unos segundos antes de empezar la Coroutine

        AsyncOperation operation = SceneManager.LoadSceneAsync(NumeroDeEscena);
    }

    public void CargarNivel(int NumeroDeEscena)
    {
        StartCoroutine(CargarAsync(NumeroDeEscena));
    }
}
