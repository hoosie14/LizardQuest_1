using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Doorway : MonoBehaviour
{
    public PlayerMovement player;
    public GameObject Fader;

    public string Scene; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.enablePlayerControls = false;
        StartCoroutine(DoorLoad());
    }

    IEnumerator DoorLoad()
    {
        player.enablePlayerControls = false;
        Fader.active = true;

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(Scene); 
    }
}
