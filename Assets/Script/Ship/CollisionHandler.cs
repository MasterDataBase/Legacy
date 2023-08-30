using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    //[SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    [SerializeField] ParticleSystem successP;
    [SerializeField] ParticleSystem crashP;

    public bool isTransitioning = false;

    public bool collisionDisabled = false;

    AudioSource asrc;

    void Start() {
        asrc = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void OnCollisionEnter(Collision other)
    {

        if (isTransitioning || collisionDisabled)
        { return; }
        switch (other.gameObject.tag)
        {
            case "Friendly":
                //Debug.Log("Ciao");
                break;

            case "Finish":
                //LoadNextLevel();
                StartSuccessSequence();
                break;

            case "Fuel":
                //Debug.Log("Ok");
                break;

            default:
                Debug.Log("Ok");
                //StartReloadSequence();
                StartCrashSequence();
                break;
        }
    }

    /// <summary>
    /// Da usare per recepire le collisioni con il sistema particellare
    /// </summary>
    /// <param name="other"></param>
    //private void OnParticleCollision(GameObject other)
    //{
    //    //StartCrashSequence();
    //    Debug.Log("Particle hit " + name);
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "NoGravity")
        {
            other.gameObject.AddComponent<PlanetGravity>();
            other.GetComponent<PlanetGravity>().SetTarget(this.gameObject.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlanetGravity planGrav)) { planGrav.Destroy(); }
    }

    public void StartSuccessSequence(){

        isTransitioning = true;
        asrc.Stop();
        GetComponent<Movement>().enabled = false;
        asrc.PlayOneShot(success);
        successP.Play();
        //Invoke("LoadNextLevel", levelLoadDelay); 
        FindObjectOfType<SceneLoader>().EndLevel();
    }

    //void StartReloadSequence(){
    //    GetComponent<Movement>().enabled = false;
    //    Invoke("ReloadLevel",levelLoadDelay);
    //}

    public void StartCrashSequence()
    {
        isTransitioning = true;
        asrc.Stop();
        // todo add SFX upon crash
        asrc.PlayOneShot(crash);
        crashP.Play();
        // todo add particle effect upon crash
        GetComponent<Movement>().enabled = false;
        //Invoke("ReloadLevel", levelLoadDelay);
        FindObjectOfType<SceneLoader>().EndLevel();
    }


    //void LoadNextLevel(){
    //    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    //    int nextSceneIndex = currentSceneIndex +1; 
    //    if(nextSceneIndex == SceneManager.sceneCountInBuildSettings){ nextSceneIndex = 0; }
    //    SceneManager.LoadScene(nextSceneIndex);
    //}

    //void ReloadLevel(){
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //}

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            //LoadNextLevel();
            FindObjectOfType<SceneLoader>().EndLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }
}
