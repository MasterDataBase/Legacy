using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;


public class NavPoint : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textUI = null;
    [SerializeField] Image imageUI = null;

    [SerializeField] Sprite imageSprite = null;
    [SerializeField] Sprite blankSprite = null;

    [SerializeField] NavPoint nextNavPoint = null;

    [Header("Funcional")]
    [SerializeField] bool downloaded = false;
    [SerializeField] bool downloading = false;
    [SerializeField] float timeConst = float.NaN;
    [SerializeField] Collider inside = null;

    IEnumerator coo = null;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "ShipComponent" && !downloaded)
        {
            textUI.text = "Premere T per scaricare i dati";
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "ShipComponent"){ inside = other; } else { inside = null; }
    }

    private void OnTriggerExit(Collider other)
    {
        inside = null;
        downloading = false;
        StopCoroutine(download());
        coo = null;
        textUI.text = "";
        imageUI.sprite = blankSprite;
    }

    private void Update()
    {
        if (inside != null) { InizializeDownload(inside); }
    }

    private void InizializeDownload(Collider other)
    {
        if ((Input.GetKeyDown(KeyCode.T)) && !downloading && coo == null && !downloaded)
        {
            textUI.text = "Inizio download";
            imageUI.sprite = imageSprite;
            imageUI.type = Image.Type.Filled;
            imageUI.fillMethod = Image.FillMethod.Radial360;
            imageUI.fillOrigin = (int)Image.Origin360.Bottom;
            imageUI.fillAmount = 0;
            downloading = true;
            coo = download();
            StartCoroutine(download());
        }
    }

    IEnumerator download()
    {
        float time = 0f;
        do
        {
            imageUI.fillAmount += time / timeConst;
            textUI.text = "Download in corso...";
            time += 0.5f;
            yield return new WaitForSeconds(.1f);
        } while (time <= timeConst);
        textUI.text = "Download completato";
        FindObjectOfType<MissionWaypoint>().target = nextNavPoint.transform;
        downloaded = true;
        coo = null;
    }
}
