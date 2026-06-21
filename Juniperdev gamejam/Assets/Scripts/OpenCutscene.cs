using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCutscene : MonoBehaviour
{
    bool hasPlayed;
    public GameObject EnergyBar;
    public Playermove playermove;
    public Animator PlayerAnim;
    public GameObject Title;
    public Animator TitleAnim;

    // Start is called before the first frame update
    void Start()
    {
        playermove.frozen = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasPlayed && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Activate());
        }
    }

    IEnumerator Activate()
    {
        hasPlayed = true;
        TitleAnim.SetBool("Start", true);
        PlayerAnim.SetInteger("state", 3);
        yield return new WaitForSeconds(2);
        playermove.frozen = false;
        Title.SetActive(false);
        EnergyBar.SetActive(true);
        PlayerAnim.SetInteger("state", 0);
    }
}
